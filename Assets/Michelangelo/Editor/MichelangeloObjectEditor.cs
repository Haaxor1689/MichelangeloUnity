using System;
using System.IO;
using System.Text.RegularExpressions;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Session;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(MichelangeloObject))]
    public class LevelScriptEditor : UnityEditor.Editor {
        private static readonly string GrammarCodeFolder = Path.Combine("Michelangelo", "GrammarSources");
        private string errorMessage;
        private bool isLoading;

        private Vector2 scrollPos;

        public override void OnInspectorGUI() {
            if (!WebAPI.IsAuthenticated) {
                EditorGUILayout.LabelField("To use this feature, please log in to Michelangelo first, through Window -> Michelangelo.");
                return;
            }

            var obj = (MichelangeloObject) target;
            if (obj.Grammar != Grammar.Placeholder &&
                string.IsNullOrEmpty(obj.Grammar?.code) &&
                !isLoading) {
                Reload();
            }

            EditorGUILayout.LabelField("Name: ", obj.Grammar?.name);
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            EditorGUILayout.LabelField("Code: ", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MinHeight(100), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            EditorGUILayout.TextArea(obj.Grammar?.code);
            EditorGUILayout.EndScrollView();

            GUILayout.Space(20.0f);
            if (isLoading) {
                EditorGUILayout.LabelField("Please wait...", EditorStyles.boldLabel);
                return;
            }

            if (GUILayout.Button("Reload")) {
                Reload();
            }

            if (obj.Grammar?.code != "" && GUILayout.Button("Create code file")) {
                CreateCodeFile();
            }

            if (obj.Grammar?.code != "" && GUILayout.Button("Generate")) {
                isLoading = true;
                MichelangeloSession.GenerateGrammar(obj.Grammar?.id)
                                   .Then(model => {
                                       obj.Model = model;
                                       isLoading = false;
                                       Repaint();
                                   })
                                   .Catch(HandleError);
            }
            if (!string.IsNullOrEmpty(errorMessage)) {
                var style = new GUIStyle(EditorStyles.textField) { normal = { textColor = Color.red } };

                GUILayout.Space(20.0f);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(errorMessage, style);
                if (GUILayout.Button("X")) {
                    errorMessage = null;
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void Reload() {
            var obj = (MichelangeloObject) target;
            isLoading = true;
            MichelangeloSession.UpdateGrammar(obj.Grammar.id)
                               .Then(_ => {
                                   isLoading = false;
                                   Repaint();
                               })
                               .Catch(HandleError);
        }

        private void CreateCodeFile() {
            var obj = (MichelangeloObject) target;
            var fullPath = Path.Combine(Application.dataPath, GrammarCodeFolder);
            var friendlyScriptName = obj.Grammar.name;
            var codeFileName = Path.Combine(fullPath, $"{friendlyScriptName}Grammar.cs");

            if (File.Exists(codeFileName)) {
                HandleError(new Exception($"File with name {Path.GetFileName(codeFileName)} already exists."));
                return;
            }

            File.Copy(Path.Combine(fullPath, "_empty_Grammar.cs"), codeFileName);

            var indentedCode = Regex.Replace(obj.Grammar.code, "\n", "\n            ");
            File.WriteAllText(codeFileName, Regex.Replace(Regex.Replace(File.ReadAllText(codeFileName), "_empty_", friendlyScriptName), "//_codePlaceholder_", indentedCode));
            AssetDatabase.Refresh();
        }

        private void HandleError(Exception error) {
            errorMessage = error.Message;
            isLoading = false;
            Repaint();
            Debug.LogError(error);
        }
    }
}
