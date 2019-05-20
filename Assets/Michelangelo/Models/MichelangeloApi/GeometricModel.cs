using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MessagePack;
using Michelangelo.Utility;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    ///   Geometric model containing vertices, material and transform matrix.
    /// </summary>
    [Serializable]
    [MessagePackObject(true)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GeometricModel {
        /// <summary>
        ///   Material id of the object.
        /// </summary>
        public int MaterialID;

        /// <summary>
        ///   Raw mesh of the object.
        /// </summary>
        public TriangularMesh Mesh;

        /// <summary>
        ///   Geometry type of the object.
        /// </summary>
        public string Primitive;

        /// <summary>
        ///   Transformation matrix of the object.
        /// </summary>
        public float[] Transform;

        internal uint GetVertexCount() => Mesh != null ? (uint) Mesh.Indices.Length : Primitives.GetVertexCount(Primitive);
    }
}
