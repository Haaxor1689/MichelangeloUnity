using System;
using System.Collections.Generic;
using System.Linq;

namespace Michelangelo.Models.MichelangeloApi {
    [Serializable]
    public class NormalizedParseTreeModel {
        private const uint MeshVertexCountLimit = 65534u;

        public uint Id = uint.MaxValue;
        public string Rule = string.Empty;
        public uint Parent = uint.MaxValue;
        public string[] Ontology;
        public GeometricModel Shape;
        public uint[] Children;

        public string Name => Ontology.Length > 0 ? Ontology.Last().Split().First() : Rule;
        public bool IsLeaf => Children.Length == 0;
        
        private IEnumerable<NormalizedParseTreeModel> cachedChildren;
        private uint? cachedVertexCount;

        public IEnumerable<NormalizedParseTreeModel> GetChildren(ParseTree parseTree) => 
            cachedChildren ?? (cachedChildren = parseTree.Data.Select(kvp => kvp.Value).Where(n => Children.Any(c => c == n.Id)));

        private uint GetVertexCount(ParseTree parseTree) => (uint) (cachedVertexCount ?? (cachedVertexCount = IsLeaf
            ? Shape.GetVertexCount()
            : GetChildren(parseTree).Aggregate(0u, (sum, node) => sum + node.GetVertexCount(parseTree))));

        public IEnumerable<NormalizedParseTreeModel> GetMeshNodes(ParseTree parseTree) => GetVertexCount(parseTree) < MeshVertexCountLimit
            ? new List<NormalizedParseTreeModel> { this }
            : GetChildren(parseTree).SelectMany(c => c.GetMeshNodes(parseTree));

        public IEnumerable<NormalizedParseTreeModel> GetLeafNodes(ParseTree parseTree) => IsLeaf
            ? new List<NormalizedParseTreeModel> { this }
            : GetChildren(parseTree).SelectMany(c => c.GetLeafNodes(parseTree));
    }
}
