using System;
using System.Globalization;
using System.IO;
using Michelangelo.Models;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor.Draw {
    internal static class GrammarDraw {
        internal static void Draw(this Grammar grammar, Action onResolved, Action<Exception> onRejected, bool showInstantiate = false) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(grammar.name, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Type", grammar.type);
            EditorGUILayout.LabelField("Last Modified", grammar.LastModifiedDate.ToString(CultureInfo.CurrentCulture));
            grammar.DrawCodeField(onResolved, onRejected);

            if (showInstantiate || grammar.isOwner) {
                EditorGUILayout.Space();
            }

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            if (showInstantiate && GUILayout.Button("Instantiate")) {
                MichelangeloSession.InstantiateGrammar(grammar.id).Then(_ => onResolved()).Catch(onRejected);
            }
            if (grammar.isOwner && GUILayout.Button("Delete Grammar") && EditorUtility.DisplayDialog("Delete grammar?", $"Are you sure you want to delete grammar \"{grammar.name}\"?", "Delete", "Cancel")) {
                MichelangeloSession.DeleteGrammar(grammar.id).Then(onResolved).Catch(onRejected);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private static void DrawCodeField(this Grammar grammar, Action onResolved, Action<Exception> onRejected) {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Code", GUILayout.Width(EditorGUIUtility.labelWidth - GUI.skin.box.padding.left));
            if (grammar.SourceCode != null) {
                var original = GUI.enabled;
                GUI.enabled = false;
                EditorGUILayout.ObjectField(grammar.SourceCode, typeof(TextAsset), false, GUILayout.ExpandWidth(true));
                GUI.enabled = original;
            }
            if (GUILayout.Button("Download", GUILayout.ExpandWidth(true)) 
             && (grammar.SourceCode == null || 
                 EditorUtility.DisplayDialog(
                     "Download server version of grammar?",
                     "Any unsaved local changes to the grammar will be lost.",
                     "Download",
                     "Cancel"))) {
                MichelangeloSession.GetGrammar(grammar.id).Then(g => {
                    g.SaveSourceCodeFile();
                    onResolved();
                }).Catch(onRejected);
            }
            EditorGUILayout.EndHorizontal();
        }

        internal static void DrawSimple(this Grammar grammar, Action<string> onSelected) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(grammar.name, EditorStyles.boldLabel);
            if (GUILayout.Button("Select")) {
                onSelected(grammar.id);
            }
            EditorGUILayout.EndVertical();
        }

        private static void SaveSourceCodeFile(this Grammar grammar) {
            if (File.Exists(grammar.SourceFilePath)) {
                Debug.LogWarning($"Replacing old code file for \"{grammar.name}\".");
            }

            File.WriteAllText(grammar.SourceFilePath, grammar.code);
            AssetDatabase.Refresh();
        }
    }
}
