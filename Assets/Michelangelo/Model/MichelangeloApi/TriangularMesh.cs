using System;
using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [Serializable]
    [MessagePackObject(true)]
    public class TriangularMesh {
        public bool Indexed;
        public int[] Indices;
        public double[] Points;
    }
}
