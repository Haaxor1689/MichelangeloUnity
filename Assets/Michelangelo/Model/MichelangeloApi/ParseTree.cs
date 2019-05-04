using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Michelangelo.Model.MichelangeloApi {
    [Serializable]
    public class ParseTree : ISerializationCallbackReceiver {
        private Dictionary<uint, NormalizedParseTreeModel> data;

        [SerializeField]
        private List<NormalizedParseTreeModel> serializedValues = new List<NormalizedParseTreeModel>();

        public ParseTree(IDictionary<uint, ParseTreeModel> dict) {
            data = dict.Select(k => k.Value)
                       .Select(m => new NormalizedParseTreeModel { Id = m.ID, Rule = m.Rule, Children = m.ChildIndices.Select((c, i) => new ParseTreeChild { Index = c, IsLeaf = !dict.ContainsKey(c), Ontology = m.Ontology[i], Shape = m.Shape[i] }).ToArray() })
                       .ToDictionary(k => k.Id, v => v);
        }

        public NormalizedParseTreeModel this[uint key] => data?[key];
        public int Count => data?.Count ?? 0;

        public void OnBeforeSerialize() {
            if (data == null || serializedValues.Count == data.Count) {
                return;
            }
            serializedValues.Clear();
            foreach (var pair in data) {
                serializedValues.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize() {
            data = new Dictionary<uint, NormalizedParseTreeModel>();
            foreach (var model in serializedValues) {
                data.Add(model.Id, model);
            }
        }

        public IEnumerable<NormalizedParseTreeModel> GetRoots() => data.Values.Where(n => n.Rule == "ROOT");
        public NormalizedParseTreeModel GetParent(uint id) => data.Values.First(n => n.Children.Any(c => c.Index == id));
        public IEnumerable<ParseTreeChild> GetMeshNodes() => GetRoots().SelectMany(n => n.GetMeshNodes(this));
    }
}
