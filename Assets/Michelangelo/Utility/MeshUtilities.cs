using System.Collections.Generic;
using System.Reflection;
using Michelangelo.Model;
using Michelangelo.Model.MichelangeloApi;
using UnityEngine;

namespace Michelangelo.Utility {
    public static class MeshUtilities {
        public static Matrix4x4 MatrixFromArray(float[] arr) {
            return new Matrix4x4(
                new Vector4(arr[0], arr[4], arr[8], arr[12]),
                new Vector4(arr[1], arr[5], arr[9], arr[13]),
                new Vector4(arr[2], arr[6], arr[10], arr[14]),
                new Vector4(arr[3], arr[7], arr[11], arr[15])
            );
        }

        public static Mesh MeshFromGeometricModel(GeometricModel model) => MeshFromData(model.Mesh) ?? MeshFromPrimitive(model.Primitive);

        public static Mesh MeshFromData(TriangularMesh v) {
            if (v == null || v.Points.Length == 0) {
                return null;
            }
            var mesh = new Mesh();

            var vertices = new List<Vector3>();
            for (var i = 0; i < v.Points.Length; i += 3) {
                vertices.Add(new Vector3((float)v.Points[i], (float)v.Points[i + 1], (float)v.Points[i + 2]));
            }
            mesh.vertices = vertices.ToArray();
            mesh.triangles = v.Indices;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        public static Mesh MeshFromPrimitive(string primitive) {
            var field = typeof(Primitives).GetField(primitive, BindingFlags.Static | BindingFlags.Public);
            if (field != null) {
                return field.GetValue(null) as Mesh;
            }
            Debug.LogError("Unknown primitive type " + primitive);
            return new Mesh();
        }

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
