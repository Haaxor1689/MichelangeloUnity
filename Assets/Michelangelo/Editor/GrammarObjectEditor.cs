using Michelangelo.Draw;
using Michelangelo.Models;
using Michelangelo.Scripts;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo {
    [CustomEditor(typeof(GrammarObject))]
    internal class GrammarObjectEditor : ObjectBaseEditor {
        private GrammarObject Object => (GrammarObject) target;

        protected override void RenderBody() {
            if (Object.Grammar == Grammar.Placeholder) {
                EditorGUILayout.HelpBox("You must first connect this object to a grammar.", MessageType.Info);
                if (GUILayout.Button("Connect to grammar", GUILayout.Height(40.0f))) {
                    ReconnectGrammarPopup.Init(Object);
                }
                GUI.enabled = false;
            }

            Object.Grammar.Draw(Repaint, OnRejected);

            if (!string.IsNullOrEmpty(Object.Grammar.code)) {
                return;
            }
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Grammar source code missing. Please download it first.", MessageType.Info);
        }
    }
}
