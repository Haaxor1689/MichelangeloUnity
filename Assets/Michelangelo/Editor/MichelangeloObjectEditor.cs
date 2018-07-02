using System;
using System.IO;
using System.Text.RegularExpressions;
using Michelangelo.GrammarSources;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Session;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    // [CustomEditor(typeof(MichelangeloObject))]
    public class LevelScriptEditor : UnityEditor.Editor {
        private string errorMessage;
        private bool isLoading;

        public GrammarSourceBase foo;

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

            EditorGUILayout.LabelField("Name:", obj.Grammar?.name ?? Constants.PlaceholderText);
            EditorGUILayout.LabelField("Type:", obj.Grammar?.type ?? Constants.PlaceholderText);
            EditorGUILayout.LabelField("Last Modified:", obj.Grammar?.lastModified ?? Constants.PlaceholderText);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Code:", obj.Grammar?.SourceFile, typeof(GrammarSourceBase), false);
            EditorGUI.EndDisabledGroup();

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

            var sourceFilePath = obj.Grammar.SourceFilePath;
            if (File.Exists(sourceFilePath)) {
                HandleError(new Exception($"File with name {Path.GetFileName(sourceFilePath)} already exists."));
                return;
            }

            File.Copy(Path.Combine(Constants.GrammarCodeFolder, "_empty_Grammar.cs"), sourceFilePath);

            var indentedCode = Regex.Replace(obj.Grammar.code, "\n", "\n            ");
            File.WriteAllText(sourceFilePath, Regex.Replace(Regex.Replace(File.ReadAllText(sourceFilePath), "_empty_", obj.Grammar.ClassName), "//_codePlaceholder_", indentedCode));
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
