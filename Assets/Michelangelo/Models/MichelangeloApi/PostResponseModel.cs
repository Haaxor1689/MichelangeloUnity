using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    ///   Backend model of generate request response.
    /// </summary>
    [MessagePackObject(true)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PostResponseModel {
        /// <summary>
        ///   Errors from grammar compilation.
        /// </summary>
        public readonly string Errors;

        /// <summary>
        ///   Path traced image.
        /// </summary>
        public readonly string IMG;

        /// <summary>
        ///   Extra information.
        /// </summary>
        public readonly string Info;

        /// <summary>
        ///   Material Library containing IDs and materials for the created model.
        /// </summary>
        public readonly IReadOnlyDictionary<int, MaterialModel> Materials;

        /// <summary>
        ///   Parse trees produced by this grammar.
        /// </summary>
        public readonly IReadOnlyDictionary<uint, ParseTreeModel> ParseTree;

        /// <summary>
        ///   Dictionary with infos about the applied rules.
        /// </summary>
        public readonly IReadOnlyDictionary<string, RuleExtraInfo> Rules;

        /// <summary>
        ///   Default constructor that initializes all class fields.
        /// </summary>
        public PostResponseModel(IReadOnlyDictionary<string, RuleExtraInfo> rules, IReadOnlyDictionary<uint, ParseTreeModel> parseTree, IReadOnlyDictionary<int, MaterialModel> materials, string info, string img, string errors) {
            Rules = rules;
            ParseTree = parseTree;
            Materials = materials;
            Info = info;
            IMG = img;
            Errors = errors;
        }
    }
}
