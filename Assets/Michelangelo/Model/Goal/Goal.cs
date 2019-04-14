using System;
using UnityEditor;

namespace Michelangelo.Model {
    [Serializable]
    public class Goal {
        public string Name;

        public void Draw() {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(Name, EditorStyles.boldLabel);
            Name = EditorGUILayout.TextField("Name", Name);
            EditorGUILayout.EndVertical();
        }

        public string ToCode() => $"new Model(\"{Name}\");";
    }
}
