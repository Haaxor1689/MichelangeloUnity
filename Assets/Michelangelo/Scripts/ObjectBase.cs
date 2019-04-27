using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Model.MichelangeloApi;
using Michelangelo.Utility;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    public abstract class ObjectBase : MonoBehaviour, IObjectBase {
        public bool IsFlatShaded;

        [SerializeField]
        protected bool isInEditMode;

        [SerializeField]
        protected Material[] Materials;

        [SerializeField]
        protected ParseTree ParseTree;

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

        public bool IsInEditMode => isInEditMode;

        public abstract IPromise<GenerateGrammarResponse> Generate();

        protected void CreateMesh(ParseTree parseTree, IDictionary<int, MaterialModel> materials) {
            DeleteOldMeshes();

            ParseTree = parseTree;
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
            IsFlatShaded = false;
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

        public void ToFlatShaded() {
            // for (var i = 0; i < transform.childCount; ++i) {
            //     var child = transform.GetChild(i);
            //     if (child.name == Element.ObjectName) {
            //         MeshUtilities.ToFlatShaded(child.GetComponent<MeshFilter>().sharedMesh);
            //     }
            // }
            // MeshUtilities.ToFlatShaded(MeshFilter.sharedMesh);
            IsFlatShaded = true;
        }

        // public void BakeMesh() {
        //     var bakery = gameObject.AddComponent<MeshBakery>();
        //     bakery.m_BatchIngredients = new List<BatchIngredient>();
        //     foreach (Transform t in transform) {
        //         bakery.m_BatchIngredients.Add(new BatchIngredient(t.gameObject.GetComponent<MeshFilter>().sharedMesh, t.gameObject.GetComponent<MeshRenderer>().sharedMaterial));
        //     }
        //     bakery.Init();
        // }
    }
}
