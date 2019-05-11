using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
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
