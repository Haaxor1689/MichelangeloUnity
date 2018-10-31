using Michelangelo.Model;
using Michelangelo.Model.Render;
using UnityEngine;

namespace Michelangelo {
    public class MichelangeloObject : MonoBehaviour {
        [HideInInspector] public Grammar Grammar;
        [HideInInspector] public ModelMesh Model;


        public void CreateMesh() {
            foreach (var primitive in Model.Primitives) {
                var newObject = new GameObject("PartialMesh");
                newObject.transform.SetParent(transform);
                var michelangeloMesh = newObject.AddComponent<MichelangeloMesh>();
                michelangeloMesh.CreateMesh(primitive, Model.Materials[primitive.Material]);
            }
        }
    }
}
