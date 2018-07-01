using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo.Scripts {
    [ExecuteInEditMode]
    public class MichelangeloObject : MonoBehaviour {
        [SerializeField]
        private string grammarId;

        public ModelMesh Model;

        public Grammar Grammar {
            get { return MichelangeloSession.GetGrammar(grammarId); }
            set { grammarId = value.id; }
        }

        private void Update() {
            Model?.Render(transform);
        }
    }
}
