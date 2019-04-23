using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [MessagePackObject(true)]
    public class SemanticModel {
        /// <summary>
        ///   Attributes
        /// </summary>
        public Dictionary<string, string> At;

        /// <summary>
        ///   Goals
        /// </summary>
        public Dictionary<string, string> Go;

        /// <summary>
        ///   Ontology
        /// </summary>
        public IList<string> On;

        /// <summary>
        ///   Tags
        /// </summary>
        public string[] Ta;
    }
}
