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
    /// <summary>
    ///   Base class for all Michelangelo objects.
    /// </summary>
    [RequireComponent(typeof(ParseTreeData))]
    public abstract class ObjectBase : MonoBehaviour {
        [SerializeField]
        private Material[] materials;

        [SerializeField]
        internal List<MeshGizmoData> MeshHighlights;

        private ParseTreeData parseTreeData;
        private ParseTreeData ParseTreeData => parseTreeData ? parseTreeData : parseTreeData = GetComponent<ParseTreeData>();

        [SerializeField]
        [HideInInspector]
        private TreeViewState treeViewState;

        /// <summary>
        ///   State of parse tree view.
        /// </summary>
        public TreeViewState TreeViewState => treeViewState ?? (treeViewState = new TreeViewState());

        /// <summary>
        ///   Parse tree of this object.
        /// </summary>
        public ParseTree ParseTree => ParseTreeData.ParseTree;

        /// <summary>
        ///   Returns true if object can be sent for generation.
        /// </summary>
        public virtual bool CanGenerate => true;

        /// <summary>
        ///   Runs common construct code for all ObjectBase deriving scripts.
        /// </summary>
        protected void Construct() {
            ParseTreeData.hideFlags = HideFlags.HideInInspector;
        }

        /// <summary>
        ///   Updates highlights of selected parse tree nodes visible in scene view.
        /// </summary>
        public void UpdateNodeHighlights() {
            MeshHighlights = treeViewState.selectedIDs.Where(id => ParseTree[(uint) id].Rule != "ROOT")
                                          .Select(id => {
                                              var node = ParseTree[(uint) id];
                                              var matrix = MeshUtilities.MatrixFromArray(node.Shape.Transform);
                                              return new MeshGizmoData { Position = matrix.ExtractPosition(), Rotation = matrix.ExtractRotation(), Scale = matrix.ExtractScale() + new Vector3(0.05f, 0.05f, 0.05f), Model = node.Shape };
                                          })
                                          .ToList();
        }

        /// <summary>
        ///   API callback to Michelangelo service.
        /// </summary>
        /// <returns>Generated response.</returns>
        protected abstract IPromise<GenerateGrammarResponse> GenerateCallback();

        /// <summary>
        ///   Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise" /> that contains mesh info and response message from server.</returns>
        public IPromise<GenerateGrammarResponse> Generate() => !CanGenerate
            ? Promise<GenerateGrammarResponse>.Rejected(new ApplicationException("Generate request error:\nThis object can't be generated right now."))
            : GenerateCallback()
                .Then(response => {
                    CreateMesh(response.ParseTree, response.Materials);
                    return response;
                });

        /// <summary>
        ///   Instantiates new GameObject at the pivot of node specified by id.
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

        private void CreateMesh(ParseTree parseTree, IReadOnlyDictionary<int, MaterialModel> materialsDictionary) {
            DeleteOldMeshes();

            ParseTreeData.ParseTree = parseTree;
            materials = materialsDictionary.Select(m => MeshUtilities.MaterialFromModel(m.Value)).ToArray();

            var nodes = ParseTree.GetMeshNodes();
            foreach (var node in nodes) {
                ParseTreeScript.Construct(transform, ParseTree, node, materials);
            }
        }

        private void DeleteOldMeshes() {
            for (var i = 0; i < transform.childCount; ++i) {
                var child = transform.GetChild(i);
                if (!child.GetComponent<ParseTreeScript>()) {
                    continue;
                }
                DestroyImmediate(child.gameObject);
                --i;
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
                Gizmos.DrawWireMesh(data.Mesh,
                    data.Position + transform.position,
                    data.Rotation * transform.rotation,
                    data.Scale + transform.localScale);
                Gizmos.color = new Color(0.97f, 0.58f, 0.11f, 0.3f);
                Gizmos.DrawMesh(data.Mesh,
                    data.Position + transform.position,
                    data.Rotation * transform.rotation,
                    data.Scale + transform.localScale);
            }
        }
    }
}
