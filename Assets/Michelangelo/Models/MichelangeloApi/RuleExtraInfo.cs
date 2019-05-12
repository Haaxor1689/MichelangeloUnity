using System.Diagnostics.CodeAnalysis;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    /// Info about a rule.
    /// </summary>
    [MessagePackObject(true)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RuleExtraInfo {
        /// <summary>
        /// 
        /// </summary>
        public uint CallsCount = 0;

        /// <summary>
        /// 
        /// </summary>
        public string[] FulfillsAttributes;

        /// <summary>
        /// 
        /// </summary>
        public string[] FulfillsGoals;

        /// <summary>
        /// 
        /// </summary>
        public bool Local;

        /// <summary>
        /// 
        /// </summary>
        public string RUID;

        /// <summary>
        /// 
        /// </summary>
        public string TypeStr;
    }
}
