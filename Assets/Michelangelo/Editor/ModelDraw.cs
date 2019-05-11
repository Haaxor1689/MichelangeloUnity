using System;
using Michelangelo.Models.Handlers;
using UnityEditor;

namespace Michelangelo.Editor {
    internal static class ModelDraw {
        public static void Draw(this Model model) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(String.IsNullOrEmpty(model.Goal) ? "Invalid goal" : $"Goal: {model.Goal}", EditorStyles.boldLabel);
            model.Goal = EditorGUILayout.TextField("Goal", model.Goal);
            model.Size = EditorGUILayout.Vector3Field("Size", model.Size);
            model.CenterOnPivot = EditorGUILayout.Toggle("Center on pivot", model.CenterOnPivot);
            
            EditorGUILayout.Space();
            model.RestrictSources.Draw();
            EditorGUILayout.EndVertical();
        }
    }
}
