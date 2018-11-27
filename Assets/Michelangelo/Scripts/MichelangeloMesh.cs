using Michelangelo.Model.Render;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Scripts {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MichelangeloMesh : MonoBehaviour {

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
            if (!parentObject.IsInEditMode) {
                if (Selection.activeGameObject == gameObject) {
                    Selection.objects = new Object[] { parentObject.gameObject };
                }
                return;
            }

            if (Selection.activeGameObject == parentObject.gameObject) {
                return;
            }

            // Draw vertices and normals
            for (var i = 0; i < meshFilter.sharedMesh.vertices.Length; i++) {
                var sharedMeshVertex = meshFilter.sharedMesh.vertices[i];
                sharedMeshVertex.Scale(transform.localScale);
                sharedMeshVertex = transform.localRotation * sharedMeshVertex;
                sharedMeshVertex += transform.position;
                
                Gizmos.color = Color.green;
                Gizmos.DrawLine(sharedMeshVertex, sharedMeshVertex + transform.localRotation * meshFilter.sharedMesh.normals[i] * 0.2f);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(sharedMeshVertex, 0.02f);
            }
        }

        public static GameObject Construct(Transform parent, Primitive primitive, Material material) {
            var newObject = new GameObject(MichelangeloMeshObjectName);
            newObject.hideFlags = HideFlags.NotEditable;
            newObject.transform.SetParent(parent);
            var michelangeloMesh = newObject.AddComponent<MichelangeloMesh>();
            newObject.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;
            newObject.GetComponent<MeshRenderer>().hideFlags = HideFlags.HideInInspector;
            michelangeloMesh.CreateMesh(primitive, material);
            return newObject;
        }
    }
}