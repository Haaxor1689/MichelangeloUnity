using System;
using System.Collections.Generic;
using Michelangelo.Utility;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class ModelMesh {
        public Primitive[] primitives;

        public ModelMesh(Primitive[] primitives) {
            this.primitives = primitives;
        }

        public void Render(Transform transform) {
            if (primitives == null) {
                return;
            }
            var inverse = transform.worldToLocalMatrix.inverse;
            foreach (var item in primitives) {
                Graphics.DrawMesh(item.type.Mesh(), inverse * item.modelMatrix, new Material(Shader.Find("Diffuse")), 0);
            }
        }
    }
}