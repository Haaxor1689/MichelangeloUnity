using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

namespace Michelangelo {
    [CustomEditor(typeof(ParseTreeScript))]
    internal class ParseTreeNodeEditor : Editor {

        private ParseTreeScript Script => (ParseTreeScript) target;

        public override void OnInspectorGUI() {
            GUI.enabled = true;
            if (GUILayout.Button("Select parent object", GUILayout.Height(40.0f))) {
                Selection.objects = new Object[] { Script.transform.parent.gameObject };
                Selection.activeObject = Script.transform.parent.gameObject;
            }
            GUI.enabled = false;
        }
    }
}
