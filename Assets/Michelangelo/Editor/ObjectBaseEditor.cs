using System;
using Michelangelo.Scripts;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo {
    [CustomEditor(typeof(ObjectBase))]
    internal class ObjectBaseEditor : Editor {
        private bool compilationFoldout = true;
        private string compilationOutput;
        private bool parseTreeFoldout = true;
        private Vector2 scrollPos;
        private ParseTreeView TreeView { get; set; }

        protected virtual string GenerateButtonTooltip => "Sends a request to Michelangelo API to generate new mesh. This will replace current mesh of the object if it has any.";

        private ObjectBase Object => (ObjectBase) target;

        private void OnEnable() {
            TreeView = new ParseTreeView(Object);
        }

        public override void OnInspectorGUI() {
            if (!MichelangeloSession.IsAuthenticated) {
                EditorGUILayout.HelpBox("To use this feature, please log in to Michelangelo first.", MessageType.Warning);
                MichelangeloEditorWindow.OpenMichelangeloWindowButton();
                GUI.enabled = false;
            } else if (MichelangeloSession.IsLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                if (GUILayout.Button("Stop generation", GUILayout.Height(40.0f))) {
                    MichelangeloSession.CancelGeneration();
                }
                GUI.enabled = false;
            }

            RenderBody();

            EditorGUILayout.Space();
            GUI.enabled = GUI.enabled && Object.CanGenerate;
            if (GUILayout.Button(new GUIContent("Generate new mesh", GenerateButtonTooltip), GUILayout.Height(40.0f))) {
                Generate();
            }
            EditorGUILayout.Space();

            parseTreeFoldout = EditorGUILayout.Foldout(parseTreeFoldout, "Parse tree");
            if (parseTreeFoldout) {
                EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
                TreeView.OnGUI(GUILayoutUtility.GetRect(0, 100, 100, 2000));
                EditorGUILayout.EndVertical();

                using (new EditorGUILayout.HorizontalScope()) {
                    if (GUILayout.Button("Expand All", "miniButton") && (Object.ParseTree.Count < 200 || EditorUtility.DisplayDialog("Expand large tree?", $"This tree contains a total of {Object.ParseTree.Count} nodes. Expanding it may cause unity to freeze.", "Expand anyway", "Cancel"))) {
                        TreeView.ExpandAll();
                    }
                    if (GUILayout.Button("Collapse All", "miniButton")) {
                        TreeView.CollapseAll();
                    }
                }
            }

            RenderCompilationOutput();
        }

        protected virtual void RenderBody() { }

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

            EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true), GUILayout.MaxHeight(2000));
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            var lines = compilationOutput.Split(new[] { "\t\n" }, StringSplitOptions.None);
            foreach (var line in lines) {
                var split = line.Split('\t');

                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField(split[0], new GUIStyle { normal = { textColor = GetOutputNoteColor(split[0]) }, alignment = TextAnchor.MiddleRight, fontStyle = FontStyle.Bold }, GUILayout.Width(70));

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(split[1], EditorStyles.boldLabel);
                EditorGUI.LabelField(GUILayoutUtility.GetRect(new GUIContent(split[2]), "label"), split[2]);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private static Color GetOutputNoteColor(string text) {
            switch (text) {
            case "Suggestion":
            case "Info":
                return Color.blue;
            case "Warning": return new Color(0.6f, 0.6f, 0.0f);
            case "Serious":
            case "Critical":
                return new Color(0.6f, 0.2f, 0.0f);
            case "Comment": return new Color(0.1f, 0.6f, 0.1f);
            case "Fatal": return new Color(0.8f, 0.1f, 0.0f);
            default: return Color.cyan;
            }
        }

        private void Generate() {
            Object.Generate()
                  .Then(response => {
                      compilationOutput = response.ErrorMessage;
                      TreeView = new ParseTreeView(Object);
                      Repaint();
                  })
                  .Catch(OnRejected);
        }

        protected void OnRejected(Exception error) {
            compilationOutput = error.Message;
            Repaint();
            Debug.LogError(error);
        }
    }
}
