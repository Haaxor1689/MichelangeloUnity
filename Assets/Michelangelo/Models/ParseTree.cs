using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Models.MichelangeloApi;
using UnityEngine;

namespace Michelangelo.Models {
    /// <summary>
    /// Class wrapping parse tree data and providing it's serialization.
    /// </summary>
    [Serializable]
    public class ParseTree : ISerializationCallbackReceiver {
        private IEnumerable<ParseTreeNode> cachedRoots;

        /// <summary>
        /// Normalized parse tree saved in a dictionary keyed by node ids.
        /// </summary>
        public Dictionary<uint, ParseTreeNode> Data;

        [SerializeField]
        private List<ParseTreeNode> serializedValues = new List<ParseTreeNode>();

        internal ParseTree(IReadOnlyDictionary<uint, ParseTreeModel> dict) {
            Data = dict.SelectMany(kvp => kvp.Value.ChildIndices)
                       .Concat(dict.Keys)
                       .Distinct()
                       .Select(id => {
                           var node = dict.ContainsKey(id) ? dict[id] : null;
                           var parent = node?.Rule == "ROOT" ? null : dict.First(n => n.Value.ChildIndices.Any(c => c == id)).Value;
                           var index = parent?.ChildIndices.ToList().IndexOf(id);
                           return new ParseTreeNode {
                               Id = id,
                               Rule = node?.Rule,
                               Parent = parent?.ID ?? uint.MaxValue,
                               Ontology = parent?.Ontology[index.Value] ?? new string[0],
                               Shape = parent?.Shape[index.Value],
                               Children = node?.ChildIndices ?? new uint[0]
                           };
                       })
                       .ToDictionary(k => k.Id, v => v);
        }

        /// <summary>
        ///   Retrieves <see cref="ParseTreeNode" /> with given key from <see cref="Data" />.
        /// </summary>
        /// <param name="key">Key of parse tree node.</param>
        public ParseTreeNode this[uint key] => Data?[key];

        /// <summary>
        ///   Count of nodes in parse tree.
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <inheritdoc />
        public void OnBeforeSerialize() {
            if (Data == null || serializedValues.Count == Data.Count) {
                return;
            }
            serializedValues.Clear();
            foreach (var pair in Data) {
                serializedValues.Add(pair.Value);
            }
        }

        /// <inheritdoc />
        public void OnAfterDeserialize() {
            Data = new Dictionary<uint, ParseTreeNode>();
            foreach (var model in serializedValues) {
                Data.Add(model.Id, model);
            }
        }

        /// <summary>
        /// Retrieves all model roots of parse tree.
        /// </summary>
        /// <returns>List of roots.</returns>
        public IEnumerable<ParseTreeNode> GetRoots() => cachedRoots ?? (cachedRoots = Data.Values.Where(n => n.Rule == "ROOT"));

        internal IEnumerable<ParseTreeNode> GetMeshNodes() => GetRoots().SelectMany(n => n.GetMeshNodes(this));
    }
}
