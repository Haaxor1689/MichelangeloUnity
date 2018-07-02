using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo {
    [ExecuteInEditMode]
    public class MichelangeloObject : MonoBehaviour {
        [SerializeField] public Grammar Grammar;
        [HideInInspector] public ModelMesh Model;
        
        private void Update() {
            Model?.Render(transform);
        }
    }
}
