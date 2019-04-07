using MessagePack;

namespace Michelangelo.Model.MsgSerialized {
    [MessagePackObject(true)]
    public class RuleExtraInfo {
        public uint CallsCount = 0;

        public string[] FulfillsAttributes;

        public string[] FulfillsGoals;

        public bool Local;
        public string RUID;

        public string TypeStr;
    }
}
