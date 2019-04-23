using Michelangelo.Model;
using RSG;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Scripts {
    public class GrammarObject : ObjectBase {

        [SerializeField]
        private string id = "";
        public Grammar Grammar => MichelangeloSession.GetGrammar(id) ?? Grammar.Placeholder;

        /// <summary>
        /// Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise"/> that contains mesh info and response message from server.</returns>
        public override IPromise<GenerateGrammarResponse> Generate() {
            return MichelangeloSession.GenerateGrammar(Grammar.id).Then(response => CreateMesh(response.ParseTree, response.Materials));
        }

        public void SetId(string newId) => id = newId;
        
        [MenuItem("Michelangelo/GrammarObject", false, 11)]
        public static void ShowWindow() {
            var obj = new GameObject("New Grammar Object", typeof(GrammarObject));
            Selection.objects = new Object[] { obj };
            Selection.activeObject = obj;
        }

        /// <summary>
        /// Constructs new <see cref="GameObject"/> with correctly initialized <see cref="GrammarObject"/> with a grammar.
        /// </summary>
        /// <param name="grammar">Grammar that newly created <see cref="GrammarObject"/> should be referencing.</param>
        /// <returns>Newly created <see cref="GameObject"/> with <see cref="GrammarObject"/> component initialized.</returns>
        public static GameObject Construct(Grammar grammar) {
            var newObject = new GameObject(grammar.name);
            var michelangeloObject = newObject.AddComponent<GrammarObject>();
            michelangeloObject.id = grammar.id;
            return newObject;
        }
    }
}
