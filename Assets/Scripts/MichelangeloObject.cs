using System;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo.Scripts {
    [ExecuteInEditMode]
    public class MichelangeloObject : MonoBehaviour {
        [SerializeField]
        private string grammarId;
        public Grammar grammar { get { return MichelangeloSession.GetGrammar(grammarId); } }

        public ModelMesh model;

        public void SetGrammar(Grammar g) {
            grammarId = g.id;
        }

        private void Update() {
            if (model != null) {
                model.Render();
            }
        }

        public void ModelGenerated(ModelMesh newModel) {
            model = newModel;
            MichelangeloSession.modelGenerated -= ModelGenerated;
        }
    }
}