using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Model.MichelangeloApi;
using Michelangelo.Utility;
using RSG;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Michelangelo.Scripts {
    public abstract class ObjectBase : MonoBehaviour, IObjectBase {
        public TreeViewState TreeViewState => treeViewState ?? (treeViewState = new TreeViewState());

        [SerializeField]
        [HideInInspector]
        private TreeViewState treeViewState; 

        [SerializeField]
        protected bool isInEditMode;
        public bool IsInEditMode => isInEditMode;

        [SerializeField]
        protected Material[] Materials;
        
        [SerializeField]
        private ParseTreeData parseTreeData;
        public ParseTree ParseTree {
            get {
                if (parseTreeData == null) {
                    InitParseTreeData();
                }
                return parseTreeData.ParseTree;
            }
        }

        void InitParseTreeData() {
            var child = transform.Find("ParseTreeData");
            if (child) {
                parseTreeData = child.GetComponent<ParseTreeData>();
                return;
            }
            var newObject = new GameObject("ParseTreeData");
            newObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
            newObject.transform.SetParent(transform);
            parseTreeData = newObject.AddComponent<ParseTreeData>();
        }

        public bool HasMesh {
            get {
                for (var i = 0; i < transform.childCount; ++i) {
                    if (transform.GetChild(i).GetComponent<ParseTreeNode>()) {
                        return true;
                    }
                }
                return false;
            }
        }
        
        public IReadOnlyList<Tuple<Mesh, Matrix4x4>> MeshHighlights;

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

        protected void CreateMesh(ParseTree parseTree, IDictionary<int, MaterialModel> materials) {
            DeleteOldMeshes();

            InitParseTreeData();
            parseTreeData.ParseTree = parseTree;
            Materials = materials.Select(m => MaterialFromModel(m.Value)).ToArray();

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

        private static Material MaterialFromModel(MaterialModel model) {
            model.Scalars = model.Scalars ?? new Dictionary<string, double>();
            model.Vectors = model.Vectors ?? new Dictionary<string, double[]>();

            var material = new Material(Shader.Find("Standard"));
            material.SetColor("_Color", new Color((float) model.Albedo[0], (float) model.Albedo[1], (float) model.Albedo[2]));
            material.SetFloat("_Metallic", (float) model.Scalars.GetValueOrDefault("gIi", 0.0));
            material.SetFloat("_Glossiness", 1.0f - (float) model.Scalars.GetValueOrDefault("gR", 1.0));
            return material;
        }
    }
}
