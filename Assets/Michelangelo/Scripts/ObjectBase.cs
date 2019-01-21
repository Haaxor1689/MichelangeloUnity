using Michelangelo.Model;
using Michelangelo.Model.Render;
using Michelangelo.Utility;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    public abstract class ObjectBase : MonoBehaviour, IObjectBase {
        [SerializeField]
        protected bool isInEditMode;

        public bool IsInEditMode => isInEditMode;

        [SerializeField]
        protected ModelMesh modelMesh;

        public bool HasMesh {
            get {
                for (var i = 0; i < transform.childCount; ++i) {
                    if (transform.GetChild(i).name == Element.ObjectName) {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsFlatShaded;

        public abstract IPromise<GenerateGrammarResponse> Generate();

        protected void CreateMesh(ModelMesh model) {
            modelMesh = model;
            DeleteOldMeshes();
            foreach (var primitive in modelMesh.Primitives) {
                Element.Construct(transform, primitive, modelMesh.Materials[primitive.Material]);
            }
        }

        private void DeleteOldMeshes() {
            for (var i = 0; i < transform.childCount; ++i) {
                var child = transform.GetChild(i);
                if (child.name == Element.ObjectName) {
                    DestroyImmediate(child.gameObject);
                    --i;
                }
            }
            IsFlatShaded = false;
        }

        public void ToFlatShaded() {
            for (var i = 0; i < transform.childCount; ++i) {
                var child = transform.GetChild(i);
                if (child.name == Element.ObjectName) {
                    MeshUtilities.ToFlatShaded(child.GetComponent<MeshFilter>().sharedMesh);
                }
            }
            IsFlatShaded = true;
        }
    }
}
