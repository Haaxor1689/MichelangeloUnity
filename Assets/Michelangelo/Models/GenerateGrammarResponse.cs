using System.Collections.Generic;
using Michelangelo.Models.MichelangeloApi;

namespace Michelangelo.Models {
    /// <summary>
    ///   Response from a mesh generation request to Michelangelo service.
    /// </summary>
    public class GenerateGrammarResponse {
        /// <summary>
        ///   Contains output from grammar compilation on Michelangelo backend.
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        ///   Dictionary of all materials used by the resulting meshes.
        /// </summary>
        public Dictionary<int, MaterialModel> Materials;

        /// <summary>
        ///   Parse tree containing all the information about how the result was generated and the raw mesh data.
        /// </summary>
        public ParseTree ParseTree;
    }
}
