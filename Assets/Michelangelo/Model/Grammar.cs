using System;
using System.Globalization;
using System.IO;
using System.Text;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public class Grammar {
        public static readonly Grammar Placeholder = new Grammar { name = "...", type = "..." };

        public string code;
        public string id;
        public bool isOwner;
        public string lastModified;
        public string name;
        public bool shared;
        public string[] tags;
        public string type;
        public bool isTutorial;

        private DateTime lastModifiedDate;
        public DateTime LastModifiedDate => lastModifiedDate != DateTime.MinValue ? lastModifiedDate : lastModifiedDate = Convert.ToDateTime(lastModified);

        private TextAsset sourceCode;
        public TextAsset SourceCode => sourceCode ? sourceCode : sourceCode = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine("Assets", SourceFilePathRelative));

        public string ClassName => name.ClassNameFriendly();
        public string FileName => $"{ClassName}Grammar.txt";
        public string SourceFilePathRelative => Path.Combine(Constants.GrammarCodeFolderRelative, FileName);
        public string SourceFilePath => Path.Combine(Constants.GrammarCodeFolder, FileName);

        public void SaveSourceCodeFile() {
            if (File.Exists(SourceFilePath)) {
                Debug.LogWarning($"Replacing old code file for \"{name}\".");
            }

            File.Copy(Path.Combine(Constants.GrammarCodeFolder, "_empty_Grammar.cs"), SourceFilePath, true);
            File.WriteAllText(SourceFilePath, code);
            AssetDatabase.Refresh();
        }

        public static Grammar FromJSON(string json) => JsonUtility.FromJson<Grammar>(json);
        public static Grammar[] FromJSONArray(string json) => JsonArray.FromJsonArray<Grammar>(json);

        public new string ToString() {
            var builder = new StringBuilder();
            builder.Append("Grammar ");
            builder.Append(name);
            builder.Append("(");
            builder.Append(type);
            builder.Append("), modified ");
            builder.Append(lastModified);
            return builder.ToString();
        }
        
        public void Draw(Action onResolved, Action<Exception> onRejected, bool showInstantiate = false) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Type", type);
            EditorGUILayout.LabelField("Last Modified", LastModifiedDate.ToString(CultureInfo.CurrentCulture));
            DrawCodeField(onResolved, onRejected);

            if (showInstantiate || isOwner) {
                EditorGUILayout.Space();
            }

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            if (showInstantiate && GUILayout.Button("Instantiate")) {
                MichelangeloSession.InstantiateGrammar(id).Then(_ => onResolved()).Catch(onRejected);
            }
            if (isOwner && GUILayout.Button("Delete Grammar") && EditorUtility.DisplayDialog("Delete grammar?", $"Are you sure you want to delete grammar \"{name}\"?", "Delete", "Cancel")) {
                MichelangeloSession.DeleteGrammar(id).Then(onResolved).Catch(onRejected);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private void DrawCodeField(Action onResolved, Action<Exception> onRejected) {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Code", GUILayout.Width(EditorGUIUtility.labelWidth - GUI.skin.box.padding.left));
            if (SourceCode != null) {
                var original = GUI.enabled;
                GUI.enabled = false;
                EditorGUILayout.ObjectField(SourceCode, typeof(TextAsset), false, GUILayout.ExpandWidth(true));
                GUI.enabled = original;
            }
            if (GUILayout.Button("Download", GUILayout.ExpandWidth(true)) 
             && (SourceCode == null || 
                 EditorUtility.DisplayDialog(
                     "Download server version of grammar?",
                     "Any unsaved local changes to the grammar will be lost.",
                     "Download",
                     "Cancel"))) {
                MichelangeloSession.GetGrammar(id).Then(g => {
                    g.SaveSourceCodeFile();
                    onResolved();
                }).Catch(onRejected);
            }
            EditorGUILayout.EndHorizontal();
        }

        public void DrawSimple(Action<string> onSelected) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
            if (GUILayout.Button("Select")) {
                onSelected(id);
            }
            EditorGUILayout.EndVertical();
        }
    }
}
