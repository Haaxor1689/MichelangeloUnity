using System.Collections.Generic;
using System.Reflection;
using Michelangelo.Models.MichelangeloApi;
using UnityEngine;

namespace Michelangelo.Utility {
    internal static class MeshUtilities {
        internal static Matrix4x4 MatrixFromArray(IReadOnlyList<float> arr) {
            return new Matrix4x4(
                new Vector4(arr[0], arr[4], arr[8], arr[12]),
                new Vector4(arr[1], arr[5], arr[9], arr[13]),
                new Vector4(arr[2], arr[6], arr[10], arr[14]),
                new Vector4(arr[3], arr[7], arr[11], arr[15])
            );
        }

        internal static Mesh MeshFromGeometricModel(GeometricModel model) => MeshFromData(model.Mesh) ?? MeshFromPrimitive(model.Primitive);

        private static Mesh MeshFromData(TriangularMesh v) {
            if (v == null || v.Points == null || v.Points.Length == 0) {
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

        private static Mesh MeshFromPrimitive(string primitive) {
            var field = typeof(Primitives).GetField(primitive, BindingFlags.Static | BindingFlags.Public);
            if (field != null) {
                return field.GetValue(null) as Mesh;
            }
            Debug.LogError("Unknown primitive type " + primitive);
            return new Mesh();
        }

        internal static Material MaterialFromModel(MaterialModel model) {
            var material = new Material(Shader.Find("Standard"));
            material.SetColor("_Color", new Color((float) model.Albedo[0], (float) model.Albedo[1], (float) model.Albedo[2]));
            material.SetFloat("_Metallic", (float) model.Scalars.GetValueOrDefault("gIi", 0.0));
            material.SetFloat("_Glossiness", 1.0f - (float) model.Scalars.GetValueOrDefault("gR", 1.0));

            return material;
        }
    }
}
