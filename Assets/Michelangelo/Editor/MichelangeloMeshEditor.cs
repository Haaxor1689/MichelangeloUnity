using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(MichelangeloMesh))]
    public class MichelangeloMeshEditor : UnityEditor.Editor {

        private MichelangeloMesh Script => (MichelangeloMesh) target;

        public override void OnInspectorGUI() {
            GUI.enabled = true;
            if (GUILayout.Button("Select MichelangeloObject", GUILayout.Height(40.0f))) {
                Selection.objects = new Object[] { Script.transform.parent.gameObject };
            }
            GUI.enabled = false;
        }
    }
}
