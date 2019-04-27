using System;
using Michelangelo.Scripts;
using Michelangelo.Session;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(ObjectBase))]
    public class ObjectBaseEditor : UnityEditor.Editor {
        protected string ErrorMessage;
        protected bool IsLoading;

        protected virtual bool CanGenerate => true;
        protected virtual string GenerateButtonTooltip => "Sends a request to Michelangelo API to generate new mesh. This will replace current mesh of the object if it has any.";

        private ObjectBase Object => (ObjectBase) target;

        public override void OnInspectorGUI() {
            if (!WebAPI.IsAuthenticated) {
                EditorGUILayout.HelpBox("To use this feature, please log in to Michelangelo first.", MessageType.Warning);
                MichelangeloEditorWindow.OpenMichelangeloWindowButton();
                GUI.enabled = false;
            } else if (IsLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                if (GUILayout.Button("Stop generation", GUILayout.Height(40.0f))) {
                    WebAPI.CancelGeneration = true;
                    IsLoading = false;
                }
                GUI.enabled = false;
            }

            RenderBody();

            // EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            // if (Object.HasMesh) {
            //     serializedObject.FindProperty("isInEditMode").boolValue = EditorGUILayout.Toggle(
            //         new GUIContent("Edit mode", "Allows selection of single mesh elements and displays additional info about them."),
            //         serializedObject.FindProperty("isInEditMode").boolValue);
            //     serializedObject.ApplyModifiedProperties();
            //
            //     if (!Object.IsFlatShaded
            //       && GUILayout.Button(new GUIContent("Transform to flat shaded", "Used by default in Michelangelo online preview.")) 
            //       && EditorUtility.DisplayDialog(
            //             "Transform to flat shaded?",
            //             "This action is irreversible and will cause changes to generated meshes.",
            //             "Transform",
            //             "Cancel")) {
            //         Object.ToFlatShaded();
            //     }
            // }

            EditorGUILayout.Space();
            GUI.enabled = GUI.enabled && CanGenerate;
            if (GUILayout.Button(new GUIContent("Generate new mesh", GenerateButtonTooltip), GUILayout.Height(40.0f))) {
                Async(Generate);
            }

            GUI.enabled = true;
            if (!string.IsNullOrEmpty(ErrorMessage)) {
                if (string.IsNullOrEmpty(ErrorMessage)) {
                    return;
                }
                GUILayout.Space(20.0f);
                EditorGUILayout.HelpBox(ErrorMessage, MessageType.Error);
                if (GUILayout.Button("Clear error message")) {
                    ErrorMessage = null;
                }
            }
        }

        protected virtual void RenderBody() {}

        protected void Generate() {
            Object.Generate()
                  .Then(response => {
                      ErrorMessage = response.ErrorMessage;
                      IsLoading = false;
                      Repaint();
                  })
                  .Catch(OnRejected);
        }

        protected void OnRejected(Exception error) {
            ErrorMessage = error.Message;
            IsLoading = false;
            Repaint();
            Debug.LogError(error);
        }

        protected void Async(Action a) {
            try {
                IsLoading = true;
                a();
            } catch {
                IsLoading = false;
                throw;
            }
        }
    }
}
