using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Michelangelo.Utility;
using SimpleJSON;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class ModelMesh {
        private static readonly Material DefaultMaterial = new Material(Shader.Find("Diffuse"));

        [SerializeField] private string jsonSource;
        
        public ModelMesh(string rawJson) {
            jsonSource = rawJson;
            
            if (string.IsNullOrEmpty(jsonSource)) {
                return;
            }
            var json = JSON.Parse(jsonSource);
            primitives = PrimitivesFromJSON(json);
            materials = MaterialsFromJSON(json);
        }

        private Primitive[] primitives;
        public Primitive[] Primitives => primitives ?? (primitives = string.IsNullOrEmpty(jsonSource) ? new Primitive[] { } : PrimitivesFromJSON(JSON.Parse(jsonSource)));
        private Material[] materials;
        public Material[] Materials => materials ?? (materials = string.IsNullOrEmpty(jsonSource) ? new Material[] { } : MaterialsFromJSON(JSON.Parse(jsonSource)));

        public void Render(Transform transform) {
            if (string.IsNullOrEmpty(jsonSource)) {
                return;
            }

            var inverse = transform.worldToLocalMatrix.inverse;
            foreach (var item in Primitives) {
                Graphics.DrawMesh(item.Mesh, inverse * item.ModelMatrix, Materials[item.Material], 0);
            }
        }

        #region Generated model helpers
        private static Primitive[] PrimitivesFromJSON(JSONNode json) {

            var primitives = new List<Primitive>();
            foreach (var obj in json["o"]) {
                var type = obj.Value["g"].Value;
                primitives.Add(new Primitive(obj.Value["m"].AsInt,
                    type,
                    MatrixFromJSON(obj.Value["t"]),
                    MeshFromJSON(obj.Value)));
            }
            return primitives.ToArray();
        }

        private static Material[] MaterialsFromJSON(JSONNode json) {
            var materials = new List<Material>();
            foreach (var obj in json["ml"]) {
                var groups = new Regex(@"\[(?<r>[\d\.]+), (?<g>[\d\.]+), (?<b>[\d\.]+)\]").Match(obj.Value.Value).Groups;
                float r, g, b;
                if (!float.TryParse(groups["r"].Value, out r) ||
                    !float.TryParse(groups["g"].Value, out g) ||
                    !float.TryParse(groups["b"].Value, out b)) {
                    materials.Add(new Material(DefaultMaterial) { color = new Color(1, 1, 1) });
                } else {
                    materials.Add(new Material(DefaultMaterial) { color = new Color(r, g, b) });
                }
            }
            return materials.ToArray();
        }

        private static Matrix4x4 MatrixFromJSON(JSONNode json) {
            var matrix = new Matrix4x4(
                new Vector4(json[0], json[4], json[8], json[12]),
                new Vector4(json[1], json[5], json[9], json[13]),
                new Vector4(json[2], json[6], json[10], json[14]),
                new Vector4(json[3], json[7], json[11], json[15])
                );

            if (matrix.HasNegativeScale()) {
                var scaleMatrix = Matrix4x4.identity;
                scaleMatrix.m00 = -1;
                matrix *= scaleMatrix;
            }
            return matrix;
        }

        private static Mesh MeshFromJSON(JSONNode json) {
            JSONNode v;
            if ((v = json["v"]) == null) {
                return null;
            }
            var mesh = new Mesh();

            var vertices = new List<Vector3>();
            var iter = v["points"].Values;
            while (iter.MoveNext()) {
                var x = iter.Current.AsFloat;
                iter.MoveNext();
                var y = iter.Current.AsFloat;
                iter.MoveNext();
                var z = iter.Current.AsFloat;
                vertices.Add(new Vector3(x, y, z));
            }
            mesh.vertices = vertices.ToArray();

            var triangles = new List<int>();
            foreach (var i in v["indices"].Values) {
                triangles.Add(i.AsInt);
            }
            mesh.triangles = triangles.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
        #endregion
    }
}
