using System.Collections.Generic;
using System.Linq;

namespace Michelangelo.Model.MichelangeloApi {
    public class ParseTreeChild {
        private const uint MeshVertexCountLimit = 65534u;

        public uint Index;
        public bool IsLeaf = false;
        public string[] Ontology;
        public GeometricModel Shape;
        
        public uint GetVertexCount(ParseTree parseTree) =>
            IsLeaf ? Shape.GetVertexCount() : parseTree[Index].GetVertexCount(parseTree);
        
        public IEnumerable<ParseTreeChild> GetMeshNodes(ParseTree parseTree) => GetVertexCount(parseTree) < MeshVertexCountLimit
            ? new List<ParseTreeChild> { this }
            : parseTree[Index].GetMeshNodes(parseTree);

        public IEnumerable<ParseTreeChild> GetLeafNodes(ParseTree parseTree) =>
            IsLeaf ? new List<ParseTreeChild> { this } : parseTree[Index].GetLeafNodes(parseTree);
    }

    public class NormalizedParseTreeModel {
        public ParseTreeChild[] Children;

        public uint ID = uint.MaxValue;
        public string Rule = string.Empty;

        public uint GetVertexCount(ParseTree parseTree) => 
            Children.Aggregate(0u, (sum, node) => sum + (node.IsLeaf ? node.Shape.GetVertexCount() : parseTree[node.Index].GetVertexCount(parseTree)));

        public IEnumerable<ParseTreeChild> GetMeshNodes(ParseTree parseTree) => Children.SelectMany(c => c.GetMeshNodes(parseTree)); 
        public IEnumerable<ParseTreeChild> GetLeafNodes(ParseTree parseTree) => Children.SelectMany(c => c.GetLeafNodes(parseTree));
    }
}
