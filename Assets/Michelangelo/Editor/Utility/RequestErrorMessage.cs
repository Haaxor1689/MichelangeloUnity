using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Utility {
    internal static class RequestErrorMessage {
        private static readonly Regex MatchRegex = new Regex(" request error:\\n");
        private static readonly GUIStyle TitleStyle = new GUIStyle { normal = { textColor = Color.red }, fontStyle = FontStyle.Bold };

        public static bool IsRequestError(string message) => !string.IsNullOrEmpty(message) && (MatchRegex.Match(message).Success || !message.Contains("\t"));

        public static void Draw(ref string message) {
            var split = message.Split('\n');
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            EditorGUILayout.LabelField(split.Length > 1 ? split[0] : "An error occured:", TitleStyle);
            if (GUILayout.Button("Clear")) {
                message = null;
                return;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField(split.Length > 1 ? split[1] : split[0], EditorStyles.wordWrappedLabel);
            EditorGUILayout.EndVertical();
        }
    }
}
