using System;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class Primitive {
        public PrimitiveType type;
        public Matrix4x4 modelMatrix;
    }
}