using System;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Session;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(GrammarObject))]
    public class GrammarObjectEditor : ObjectBaseEditor {
        private new GrammarObject Object => (GrammarObject) target;

        protected override void RenderBody() {
            if (Object.Grammar == Grammar.Placeholder) {
                EditorGUILayout.HelpBox("You must first connect this object to a grammar.", MessageType.Info);
                if (GUILayout.Button("Connect to grammar", GUILayout.Height(40.0f))) {
                    ReconnectGrammarPopup.Init(Object);
                }
                GUI.enabled = false;
            }

            Object.Grammar.Draw(Repaint, OnRejected);
            EditorGUILayout.Space();
            
            if (string.IsNullOrEmpty(Object.Grammar.code) && GUI.enabled) {
                EditorGUILayout.HelpBox("Grammar source code missing. Please download it first.", MessageType.Info);
                GUI.enabled = false;
            }
        }

        protected void Reload() {
            MichelangeloSession.UpdateGrammar(Object.Grammar.id)
                               .Then(grammar => {
                                   isLoading = false;
                                   Repaint();
                               })
                               .Catch(OnRejected);
        }
    }
}
