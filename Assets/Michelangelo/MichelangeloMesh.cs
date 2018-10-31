using Michelangelo.Model.Render;
using Michelangelo.Utility;
using UnityEngine;

namespace Michelangelo {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MichelangeloMesh : MonoBehaviour {

        public const string MichelangeloMeshObjectName = "MichelangeloPartialMesh";
        
        private MeshFilter meshFilter => GetComponent<MeshFilter>();
        private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();

        public void CreateMesh(Primitive primitive, Material material) {
            // Transform
            transform.localScale = primitive.ModelMatrix.ExtractScale();
            transform.rotation = primitive.ModelMatrix.ExtractRotation();
            transform.position = primitive.ModelMatrix.ExtractPosition();

            // Mesh
            meshFilter.sharedMesh = primitive.Mesh;
            
            // Material
            meshRenderer.material = material;
        }
    }
}