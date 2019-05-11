using System;
using System.Collections.Generic;
using MessagePack;
using Michelangelo.Utility;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    /// Geometric model containing vertices, material and transform matrix.
    /// </summary>
    [Serializable]
    [MessagePackObject(true)]
    public class GeometricModel {
        /// <summary>
        /// Geometry type of the object.
        /// </summary>
        public readonly string Primitive;

        /// <summary>
        /// Material id of the object.
        /// </summary>
        public readonly int MaterialID;

        /// <summary>
        /// Transformation matrix of the object.
        /// </summary>
        public readonly IReadOnlyList<float> Transform;

        /// <summary>
        /// Raw mesh of the object.
        /// </summary>
        public readonly TriangularMesh Mesh;

        /// <inheritdoc />
        public GeometricModel(string primitive, int materialId, IReadOnlyList<float> transform, TriangularMesh mesh) {
            Primitive = primitive;
            MaterialID = materialId;
            Transform = transform;
            Mesh = mesh;
        }

        internal uint GetVertexCount() => Mesh != null ? (uint) Mesh.Indices.Length : Primitives.GetVertexCount(Primitive);
    }
}
