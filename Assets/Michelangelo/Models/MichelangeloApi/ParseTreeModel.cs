using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Models.MichelangeloApi {
    /// <summary>
    ///   Backed model of parse tree node.
    /// </summary>
    [MessagePackObject(true)]
    public class ParseTreeModel {
        /// <summary>
        ///   Child nodes.
        /// </summary>
        public readonly uint[] ChildIndices;

        /// <summary>
        ///   Node`s id within parse tree.
        /// </summary>
        public readonly uint ID;

        /// <summary>
        ///   Ontology of child nodes.
        /// </summary>
        public readonly IReadOnlyList<string[]> Ontology;

        /// <summary>
        ///   Id of the rule which created this shape.
        /// </summary>
        public readonly string Rule;

        /// <summary>
        ///   Geometric models of child nodes.
        /// </summary>
        public readonly IReadOnlyList<GeometricModel> Shape;

        /// <inheritdoc />
        public ParseTreeModel(uint id, string rule, uint[] childIndices, IReadOnlyList<string[]> ontology, IReadOnlyList<GeometricModel> shape) {
            ID = id;
            Rule = rule;
            ChildIndices = childIndices;
            Ontology = ontology;
            Shape = shape;
        }
    }
}
