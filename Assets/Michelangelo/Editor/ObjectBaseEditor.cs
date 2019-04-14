using System;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Session;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(ObjectBase))]
    public class ObjectBaseEditor : UnityEditor.Editor {
        protected string errorMessage;
        protected bool isLoading;
        protected bool isSynced;

        protected virtual ObjectBase Object => (ObjectBase) target;

        public override void OnInspectorGUI() {
            if (!WebAPI.IsAuthenticated) {
                EditorGUILayout.HelpBox("To use this feature, please log in to Michelangelo first.", MessageType.Warning);
                MichelangeloEditorWindow.OpenMichelangeloWindowButton();
                GUI.enabled = false;
            } else if (isLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                if (GUILayout.Button("Stop generation", GUILayout.Height(40.0f))) {
                    WebAPI.CancelGeneration = true;
                    isLoading = false;
                }
                GUI.enabled = false;
            }

            RenderBody();

            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            if (Object.HasMesh) {
                serializedObject.FindProperty("isInEditMode").boolValue = EditorGUILayout.Toggle(
                    new GUIContent("Edit mode", "Allows selection of single mesh elements and displays additional info about them."),
                    serializedObject.FindProperty("isInEditMode").boolValue);
                serializedObject.ApplyModifiedProperties();

                if (!Object.IsFlatShaded
                  && GUILayout.Button(new GUIContent("Transform to flat shaded", "Used by default in Michelangelo online preview.")) 
                  && EditorUtility.DisplayDialog(
                        "Transform to flat shaded?",
                        "This action is irreversible and will cause changes to generated meshes.",
                        "Transform",
                        "Cancel")) {
                    Object.ToFlatShaded();
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

        protected virtual void RenderBody() {}

        protected void Generate() {
            Object.Generate()
                  .Then(response => {
                      errorMessage = response.ErrorMessage;
                      isLoading = false;
                      Repaint();
                  })
                  .Catch(OnRejected);
        }

        protected void OnRejected(Exception error) {
            errorMessage = error.Message;
            isLoading = false;
            Repaint();
            Debug.LogError(error);
        }

        protected void Async(Action a) {
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
