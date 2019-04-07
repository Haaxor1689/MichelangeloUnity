using System;
using System.Collections.Generic;
using System.Reflection;
using Michelangelo.Model.MsgSerialized;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class Primitive {
        
        [SerializeField]
        public int Material;

        [SerializeField]
        public string PrimitiveType;

        [SerializeField]
        private float[] matrix;

        private Matrix4x4? modelMatrix;
        public Matrix4x4 ModelMatrix => modelMatrix ?? (modelMatrix = MatrixFromArray(matrix)).Value;

        [SerializeField]
        private TriangularMesh rawModel;

        private Mesh model;
        private Mesh Model => model != null ? model : (model = MeshFromData(rawModel));

        public Mesh Mesh => Model != null ? Model : MeshFromPrimitive(PrimitiveType);

        public Primitive(GeometricModel data) {
            Material = data.M;
            PrimitiveType = data.G;
            matrix = data.T;
            rawModel = data.V;
        }

        private static Matrix4x4 MatrixFromArray(float[] arr) {
            return new Matrix4x4(
                new Vector4(arr[0], arr[4], arr[8], arr[12]),
                new Vector4(arr[1], arr[5], arr[9], arr[13]),
                new Vector4(arr[2], arr[6], arr[10], arr[14]),
                new Vector4(arr[3], arr[7], arr[11], arr[15])
            );
        }

        private static Mesh MeshFromData(TriangularMesh v) {
            if (v == null) {
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
    }
}