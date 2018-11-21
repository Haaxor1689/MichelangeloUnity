using Michelangelo.Model.Render;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Scripts {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MichelangeloMesh : UnityEngine.MonoBehaviour {

        public const string MichelangeloMeshObjectName = "MichelangeloPartialMesh";
        
        private MeshFilter meshFilter => GetComponent<MeshFilter>();
        private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
        private MichelangeloObject parentObject => transform.parent.GetComponent<MichelangeloObject>();

        private void CreateMesh(Primitive primitive, Material material) {
            // Transform
            transform.localScale = primitive.ModelMatrix.ExtractScale();
            transform.localRotation = primitive.ModelMatrix.ExtractRotation();
            transform.localPosition = primitive.ModelMatrix.ExtractPosition();

            // Mesh
            meshFilter.sharedMesh = primitive.Mesh;
            
            // Material
            meshRenderer.material = material;
        }

        private void OnDrawGizmosSelected() {
            if (Selection.activeGameObject == gameObject) {
                parentObject.SetSelection();
            }
        }

        public static GameObject Construct(Transform parent, Primitive primitive, Material material) {
            var newObject = new GameObject(MichelangeloMesh.MichelangeloMeshObjectName);
            newObject.hideFlags = HideFlags.NotEditable;
            newObject.transform.SetParent(parent);
            var michelangeloMesh = newObject.AddComponent<MichelangeloMesh>();
            michelangeloMesh.CreateMesh(primitive, material);
            return newObject;
        }
    }
}