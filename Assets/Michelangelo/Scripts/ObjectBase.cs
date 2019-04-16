using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Model.Render;
using Michelangelo.Utility;
using Overtop.Utility;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class ObjectBase : MonoBehaviour, IObjectBase {
        [SerializeField]
        protected bool isInEditMode;

        public bool IsInEditMode => isInEditMode;

        private Mesh objectMesh;

        [SerializeField]
        protected ModelMesh modelMesh;

        // public bool HasMesh {
        //     get {
        //         for (var i = 0; i < transform.childCount; ++i) {
        //             if (transform.GetChild(i).name == Element.ObjectName) {
        //                 return true;
        //             }
        //         }
        //         return false;
        //     }
        // }
        public bool HasMesh => GetComponent<MeshFilter>().sharedMesh != null;
        public bool IsFlatShaded;

        protected MeshFilter MeshFilter => GetComponent<MeshFilter>();
        protected MeshRenderer MeshRenderer => GetComponent<MeshRenderer>();

        private void Awake() {
            MeshFilter.hideFlags = HideFlags.HideInHierarchy;
            MeshRenderer.hideFlags = HideFlags.HideInHierarchy;
        }

        public abstract IPromise<GenerateGrammarResponse> Generate();

        protected void CreateMesh(ModelMesh model) {
            modelMesh = model;
            IsFlatShaded = false;
            // DeleteOldMeshes();
            // foreach (var primitive in modelMesh.Primitives) {
            //     Element.Construct(transform, primitive, modelMesh.Materials[primitive.Material]);
            // }
            var submeshes = new List<Tuple<Mesh, int>>();
            foreach (var pair in modelMesh.Primitives.GroupBy(p => p.Material, p => p, (key, p) => new { Material = key, Primitive = p })) {
                var mesh = new Mesh();
                var combine = new List<CombineInstance>();
                foreach (var p in pair.Primitive) {
                    combine.Add(new CombineInstance { mesh = p.Mesh, transform = p.ModelMatrix });
                }
                mesh.CombineMeshes(combine.ToArray());
                submeshes.Add(Tuple.Create(mesh, pair.Material));
            }
            objectMesh = new Mesh();
            var finalCombine = new List<CombineInstance>();
            var materials = new List<Material>();
            foreach (var m in submeshes) {
                finalCombine.Add(new CombineInstance { mesh = m.Item1, transform = Matrix4x4.identity });
                materials.Add(modelMesh.Materials[m.Item2]);
            }
            objectMesh.CombineMeshes(finalCombine.ToArray(), false);
            MeshFilter.sharedMesh = objectMesh;
            MeshRenderer.sharedMaterials = materials.ToArray();
        }

        // private void DeleteOldMeshes() {
        //     for (var i = 0; i < transform.childCount; ++i) {
        //         var child = transform.GetChild(i);
        //         if (child.name == Element.ObjectName) {
        //             DestroyImmediate(child.gameObject);
        //             --i;
        //         }
        //     }
        //     IsFlatShaded = false;
        // }

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
