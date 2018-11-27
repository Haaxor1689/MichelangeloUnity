using Michelangelo.Model;
using Michelangelo.Model.Render;
using RSG;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Scripts {
    public class MichelangeloObject : MonoBehaviour {

        [SerializeField]
        private string id;

        [SerializeField]
        private bool isInEditMode;

        [SerializeField]
        private ModelMesh modelMesh;

        public bool IsInEditMode => isInEditMode;
        public Grammar Grammar => MichelangeloSession.GetGrammar(id) ?? Grammar.Placeholder;

        public bool HasMesh {
            get {
                for (var i = 0; i < transform.childCount; ++i) {
                    if (transform.GetChild(i).name == MichelangeloMesh.MichelangeloMeshObjectName) {
                        return true;
                    }
                }
                return false;
            }
        }

        public IPromise<GenerateGrammarResponse> Generate() {
            return MichelangeloSession.GenerateGrammar(Grammar.id).Then(response => CreateMesh(response.Mesh));
        }

        private void CreateMesh(ModelMesh model) {
            modelMesh = model;
            DeleteOldMeshes();
            foreach (var primitive in modelMesh.Primitives) {
                MichelangeloMesh.Construct(transform, primitive, modelMesh.Materials[primitive.Material]);
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
