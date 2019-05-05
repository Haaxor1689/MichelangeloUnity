using System;
using Michelangelo.Scripts;
using Michelangelo.Session;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(ObjectBase))]
    public class ObjectBaseEditor : UnityEditor.Editor {
        protected bool IsLoading;

        private string compilationOutput;
        private bool parseTreeFoldout = true;
        private bool compilationFoldout = true;
        private Vector2 scrollPos;
        public ParseTreeView TreeView { get; private set; }

        protected virtual bool CanGenerate => true;
        protected virtual string GenerateButtonTooltip => "Sends a request to Michelangelo API to generate new mesh. This will replace current mesh of the object if it has any.";

        private ObjectBase Object => (ObjectBase) target;

        private void OnEnable() {
            TreeView = new ParseTreeView(Object);
        }

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
            
            parseTreeFoldout = EditorGUILayout.Foldout(parseTreeFoldout, "Parse tree");
            if (parseTreeFoldout) {
                EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
                TreeView.OnGUI(GUILayoutUtility.GetRect(0, 100, 100, 1000));
                EditorGUILayout.EndVertical();

                using(new EditorGUILayout.HorizontalScope()) {
                    if (GUILayout.Button("Expand All", "miniButton") && (Object.ParseTree.Count < 200 || EditorUtility.DisplayDialog("Expand large tree?", $"This tree contains a total of {Object.ParseTree.Count} nodes. Expanding it may cause unity to freeze.", "Expand anyway", "Cancel"))) {
                        TreeView.ExpandAll();
                    }
                    if (GUILayout.Button("Collapse All", "miniButton")) {
                        TreeView.CollapseAll();
                    }
                }
            }

            EditorGUILayout.Space();
            GUI.enabled = GUI.enabled && CanGenerate;
            if (GUILayout.Button(new GUIContent("Generate new mesh", GenerateButtonTooltip), GUILayout.Height(40.0f))) {
                Async(Generate);
            }

            RenderCompilationOutput();
        }

        protected virtual void RenderBody() {}

        private void RenderCompilationOutput() {
            if (string.IsNullOrEmpty(compilationOutput)) {
                return;
            }

            if (RequestErrorMessage.IsRequestError(compilationOutput)) {
                RequestErrorMessage.Draw(ref compilationOutput);
                return;
            }
            
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            compilationFoldout = EditorGUILayout.Foldout(compilationFoldout, "Compilation output");
            if (GUILayout.Button("Clear")) {
                compilationOutput = null;
                return;
            }
            EditorGUILayout.EndHorizontal();

            if (!compilationFoldout) {
                return;
            }

            EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            var lines = compilationOutput.Split('\n');
            foreach (var line in lines) {
                var split = line.Split('\t');

                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
                EditorGUILayout.LabelField(split[0], new GUIStyle {
                    normal = { textColor = GetOutputNoteColor(split[0]) },
                    alignment = TextAnchor.MiddleRight,
                    fontStyle = FontStyle.Bold
                }, GUILayout.Width(70));

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(split[1], EditorStyles.boldLabel);
                EditorGUILayout.LabelField(split[2], EditorStyles.wordWrappedLabel);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private Color GetOutputNoteColor(string text) {
            switch (text) {
                case "Suggestion":
                case "Info":
                    return Color.blue;
                case "Warning":
                    return new Color(0.6f, 0.6f, 0.0f);
                case "Serious":
                case "Critical":
                    return new Color(0.6f, 0.2f, 0.0f);
                case "Comment": return new Color(0.1f, 0.6f, 0.1f);
                case "Fatal": return new Color(0.8f, 0.1f, 0.0f);
                default: return Color.cyan;
            }
        }

        protected void Generate() {
            Object.Generate()
                  .Then(response => {
                      compilationOutput = response.ErrorMessage;
                      IsLoading = false;
                      TreeView = new ParseTreeView(Object);
                      Repaint();
                  })
                  .Catch(OnRejected);
        }

        protected void OnRejected(Exception error) {
            compilationOutput = error.Message;
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
