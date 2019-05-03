using System;
using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [Serializable]
    [MessagePackObject(true)]
    public class GeometricModel {
        /// <summary>
        /// Geometry type of the object
        /// </summary>
        public string Primitive;

        /// <summary>
        /// Material of the object (JSON)
        /// </summary>
        public int MaterialID;

        /// <summary>
        /// Node ID for the
        /// </summary>
        public uint NodeID;

        /// <summary>
        /// Transformation matrix of the object (JSON)
        /// </summary>
        public float[] Transform;

        /// <summary>
        /// Mesh of the object
        /// </summary>
        public TriangularMesh Mesh;

        public uint GetVertexCount() => Mesh != null ? (uint) Mesh.Indices.Length : Primitives.GetVertexCount(Primitive);
    }
}
