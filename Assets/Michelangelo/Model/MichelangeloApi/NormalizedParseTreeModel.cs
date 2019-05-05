using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Michelangelo.Model.MichelangeloApi {
    [Serializable]
    public class NormalizedParseTreeModel {
        private const uint MeshVertexCountLimit = 65534u;

        public uint Id = uint.MaxValue;
        public string Rule = string.Empty;

        public string Name => Ontology.Length > 0 ? Ontology.Last().Split().First() : Rule;
        public bool IsLeaf => Children.Length == 0;
        public string[] Ontology;
        public GeometricModel Shape;
        public uint[] Children;

        private uint? cachedVertexCount;

        public IEnumerable<NormalizedParseTreeModel> GetChildren(ParseTree parseTree) => parseTree.Data.Select(kvp => kvp.Value).Where(n => Children.Any(c => c == n.Id));
 
        public uint GetVertexCount(ParseTree parseTree) {
            if (cachedVertexCount == null) {
                cachedVertexCount = IsLeaf
                    ? Shape.GetVertexCount()
                    : GetChildren(parseTree).Aggregate(0u, (sum, node) => sum + node.GetVertexCount(parseTree));
            }
            return cachedVertexCount.Value;
        }

        public IEnumerable<NormalizedParseTreeModel> GetMeshNodes(ParseTree parseTree) => GetVertexCount(parseTree) < MeshVertexCountLimit
            ? new List<NormalizedParseTreeModel> { this }
            : GetChildren(parseTree).SelectMany(c => c.GetMeshNodes(parseTree));

        public IEnumerable<NormalizedParseTreeModel> GetLeafNodes(ParseTree parseTree) => IsLeaf
            ? new List<NormalizedParseTreeModel> { this }
            : GetChildren(parseTree).SelectMany(c => c.GetLeafNodes(parseTree));
    }
}
