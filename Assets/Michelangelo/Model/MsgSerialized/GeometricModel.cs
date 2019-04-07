using MessagePack;

namespace Michelangelo.Model.MsgSerialized {
    [MessagePackObject(true)]
    public class GeometricModel {
        /// <summary>
        ///   Geometry type of the object
        /// </summary>
        public string G;

        /// <summary>
        ///   Material of the object (JSON)
        /// </summary>
        public int M;

        /// <summary>
        ///   Node ID for the
        /// </summary>
        public uint N;

        /// <summary>
        ///   Transformation matrix of the object (JSON)
        /// </summary>
        public float[] T;

        /// <summary>
        ///   Mesh of the object
        /// </summary>
        public TriangularMesh V;
    }
}
