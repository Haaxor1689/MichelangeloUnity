using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Michelangelo.Model.MsgSerialized;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class ModelMesh {
        private static readonly Material DefaultMaterial = new Material(Shader.Find("Diffuse"));
        
        public ModelMesh(PostResponseModel response) {
            Primitives = response.O.Select(o => new Primitive(o)).ToArray();
            Materials = response.ML.Select(ml => new Material(DefaultMaterial) { color = ParseColor(ml.Value) }).ToArray();
        }
        
        public Primitive[] Primitives;
        public Material[] Materials;
        
        private static Color ParseColor(string color) {
            var groups = new Regex(@"\[(?<r>[\d\.]+), (?<g>[\d\.]+), (?<b>[\d\.]+)\]").Match(color).Groups;
            float r, g, b;
            if (!float.TryParse(groups["r"].Value, out r) ||
                !float.TryParse(groups["g"].Value, out g) ||
                !float.TryParse(groups["b"].Value, out b)) {
                return new Color(1, 1, 1);
            }
            return new Color(r, g, b);
        }
    }
}
