using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model.Handlers {
    [Flags]
    public enum SourceType {
        Unrestricted = 0,
        Mine = 1,
        Team = 2,
        Project = 4,
        All = ~0
    }

    [Serializable]
    public class RestrictSource : IHandler {
        public SourceType SourceType;
        public string[] Teams = new string[1];
        public string[] Projects = new string[1];

        public const int ButtonWidth = 40;

        public void Draw() {
            EditorGUILayout.LabelField("Restrict", EditorStyles.boldLabel);
            SourceType = (SourceType) EditorGUILayout.EnumFlagsField("Source", SourceType);
            if (SourceType.HasFlag(SourceType.Team)) {
                DrawArray("Team", ref Teams);
            }
            if (SourceType.HasFlag(SourceType.Project)) {
                DrawArray("Project", ref Projects);
            }
        }

        private void DrawArray(string label, ref string[] values) {
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

        public string ToCode() {
            if (SourceType == 0) {
                return "";
            }
            var list = new List<string>();
            if (SourceType.HasFlag(SourceType.Mine)) {
                list.Add("Source.Mine");
            }
            if (SourceType.HasFlag(SourceType.Team)) {
                list.Add($"Source.Team({String.Join(", ", Teams.Select(s => $"\"{s}\""))})");
            }
            if (SourceType.HasFlag(SourceType.Project)) {
                list.Add($"Source.Project({String.Join(", ", Projects.Select(s => $"\"{s}\""))})");
            }
            return $".Restrict({String.Join(", ", list)})";
        }
    }
}
