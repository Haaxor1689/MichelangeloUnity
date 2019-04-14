using System;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model.Handlers {
    public enum SourceType {
        This,
        Mine,
        Team,
        Project
    }

    [Serializable]
    public class RestrictSource : IHandler {
        public SourceType SourceType;
        public string[] Values = new string[1];

        public const int ButtonWidth = 40;

        public bool ShouldHaveValue => SourceType == SourceType.Team || SourceType == SourceType.Project;

        public void Draw() {
            SourceType = (SourceType) EditorGUILayout.EnumPopup("Source", SourceType);
            if (!ShouldHaveValue) {
                return;
            }
            for (var i = 0; i < Values.Length; ++i) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(SourceType.ToString(), GUILayout.Width(EditorGUIUtility.labelWidth - GUI.skin.box.padding.left));
                GUI.SetNextControlName("NameFilter");
                Values[i] = GUILayout.TextField(Values[i], GUILayout.ExpandWidth(true));
                if (Values.Length > 1 && GUILayout.Button("-", GUILayout.Width(ButtonWidth))) {
                    Values = Values.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(ButtonWidth))) {
                Values = Values.Add("");
            }
            EditorGUILayout.EndHorizontal();
        }

        public string ToCode() => $"Source.{SourceType}{(ShouldHaveValue ? $"(\"{string.Join(", ", Values)}\")" : "")}";
    }
}
