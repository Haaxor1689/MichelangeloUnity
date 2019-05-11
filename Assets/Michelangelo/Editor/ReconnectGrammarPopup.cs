using System;
using System.Linq;
using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    internal class ReconnectGrammarPopup : EditorWindow {
        private GrammarObject grammar;
        private bool hasChangedNameFilter;
        private string nameFilter;
        private Vector2 scrollPos;

        public static void Init(GrammarObject grammar) {
            var window = CreateInstance<ReconnectGrammarPopup>();
            window.position = new Rect(0, 0, 300, 450);
            window.CenterOnMainWin();
            window.grammar = grammar;
            window.ShowPopup();
        }

        private void OnGUI() {
            EditorGUILayout.LabelField("Select grammar");
            DrawNameFilter();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            var grammarList = MichelangeloSession.GrammarList.Where(g => string.IsNullOrEmpty(nameFilter) || g.Value.name.IndexOf(nameFilter, StringComparison.InvariantCultureIgnoreCase) != -1);
            foreach (var g in grammarList) {
                g.Value.DrawSimple(OnSelected);
            }
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Cancel")) {
                Close();
            }
        }

        private void OnSelected(string id) {
            grammar.SetId(id);
            Close();
        }

        private void DrawNameFilter() {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Width(EditorGUIUtility.labelWidth - GUI.skin.box.padding.left));
            GUI.SetNextControlName("NameFilter");
            var newNameFilter = GUILayout.TextField(nameFilter, GUILayout.ExpandWidth(true));
            if (hasChangedNameFilter) {
                GUI.FocusControl("NameFilter");
                hasChangedNameFilter = false;
            }
            if (newNameFilter != nameFilter) {
                hasChangedNameFilter = true;
                nameFilter = newNameFilter;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
