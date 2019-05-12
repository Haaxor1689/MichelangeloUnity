using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    ///   Michelangelo's material properties.
    /// </summary>
    [MessagePackObject(true)]
    public class MaterialModel {
        /// <summary>
        ///   RGBA value of base material color.
        /// </summary>
        public readonly IReadOnlyList<double> Albedo;

        /// <summary>
        ///   Dictionary of all scalar properties for material shader.
        /// </summary>
        public readonly IReadOnlyDictionary<string, double> Scalars;

        /// <summary>
        ///   Dictionary of all vector properties for material shader.
        /// </summary>
        public readonly IReadOnlyDictionary<string, double[]> Vectors;

        /// <summary>
        ///   Default constructor that initializes all class fields.
        /// </summary>
        public MaterialModel(IReadOnlyList<double> albedo, IReadOnlyDictionary<string, double> scalars, IReadOnlyDictionary<string, double[]> vectors) {
            Albedo = albedo;
            Scalars = scalars ?? new Dictionary<string, double>();
            Vectors = vectors ?? new Dictionary<string, double[]>();
        }
    }
}
