using MessagePack;

namespace Michelangelo.Model.MsgSerialized {
    [MessagePackObject(true)]
    public class ParseTreeModel {
        //Human readable nme of the rule
        //public string RN = string.Empty;

        //Children nodes (uint.MaxValue if terminals)
        public GeometricModel[] G;

        //My id within the tree
        public uint ID = uint.MaxValue;

        public uint[] P;

        //Id of the rule which created this shape
        //public uint R = uint.MaxValue;
        public string R = string.Empty;

        public SemanticModel[] S;
    }
}
