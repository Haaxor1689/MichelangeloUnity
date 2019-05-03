﻿using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(ParseTreeNode))]
    public class ParseTreeNodeEditor : UnityEditor.Editor {

        private ParseTreeNode Script => (ParseTreeNode) target;

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