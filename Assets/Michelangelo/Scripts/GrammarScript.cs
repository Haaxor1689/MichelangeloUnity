using Michelangelo.Model;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    public class GrammarScript : ObjectBase {

        [SerializeField]
        private string id;
        public Grammar Grammar => MichelangeloSession.GetGrammar(id) ?? Grammar.Placeholder;

        /// <summary>
        /// Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise"/> that contains mesh info and response message from server.</returns>
        public override IPromise<GenerateGrammarResponse> Generate() {
            return MichelangeloSession.GenerateGrammar(Grammar.id).Then(response => CreateMesh(response.Mesh));
        }

        public void SetId(string newId) => id = newId;

        /// <summary>
        /// Constructs new <see cref="GameObject"/> with correctly initialized <see cref="GrammarScript"/> with a grammar.
        /// </summary>
        /// <param name="grammar">Grammar that newly created <see cref="GrammarScript"/> should be referencing.</param>
        /// <returns>Newly created <see cref="GameObject"/> with <see cref="GrammarScript"/> component initialized.</returns>
        public static GameObject Construct(Grammar grammar) {
            var newObject = new GameObject(grammar.name);
            var michelangeloObject = newObject.AddComponent<GrammarScript>();
            michelangeloObject.id = grammar.id;
            return newObject;
        }
    }
}
