using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    [MessagePackObject(true)]
    public class AxiomPostModel {
        /// <summary>
        /// Axiom ID
        /// </summary>
        public uint AxiomID;

        /// <summary>
        /// Parse Tree Pointer
        /// </summary>
        public uint ParseTreeIndex;
    }
}
