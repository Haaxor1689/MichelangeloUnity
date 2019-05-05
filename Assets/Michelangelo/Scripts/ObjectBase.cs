using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Model.MichelangeloApi;
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
        public ParseTree ParseTree => (parseTreeData ?? (parseTreeData = GetParseTreeData())).ParseTree;
        
        public IReadOnlyList<Tuple<Mesh, Matrix4x4>> MeshHighlights;

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

        protected abstract IPromise<GenerateGrammarResponse> GenerateCallback();
        
        /// <summary>
        /// Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise"/> that contains mesh info and response message from server.</returns>
        public IPromise<GenerateGrammarResponse> Generate() {
            return GenerateCallback().Then(response => CreateMesh(response.ParseTree, response.Materials));
        }

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

            GetParseTreeData();
            parseTreeData.ParseTree = parseTree;
            Materials = materials.Select(m => MeshUtilities.MaterialFromModel(m.Value)).ToArray();

            var nodes = parseTree.GetMeshNodes();
            foreach (var node in nodes) {
                ParseTreeNode.Construct(transform, parseTree, node, Materials);
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
            foreach (var mesh in MeshHighlights) {
                if (mesh.Item1.vertexCount == 0) {
                    return;
                }

                Gizmos.color = new Color(0.97f, 0.58f, 0.11f);
                Gizmos.DrawWireMesh(
                    mesh.Item1,
                    mesh.Item2.ExtractPosition() + transform.position,
                    mesh.Item2.ExtractRotation() * transform.rotation,
                    mesh.Item2.ExtractScale() + transform.localScale + new Vector3(0.1f, 0.1f, 0.1f)
                );
                Gizmos.color = new Color(0.97f, 0.58f, 0.11f, 0.3f);
                Gizmos.DrawMesh(
                    mesh.Item1,
                    mesh.Item2.ExtractPosition() + transform.position,
                    mesh.Item2.ExtractRotation() * transform.rotation,
                    mesh.Item2.ExtractScale() + transform.localScale + new Vector3(0.1f, 0.1f, 0.1f)
                );
            }
        }
    }
}
