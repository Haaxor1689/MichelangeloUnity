using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor.Utility {
    internal static class LinkLabel {
        private static GUIStyle LinkStyle { get; }

        static LinkLabel() {
            LinkStyle = new GUIStyle(EditorStyles.label) {
                wordWrap = false,
                normal = {
                    textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f)
                },
                stretchWidth = false
            };
        }

        public static bool Draw(string text, params GUILayoutOption[] options) => Draw(new GUIContent(text), options);

        private static bool Draw(GUIContent label, params GUILayoutOption[] options) {
            var position = GUILayoutUtility.GetRect(label, LinkStyle, options);
         
            Handles.BeginGUI();
            Handles.color = LinkStyle.normal.textColor;
            Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
            Handles.color = Color.white;
            Handles.EndGUI();
         
            EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);
         
            return GUI.Button(position, label, LinkStyle);
        }
    }
}
