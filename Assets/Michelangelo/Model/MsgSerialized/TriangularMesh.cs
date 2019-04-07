using MessagePack;

namespace Michelangelo.Model.MsgSerialized {
    [MessagePackObject(true)]
    public class TriangularMesh {
        public bool Indexed;
        public int[] Indices;
        public double[] Points;
    }
}
