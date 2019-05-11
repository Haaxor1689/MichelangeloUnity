using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Models;
using Michelangelo.Models.MichelangeloApi;
using Michelangelo.Utility;
using RSG;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Michelangelo.Scripts {
    public abstract class ObjectBase : MonoBehaviour {
        public TreeViewState TreeViewState => treeViewState ?? (treeViewState = new TreeViewState());

        [SerializeField]
        [HideInInspector]
        private TreeViewState treeViewState;

        [SerializeField]
        protected Material[] Materials;
        
        [SerializeField]
        private ParseTreeData parseTreeData;
        public ParseTree ParseTree => (parseTreeData ? parseTreeData : parseTreeData = GetParseTreeData()).ParseTree;
        
        [SerializeField]
        public List<MeshGizmoData> MeshHighlights;

        private ParseTreeData GetParseTreeData() {
            var child = transform.Find("ParseTreeData");
            if (child) {
                return child.GetComponent<ParseTreeData>();
            }
            var newObject = new GameObject("ParseTreeData");
            newObject.transform.SetParent(transform);
            newObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
            return newObject.AddComponent<ParseTreeData>();
        }

        public virtual bool CanGenerate => true;

        protected abstract IPromise<GenerateGrammarResponse> GenerateCallback();
        
        /// <summary>
        /// Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise"/> that contains mesh info and response message from server.</returns>
        public IPromise<GenerateGrammarResponse> Generate() => !CanGenerate 
            ? Promise<GenerateGrammarResponse>.Rejected(new ApplicationException("Generate request error:\nThis object can't be generated right now.")) 
            : GenerateCallback().Then(response => {
                CreateMesh(response.ParseTree, response.Materials);
                return response;
            });

        /// <summary>
        /// Instantiates new GameObject at the pivot of node specified by id.
        /// </summary>
        /// <param name="nodeId">The node Id from current parse tree.</param>
        /// <returns>Instantiated GameObject.</returns>
        public GameObject AttachGameObjectToNode(uint nodeId) {
            var node = ParseTree[nodeId];
            var newObject = new GameObject();
            newObject.transform.SetParent(transform);
            newObject.transform.position = MeshUtilities.MatrixFromArray(node.Shape.Transform).ExtractPosition();
            return newObject;
        }

        private void CreateMesh(ParseTree parseTree, IDictionary<int, MaterialModel> materials) {
            DeleteOldMeshes();

            parseTreeData = GetParseTreeData();
            parseTreeData.ParseTree = parseTree;
            Materials = materials.Select(m => MeshUtilities.MaterialFromModel(m.Value)).ToArray();
            
            var nodes = ParseTree.GetMeshNodes();
            foreach (var node in nodes) {
                ParseTreeNode.Construct(transform, ParseTree, node, Materials);
            }
        }

        private void DeleteOldMeshes() {
            for (var i = 0; i < transform.childCount; ++i) {
                var child = transform.GetChild(i);
                if (child.GetComponent<ParseTreeNode>()) {
                    DestroyImmediate(child.gameObject);
                    --i;
                }
            }
        }

        private void OnDrawGizmosSelected() {
            if (MeshHighlights == null) {
                return;
            }
            foreach (var data in MeshHighlights) {
                if (data.Mesh.vertexCount == 0) {
                    return;
                }

                Gizmos.color = new Color(0.97f, 0.58f, 0.11f);
                Gizmos.DrawWireMesh(
                    data.Mesh,
                    data.Position + transform.position,
                    data.Rotation * transform.rotation,
                    data.Scale + transform.localScale
                );
                Gizmos.color = new Color(0.97f, 0.58f, 0.11f, 0.3f);
                Gizmos.DrawMesh(
                    data.Mesh,
                    data.Position + transform.position,
                    data.Rotation * transform.rotation,
                    data.Scale + transform.localScale
                );
            }
        }
    }
}
