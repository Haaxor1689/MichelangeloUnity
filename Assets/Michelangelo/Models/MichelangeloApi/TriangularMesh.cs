using System;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    /// Raw vertex data of a mesh.
    /// </summary>
    [Serializable]
    [MessagePackObject(true)]
    public class TriangularMesh {
        /// <summary>
        /// True if mesh is indexed.
        /// </summary>
        public bool Indexed;

        /// <summary>
        /// Indices of a mesh.
        /// </summary>
        public int[] Indices;

        /// <summary>
        /// Vertices of a mesh.
        /// </summary>
        public double[] Points;
    }
}
