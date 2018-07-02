using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo {
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MichelangeloObject : MonoBehaviour {
        [HideInInspector] public Grammar Grammar;
        [HideInInspector] public ModelMesh Model;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private void Awake() {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update() {
            Model?.Render(transform);
        }
    }
}
