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

        protected abstract IPromise<GenerateGrammarResponse> GenerateCallback();
        
        /// <summary>
        /// Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise"/> that contains mesh info and response message from server.</returns>
        public IPromise<GenerateGrammarResponse> Generate() {
            return GenerateCallback().Then(response => CreateMesh(response.ParseTree, response.Materials));
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
