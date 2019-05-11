using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    [MessagePackObject(true)]
    public class PostResponseModel {
        /// <summary>
        /// Material Library - IDs and materials for the created model
        /// </summary>
        public Dictionary<int, MaterialModel> Materials;

        /// <summary>
        /// List of generated objects
        /// </summary>
        public GeometricModel[] Objects;

        /// <summary>
        /// Errors at grammar compilation
        /// </summary>
        public string Errors;

        /// <summary>
        /// Path traced image
        /// </summary>
        public string IMG;

        /// <summary>
        /// Parsed grammar code (by Roslyn)
        /// </summary>
        public string Parsed = "";

        /// <summary>
        /// Extra information (
        /// </summary>
        public string Info = "";

        /// <summary>
        /// Parse trees produced by this grammar
        /// </summary>
        public Dictionary<uint, ParseTreeModel> ParseTree = null;

        /// <summary>
        /// Dictionary with infos about the applied rules
        /// </summary>
        public Dictionary<string, RuleExtraInfo> Rules = null;

        /// <summary>
        /// Locks to Root
        /// </summary>
        public uint[] RootLocks = null;

        /// <summary>
        /// Locks to Leafs
        /// </summary>
        public uint[] LeafLocks = null;

        public SceneEnvironment ENV = null;

        public List<AxiomPostModel> Models = null;

        /// <summary>
        /// ID of the compilation in order to generate files on demand (like renderings, exports etc.)
        /// </summary>
        public string DL = null;

        /// <summary>
        /// Does this parsing result contain an animation?
        /// </summary>
        public bool HasAnim = false;
    }
}
