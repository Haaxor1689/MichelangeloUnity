using System.Collections.Generic;
using Michelangelo.Model.MichelangeloApi;

namespace Michelangelo.Model {
    public class GenerateGrammarResponse {
        public ParseTree ParseTree;
        public Dictionary<int, MaterialModel> Materials;
        public string ErrorMessage;
    }
}
