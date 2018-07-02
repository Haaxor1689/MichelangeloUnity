using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo.Scripts {
    [ExecuteInEditMode]
    public class MichelangeloObject : MonoBehaviour {
        [SerializeField]
        private string grammarId;

        public ModelMesh Model;

        [SerializeField]
        private Grammar grammar;
        public Grammar Grammar {
            get { return MichelangeloSession.GetGrammar(grammarId); }
            set {
                grammar = value;
                grammarId = value.id;
            }
        }

        private void Update() {
            Model?.Render(transform);
        }
    }
}
