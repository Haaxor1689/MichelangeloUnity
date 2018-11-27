using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor.Utility {
    [CustomPropertyDrawer(typeof(ReadonlyFieldAttribute))]
    public class ReadonlyFieldDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property,label,true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}