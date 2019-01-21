using UnityEngine;

namespace Michelangelo.Utility {
    public static class MeshUtilities {
        public static void ToFlatShaded(Mesh mesh) {
            var oldVertices = mesh.vertices;
            var triangles = mesh.triangles;
            var vertices = new Vector3[triangles.Length];
            for (var i = 0; i < triangles.Length; i++) {
                vertices[i] = oldVertices[triangles[i]];
                triangles[i] = i;
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }
    }
}
