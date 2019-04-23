using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [MessagePackObject(true)]
    public class TriangularMesh {
        public bool Indexed;
        public int[] Indices;
        public double[] Points;
    }
}
