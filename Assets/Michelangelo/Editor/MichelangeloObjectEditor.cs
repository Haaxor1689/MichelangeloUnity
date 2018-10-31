using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Michelangelo.Model;
using Michelangelo.Session;
using Michelangelo.Utility;
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
            DrawDefaultInspector();

            if (!WebAPI.IsAuthenticated) {
                EditorGUILayout.HelpBox("To use this feature, please log in to Michelangelo first,\nthrough Window -> Michelangelo.", MessageType.Warning);
                GUI.enabled = false;
            } else if (isLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                GUI.enabled = false;
            }

            EditorGUILayout.LabelField(Script.Grammar.name ?? Constants.PlaceholderText, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Type", Script.Grammar.type ?? Constants.PlaceholderText);
            EditorGUILayout.LabelField("Last Modified", Script.Grammar.LastModifiedDate.ToString(CultureInfo.CurrentCulture));

            if (GUILayout.Button("Reload")) {
                Reload();
            }

            if (string.IsNullOrEmpty(Script.Grammar?.code)) {
                EditorGUILayout.HelpBox("Grammar source code missing. Please reload it first.", MessageType.Info);
                GUI.enabled = false;
            }

            if (GUILayout.Button("Create code file")) {
                CreateCodeFile();
            }

            if (GUILayout.Button("Generate")) {
                Generate();
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
            isLoading = true;
            MichelangeloSession.GrammarList[Script.Grammar.id].code = Script.Grammar.code;
            MichelangeloSession.GenerateGrammar(Script.Grammar.id)
                               .Then(model => {
                                   Script.Model = model;
                                   Script.CreateMesh();
                                   isLoading = false;
                                   Repaint();
                               })
                               .Catch(HandleError);
        }

        private void Reload() {
            isLoading = true;
            MichelangeloSession.UpdateGrammar(Script.Grammar.id)
                               .Then(grammar => {
                                   isLoading = false;
                                   if (grammar != Grammar.Placeholder) {
                                       Script.Grammar = grammar;
                                   }
                                   Repaint();
                               })
                               .Catch(HandleError);
        }

        private void CreateCodeFile() {
            if (Script.Grammar.type == "ACGAX") {
                HandleError(new ApplicationException("Editing of ACGAX grammars in editor isn't supported."));
                return;
            }

            var sourceFilePath = Script.Grammar.SourceFilePath;
            if (File.Exists(sourceFilePath)) {
                HandleError(new Exception($"File with name {Path.GetFileName(sourceFilePath)} already exists."));
                return;
            }

            File.Copy(Path.Combine(Constants.GrammarCodeFolder, "_empty_Grammar.cs"), sourceFilePath);

            var indentedCode = Regex.Replace(Script.Grammar.code, "\n", "\n            ");
            File.WriteAllText(sourceFilePath, Regex.Replace(Regex.Replace(File.ReadAllText(sourceFilePath), "_empty_", Script.Grammar.ClassName), "//_codePlaceholder_", indentedCode));
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
