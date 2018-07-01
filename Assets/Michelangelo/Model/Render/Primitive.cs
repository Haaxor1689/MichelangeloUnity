using System;
using System.Reflection;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class Primitive {

        [SerializeField]
        private readonly int material;
        public int Material => material;

        [SerializeField]
        private readonly string primitiveType;
        public string PrimitiveType => primitiveType;

        [SerializeField]
        private readonly Matrix4x4 modelMatrix;
        public Matrix4x4 ModelMatrix => modelMatrix;

        [SerializeField]
        private readonly Mesh model;
        public Mesh Model => model;

        public Primitive(int material, string primitiveType, Matrix4x4 modelMatrix, Mesh model = null) {
            this.material = material;
            this.primitiveType = primitiveType;
            this.modelMatrix = modelMatrix;
            this.model = model;
        }

        public Mesh Mesh {
            get {
                if (Model != null) {
                    return Model;
                }

                var field = typeof(Utility.Primitives).GetField(PrimitiveType, BindingFlags.Static | BindingFlags.Public);
                if (field != null) {
                    return field.GetValue(null) as Mesh;
                }
                Debug.LogError("Unknown primitive type " + PrimitiveType);
                return new Mesh();
            }
        }
    }
}