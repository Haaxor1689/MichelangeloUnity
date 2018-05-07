using System;
using System.Collections.Generic;
using Michelangelo.Utility;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class ModelMesh {
        public Color[] colors;
        public Primitive[] primitives;

        [NonSerialized]
        private static Material defaultMaterial = new Material(Shader.Find("Diffuse"));
        [NonSerialized]
        private Material[] _materials;
        public Material[] materials {
            get {
                if (_materials == null) {
                    _materials = new Material[colors.Length];
                    for (int i = 0; i < colors.Length; ++i) {
                        _materials[i] = new Material(defaultMaterial);
                        _materials[i].color = colors[i];
                    }
                }
                return _materials;
            }
        }

        public ModelMesh(Primitive[] primitives, Color[] colors) {
            this.primitives = primitives;
            this.colors = colors;
        }

        public void Render(Transform transform) {
            if (primitives == null) {
                return;
            }

            var inverse = transform.worldToLocalMatrix.inverse;
            foreach (var item in primitives) {
                Graphics.DrawMesh(item.mesh, inverse * item.modelMatrix, materials[item.material], 0);
            }
        }
    }
}