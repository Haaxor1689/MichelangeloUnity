using System;
using System.Reflection;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class Primitive {

        [SerializeField]
        private int _material;
        public int material { get { return _material; } }

        [SerializeField]
        private string _primitiveType;
        public string primitiveType { get { return _primitiveType; } }

        [SerializeField]
        private Matrix4x4 _modelMatrix;
        public Matrix4x4 modelMatrix { get { return _modelMatrix; } }

        [SerializeField]
        private Mesh _model;
        public Mesh model { get { return _model; } }

        public Primitive(int material, string primitiveType, Matrix4x4 modelMatrix, Mesh model = null) {
            _material = material;
            _primitiveType = primitiveType;
            _modelMatrix = modelMatrix;
            _model = model;
        }

        public Mesh mesh {
            get {
                if (model != null) {
                    return model;
                }

                var field = typeof(Utility.Primitives).GetField(primitiveType, BindingFlags.Static | BindingFlags.Public);
                if (field != null) {
                    return field.GetValue(null) as Mesh;
                }
                Debug.LogError("Unknown primitive type " + primitiveType);
                return new Mesh();
            }
        }
    }
}