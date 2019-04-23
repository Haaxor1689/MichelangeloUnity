using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [MessagePackObject(true)]
    public class ParseTreeModel {
        private const uint MeshVertexCountLimit = 65534u;

        public uint[] ChildIndices;

        /// <summary>
        ///   My id within the tree
        /// </summary>
        public uint ID = uint.MaxValue;

        public string[][] Ontology;

        //Id of the rule which created this shape
        //public uint R = uint.MaxValue;
        public string Rule = string.Empty;

        //Human readable nme of the rule
        //public string RN = string.Empty;

        public GeometricModel[] Shape;

        private uint GetVertexCount(ParseTree parseTree) => ChildIndices.Length > 0
            ? GetChildIndices(parseTree).Aggregate(0u, (sum, c) => sum + c.GetVertexCount(parseTree))
            : Shape.Aggregate(0u, (sum, c) => sum + c.GetVertexCount());

        public IEnumerable<ParseTreeModel> GetMeshNodes(ParseTree parseTree) => GetVertexCount(parseTree) > MeshVertexCountLimit
            ? GetChildIndices(parseTree).SelectMany(node => node.GetMeshNodes(parseTree))
            : new List<ParseTreeModel> { this };

        public IEnumerable<ParseTreeModel> GetLeafNodes(ParseTree parseTree) => ChildIndices.Length > 0
            ? GetChildIndices(parseTree).SelectMany(node => node.GetLeafNodes(parseTree))
            : new List<ParseTreeModel> { this };

        private IEnumerable<ParseTreeModel> GetChildIndices(ParseTree parseTree) => parseTree.Where(p => ChildIndices.Contains(p.Key)).Select(p => p.Value);
    }
}
