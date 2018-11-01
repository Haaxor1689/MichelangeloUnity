using System.Runtime.Remoting.Messaging;
using Michelangelo.Model;
using Michelangelo.Model.Render;
using RSG;
using UnityEngine;

namespace Michelangelo {
    [SelectionBase]
    public class MichelangeloObject : MonoBehaviour {

        [SerializeField, HideInInspector]
        private string id;

        public Grammar Grammar => MichelangeloSession.GetGrammar(id) ?? Grammar.Placeholder;

        public IPromise<GenerateGrammarResponse> Generate() {
            MichelangeloSession.GrammarList[Grammar.id].code = Grammar.code;
            return MichelangeloSession.GenerateGrammar(Grammar.id).Then(response => CreateMesh(response.Mesh));
        }

        private void CreateMesh(ModelMesh model) {
            DeleteOldMeshes();
            foreach (var primitive in model.Primitives) {
                MichelangeloMesh.Construct(transform, primitive, model.Materials[primitive.Material]);
            }
        }

        private void DeleteOldMeshes() {
            for (var i = 0; i < transform.childCount; ++i) {
                var child = transform.GetChild(i);
                if (child.name == MichelangeloMesh.MichelangeloMeshObjectName) {
                    DestroyImmediate(child.gameObject);
                    --i;
                }
            }
        }

        public static GameObject Construct(Grammar grammar) {
            var newObject = new GameObject(grammar.name);
            var michelangeloObject = newObject.AddComponent<MichelangeloObject>();
            michelangeloObject.id = grammar.id;
            return newObject;
        }
    }
}
