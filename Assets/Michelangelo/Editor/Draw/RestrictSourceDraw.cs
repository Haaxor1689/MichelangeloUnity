using Michelangelo.Models.Handlers;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Draw {
    internal static class RestrictSourceDraw {
        private const int ButtonWidth = 40;

        public static void Draw(this RestrictSource source) {
            EditorGUILayout.LabelField("Restrict", EditorStyles.boldLabel);
            source.SourceType = (SourceType) EditorGUILayout.EnumFlagsField("Source", source.SourceType);
            if (source.SourceType.HasFlag(SourceType.Team)) {
                DrawArray("Team", ref source.Teams);
            }
            if (source.SourceType.HasFlag(SourceType.Project)) {
                DrawArray("Project", ref source.Projects);
            }
        }

        private static void DrawArray(string label, ref string[] values) {
            for (var i = 0; i < values.Length; ++i) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(label, GUILayout.Width(EditorGUIUtility.labelWidth - GUI.skin.box.padding.left));
                values[i] = GUILayout.TextField(values[i], GUILayout.ExpandWidth(true));
                if (values.Length > 1 && GUILayout.Button("-", GUILayout.Width(ButtonWidth))) {
                    values = values.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(ButtonWidth))) {
                values = values.Add("");
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
