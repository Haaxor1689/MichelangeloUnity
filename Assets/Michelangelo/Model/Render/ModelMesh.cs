using System;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class ModelMesh {
        [NonSerialized]
        private static readonly Material DefaultMaterial = new Material(Shader.Find("Diffuse"));

        public readonly Color[] Colors;
        public readonly Primitive[] Primitives;

        [NonSerialized]
        private Material[] materials;

        public ModelMesh(Primitive[] primitives, Color[] colors) {
            Primitives = primitives;
            Colors = colors;
        }

        public Material[] Materials {
            get {
                if (materials != null) {
                    return materials;
                }

                materials = new Material[Colors.Length];
                for (var i = 0; i < Colors.Length; ++i) {
                    materials[i] = new Material(DefaultMaterial) { color = Colors[i] };
                }
                return materials;
            }
        }

        public void Render(Transform transform) {
            if (Primitives == null) {
                return;
            }

            var inverse = transform.worldToLocalMatrix.inverse;
            foreach (var item in Primitives) {
                Graphics.DrawMesh(item.Mesh, inverse * item.ModelMatrix, Materials[item.Material], 0);
            }
        }
    }
}
