using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    [MessagePackObject(true)]
    public class MaterialModel {
        public double[] Albedo;
        public Dictionary<string, double> Scalars;
        public Dictionary<string, double[]> Vectors;
    }
}
