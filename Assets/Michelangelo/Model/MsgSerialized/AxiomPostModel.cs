using MessagePack;

namespace Michelangelo.Model.MsgSerialized {
    [MessagePackObject(true)]
    public class AxiomPostModel {
        /// <summary>
        ///   Axiom ID
        /// </summary>
        public uint A;

        /// <summary>
        ///   Parse Tree Pointer
        /// </summary>
        public uint P;
    }
}
