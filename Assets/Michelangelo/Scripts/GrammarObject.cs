using Michelangelo.Models;
using RSG;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Scripts {
    /// <summary>
    ///   Unity component that represents a Michelangelo grammar object in scene.
    /// </summary>
    public class GrammarObject : ObjectBase {
        [SerializeField]
        private string id = "";

        /// <summary>
        ///   Retrieves grammar this script links to from <see cref="MichelangeloSession" />. If grammar does not exist, a
        ///   <see cref="Grammar" /> Placeholder will be returned.
        /// </summary>
        public Grammar Grammar => MichelangeloSession.GrammarList.ContainsKey(id) ? MichelangeloSession.GrammarList[id] : Grammar.Placeholder;

        /// <inheritdoc />
        public override bool CanGenerate => !string.IsNullOrEmpty(Grammar.code);

        /// <inheritdoc />
        protected override IPromise<GenerateGrammarResponse> GenerateCallback() => MichelangeloSession.GenerateGrammar(Grammar.id);

        /// <summary>
        ///   Links this script to new id.
        /// </summary>
        /// <param name="newId">New id value.</param>
        public void SetNewId(string newId) => id = newId;

        [MenuItem("Michelangelo/GrammarObject", false, 11)]
        internal static void ShowWindow() {
            var obj = new GameObject("New Grammar Object", typeof(GrammarObject));
            Selection.objects = new Object[] { obj };
            Selection.activeObject = obj;
        }

        /// <summary>
        ///   Constructs new <see cref="GameObject" /> with correctly initialized <see cref="GrammarObject" /> with a grammar.
        /// </summary>
        /// <param name="grammar">Grammar, that newly created <see cref="GrammarObject" /> should be referencing.</param>
        /// <returns>Newly created <see cref="GameObject" /> with <see cref="GrammarObject" /> component initialized.</returns>
        public static GameObject Construct(Grammar grammar) {
            var newObject = new GameObject(grammar.name);
            var michelangeloObject = newObject.AddComponent<GrammarObject>();
            michelangeloObject.id = grammar.id;
            return newObject;
        }
    }
}
