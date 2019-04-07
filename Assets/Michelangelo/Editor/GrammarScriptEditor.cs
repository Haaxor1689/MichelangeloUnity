using System;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Session;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(GrammarScript))]
    public class GrammarScriptEditor : UnityEditor.Editor {
        private string errorMessage;
        private bool isLoading;
        private bool isSynced;

        private GrammarScript Script => (GrammarScript) target;

        public override void OnInspectorGUI() {
            if (!WebAPI.IsAuthenticated) {
                EditorGUILayout.HelpBox("To use this feature, please log in to Michelangelo first.", MessageType.Warning);
                MichelangeloEditorWindow.OpenMichelangeloWindowButton();
                GUI.enabled = false;
            } else if (Script.Grammar == Grammar.Placeholder) {
                EditorGUILayout.HelpBox("Grammar this object is referring to no longer exists. Try refreshing grammars in Michelangelo window or reconnect this object to other grammar.", MessageType.Warning);
                if (GUILayout.Button("Reconnect to grammar", GUILayout.Height(40.0f))) {
                    ReconnectGrammarPopup.Init(Script);
                }
                GUI.enabled = false;
            } else if (isLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                if (GUILayout.Button("Stop generation", GUILayout.Height(40.0f))) {
                    WebAPI.CancelGeneration = true;
                }
                GUI.enabled = false;
            }
            
            Script.Grammar.Draw(Repaint, OnRejected);
            EditorGUILayout.Space();
            
            if (string.IsNullOrEmpty(Script.Grammar.code) && GUI.enabled) {
                EditorGUILayout.HelpBox("Grammar source code missing. Please download it first.", MessageType.Info);
                GUI.enabled = false;
            }

            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            if (Script.HasMesh) {
                serializedObject.FindProperty("isInEditMode").boolValue = EditorGUILayout.Toggle(
                    new GUIContent("Edit mode", "Allows selection of single mesh elements and displays additional info about them."),
                    serializedObject.FindProperty("isInEditMode").boolValue);
                serializedObject.ApplyModifiedProperties();

                if (!Script.IsFlatShaded
                  && GUILayout.Button(new GUIContent("Transform to flat shaded", "Used by default in Michelangelo online preview.")) 
                  && EditorUtility.DisplayDialog(
                        "Transform to flat shaded?",
                        "This action is irreversible and will cause changes to generated meshes.",
                        "Transform",
                        "Cancel")) {
                    Script.ToFlatShaded();
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Generate new mesh", GUILayout.Height(40.0f))) {
                Async(Generate);
            }

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
