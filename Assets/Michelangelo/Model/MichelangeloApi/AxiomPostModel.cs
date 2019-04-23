using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
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
