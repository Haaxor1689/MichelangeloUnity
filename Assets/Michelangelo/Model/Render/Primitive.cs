using System;
using System.Reflection;
using UnityEngine;

namespace Michelangelo.Model.Render {
    [Serializable]
    public class Primitive {
        
        public int Material;
        public string PrimitiveType;
        public Matrix4x4 ModelMatrix;
        public Mesh Model;

        public Primitive(int material, string primitiveType, Matrix4x4 modelMatrix, Mesh model = null) {
            Material = material;
            PrimitiveType = primitiveType;
            ModelMatrix = modelMatrix;
            Model = model;
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