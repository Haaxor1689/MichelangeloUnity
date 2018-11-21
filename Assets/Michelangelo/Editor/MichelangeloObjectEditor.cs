using System;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Session;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(MichelangeloObject))]
    public class LevelScriptEditor : UnityEditor.Editor {
        private string errorMessage;
        private bool isLoading;
        private bool isSynced;

        private MichelangeloObject Script => (MichelangeloObject) target;

        public override void OnInspectorGUI() {
            if (!WebAPI.IsAuthenticated) {
                EditorGUILayout.HelpBox("To use this feature, please log in to Michelangelo first, through Window -> Michelangelo.", MessageType.Warning);
                GUI.enabled = false;
            } else if (Script.Grammar == Grammar.Placeholder) {
                EditorGUILayout.HelpBox("Grammar this object is referring to no longer exists or wasn't downloaded. Try refreshing Michelangelo window.", MessageType.Warning);
                GUI.enabled = false;
            } else if (isLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                GUI.enabled = false;
            }
            
            Script.Grammar.Draw(Repaint, OnRejected);

            if (string.IsNullOrEmpty(Script.Grammar.code) && GUI.enabled) {
                EditorGUILayout.HelpBox("Grammar source code missing. Please download it first.", MessageType.Info);
                GUI.enabled = false;
            }

            if (GUILayout.Button("Generate")) {
                Async(Generate);
            }

            serializedObject.FindProperty("isInEditMode").boolValue = EditorGUILayout.Toggle("Edit mode", serializedObject.FindProperty("isInEditMode").boolValue);
            serializedObject.ApplyModifiedProperties();

            GUI.enabled = true;
            if (!string.IsNullOrEmpty(errorMessage)) {
                if (string.IsNullOrEmpty(errorMessage)) {
                    return;
                }
                GUILayout.Space(20.0f);
                EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
                if (GUILayout.Button("Clear error message")) {
                    errorMessage = null;
                }
            }
        }

        private void Generate() {
            Script.Generate()
                  .Then(response => {
                      errorMessage = response.ErrorMessage;
                      isLoading = false;
                      Repaint();
                  })
                  .Catch(OnRejected);
        }

        private void Reload() {
            MichelangeloSession.UpdateGrammar(Script.Grammar.id)
                               .Then(grammar => {
                                   isLoading = false;
                                   Repaint();
                               })
                               .Catch(OnRejected);
        }

        private void OnRejected(Exception error) {
            errorMessage = error.Message;
            isLoading = false;
            Repaint();
            Debug.LogError(error);
        }

        private void Async(Action a) {
            try {
                isLoading = true;
                a();
            } catch {
                isLoading = false;
                throw;
            }
        }
    }
}
