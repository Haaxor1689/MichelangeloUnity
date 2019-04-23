using System;
using System.Collections.Generic;
using System.Linq;

namespace Michelangelo.Model.MichelangeloApi {
    [Serializable]
    public class ParseTree : SerializableDictionary<uint, ParseTreeModel> {
        public ParseTree(IDictionary<uint, ParseTreeModel> dict) : base(FilterDanglingLeaves(dict)) {}

        public IEnumerable<ParseTreeModel> GetMeshNodes() => Values.Where(n => n.Rule == "ROOT").SelectMany(n => n.GetMeshNodes(this));

        private static IDictionary<uint, ParseTreeModel> FilterDanglingLeaves(IDictionary<uint, ParseTreeModel> dict) {
            foreach (var pair in dict) {
                pair.Value.ChildIndices = pair.Value.ChildIndices.Where(dict.ContainsKey).ToArray();
            }
            return dict;
        }
    }
}
