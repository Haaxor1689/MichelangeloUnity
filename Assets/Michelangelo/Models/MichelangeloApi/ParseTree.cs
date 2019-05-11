using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Michelangelo.Models.MichelangeloApi {
    [Serializable]
    public class ParseTree : ISerializationCallbackReceiver {
        public Dictionary<uint, NormalizedParseTreeModel> Data;

        [SerializeField]
        private List<NormalizedParseTreeModel> serializedValues = new List<NormalizedParseTreeModel>();
        
        private IEnumerable<NormalizedParseTreeModel> cachedRoots;

        public NormalizedParseTreeModel this[uint key] => Data?[key];
        public int Count => Data?.Count ?? 0;

        public IEnumerable<NormalizedParseTreeModel> GetRoots() => cachedRoots ?? (cachedRoots = Data.Values.Where(n => n.Rule == "ROOT"));
        public IEnumerable<NormalizedParseTreeModel> GetMeshNodes() => GetRoots().SelectMany(n => n.GetMeshNodes(this));

        public ParseTree(IDictionary<uint, ParseTreeModel> dict) {
            Data = dict.SelectMany(kvp => kvp.Value.ChildIndices).Concat(dict.Keys).Distinct().Select(id => {
                var node = dict.ContainsKey(id) ? dict[id] : null;
                var parent = node?.Rule == "ROOT" ? null : dict.First(n => n.Value.ChildIndices.Any(c => c == id)).Value;
                var index = parent?.ChildIndices.ToList().IndexOf(id);
                return new NormalizedParseTreeModel {
                    Id = id,
                    Rule = node?.Rule,
                    Parent = parent?.ID ?? uint.MaxValue,
                    Ontology = parent?.Ontology[index.Value] ?? new string[0],
                    Shape = parent?.Shape[index.Value],
                    Children = node?.ChildIndices ?? new uint[0]
                };
            }).ToDictionary(k => k.Id, v => v);
        }

        public void OnBeforeSerialize() {
            if (Data == null || serializedValues.Count == Data.Count) {
                return;
            }
            serializedValues.Clear();
            foreach (var pair in Data) {
                serializedValues.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize() {
            Data = new Dictionary<uint, NormalizedParseTreeModel>();
            foreach (var model in serializedValues) {
                Data.Add(model.Id, model);
            }
        }
    }
}
