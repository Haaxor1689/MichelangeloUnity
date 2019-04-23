using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Model.MichelangeloApi;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    public abstract class ObjectBase : MonoBehaviour, IObjectBase {
        [SerializeField]
        protected bool isInEditMode;

        public bool IsInEditMode => isInEditMode;
        
        [SerializeField]
        protected ParseTree ParseTree;

        [SerializeField]
        protected Material[] Materials;

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
        public bool IsFlatShaded;

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
            return new Material(Shader.Find("Diffuse")){ color = new Color((float)model.Albedo[0], (float)model.Albedo[1], (float)model.Albedo[2]) };
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
