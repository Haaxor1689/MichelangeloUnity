using System.Linq;
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

        private MeshFilter meshFilter => GetComponent<MeshFilter>();
        private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();

        public void CreateMesh() {
            var combineInstances = Model.Primitives.Select(x => x.CombineInstance).ToArray();
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combineInstances);
        }

        private void Update() {
            Model?.Render(transform);
        }
    }
}
