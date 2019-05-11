using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Models.MichelangeloApi;

namespace Michelangelo.Models {
    /// <summary>
    /// Node of a parse tree.
    /// </summary>
    [Serializable]
    public class ParseTreeNode {
        private const uint MeshVertexCountLimit = 65534u;

        /// <summary>
        /// Id unique in current parse tree.
        /// </summary>
        public uint Id = uint.MaxValue;

        /// <summary>
        /// Id of a rule that generated this node.
        /// </summary>
        public string Rule = string.Empty;

        /// <summary>
        /// Id of parent node.
        /// </summary>
        public uint Parent = uint.MaxValue;

        /// <summary>
        /// Ontology chain that this node went through. 
        /// </summary>
        public string[] Ontology;

        /// <summary>
        /// Raw mesh data of node.
        /// </summary>
        public GeometricModel Shape;

        /// <summary>
        /// Ids of child nodes.
        /// </summary>
        public uint[] Children;

        /// <summary>
        /// As a name of a node is either used last ontology step or a rule name if it's root node.
        /// </summary>
        public string Name => Ontology.Length > 0 ? Ontology.Last().Split().First() : Rule;

        /// <summary>
        /// True if node does not have any child nodes.
        /// </summary>
        public bool IsLeaf => Children.Length == 0;
        
        private IEnumerable<ParseTreeNode> cachedChildren;
        private uint? cachedVertexCount;

        /// <summary>
        /// Retrieves references to child nodes.
        /// </summary>
        /// <param name="parseTree">Parse tree that holds this node.</param>
        /// <returns>Array of references to child nodes.</returns>
        public IEnumerable<ParseTreeNode> GetChildren(ParseTree parseTree) => 
            cachedChildren ?? (cachedChildren = parseTree.Data.Select(kvp => kvp.Value).Where(n => Children.Any(c => c == n.Id)));

        private uint GetVertexCount(ParseTree parseTree) => (uint) (cachedVertexCount ?? (cachedVertexCount = IsLeaf
            ? Shape.GetVertexCount()
            : GetChildren(parseTree).Aggregate(0u, (sum, node) => sum + node.GetVertexCount(parseTree))));

        internal IEnumerable<ParseTreeNode> GetMeshNodes(ParseTree parseTree) => GetVertexCount(parseTree) < MeshVertexCountLimit
            ? new List<ParseTreeNode> { this }
            : GetChildren(parseTree).SelectMany(c => c.GetMeshNodes(parseTree));

        internal IEnumerable<ParseTreeNode> GetLeafNodes(ParseTree parseTree) => IsLeaf
            ? new List<ParseTreeNode> { this }
            : GetChildren(parseTree).SelectMany(c => c.GetLeafNodes(parseTree));
    }
}
