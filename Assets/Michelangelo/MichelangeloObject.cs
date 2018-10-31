using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo {
    public class MichelangeloObject : MonoBehaviour {
        [HideInInspector] public Grammar Grammar;

        public void CreateMesh(ModelMesh model) {
            DeleteOldMeshes();
            foreach (var primitive in model.Primitives) {
                var newObject = new GameObject(MichelangeloMesh.MichelangeloMeshObjectName);
                newObject.transform.SetParent(transform);
                var michelangeloMesh = newObject.AddComponent<MichelangeloMesh>();
                michelangeloMesh.CreateMesh(primitive, model.Materials[primitive.Material]);
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
    }
}
