﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Michelangelo.Model.MichelangeloApi {
    [Serializable]
    public class ParseTree : SerializableDictionary<uint, NormalizedParseTreeModel> {
        public ParseTree(IDictionary<uint, ParseTreeModel> dict) : base(FilterDanglingLeaves(dict)) { }

        public IEnumerable<ParseTreeChild> GetMeshNodes() => Values.Where(n => n.Rule == "ROOT").SelectMany(n => n.GetMeshNodes(this));

        private static IDictionary<uint, NormalizedParseTreeModel> FilterDanglingLeaves(IDictionary<uint, ParseTreeModel> dict) => 
            dict.Select(k => k.Value)
                .Select(m => new NormalizedParseTreeModel {
                    ID = m.ID,
                    Rule = m.Rule,
                    Children = m.ChildIndices.Select((c, i) => new ParseTreeChild {
                        Index = c,
                        IsLeaf = !dict.ContainsKey(c),
                        Ontology = m.Ontology[i],
                        Shape = m.Shape[i]
                    }).ToArray()
                })
                .ToDictionary(k => k.ID, v => v);
    }
}