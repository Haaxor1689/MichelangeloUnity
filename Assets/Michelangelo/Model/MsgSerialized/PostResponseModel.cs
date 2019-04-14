using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Model.MsgSerialized {
    [MessagePackObject(true)]
    public class PostResponseModel {
        /// <summary>
        ///   Axioms exposed by this grammar
        /// </summary>
        public HashSet<string> A;

        //ID of the compilation in order to generte files on demand (like renderings, exports etc.)
        public string DL = null;

        /// <summary>
        ///   Errors at grammar compilation
        /// </summary>
        public string E;

        public SceneEnvironment ENV = null;

        public bool HasAnim = false;

        /// <summary>
        ///   Path traced image
        /// </summary>
        public string IMG;

        /// <summary>
        ///   Locks to Leafs
        /// </summary>
        public uint[] LL = null;

        /// <summary>
        ///   Locks to Root
        /// </summary>
        public uint[] LR = null;

        /// <summary>
        ///   Material Library - IDs and materials for the created model
        /// </summary>
        public Dictionary<int, string> ML;

        /// <summary>
        ///   List of generated objects
        /// </summary>
        public GeometricModel[] O;

        /// <summary>
        /// Parsed grammar code (by Roslyn)
        /// </summary>
        public string Parsed = "";

        /// <summary>
        /// Extra information (
        /// </summary>
        public string Info = "";

        public List<AxiomPostModel> PS = null;

        /// <summary>
        ///   Parse trees produced by this grammar
        /// </summary>
        public Dictionary<uint, ParseTreeModel> PT = null;

        // uint counter = 0;

        //Rules count for this grammar
        public int RC;

        /// <summary>
        ///   Dictionary with infos about the applied rules
        /// </summary>
        public Dictionary<string, RuleExtraInfo> RS = null;
    }
}
