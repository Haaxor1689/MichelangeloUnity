using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model.Handlers {
    [Serializable]
    public class Goal : IHandler {
        public string Name;
        public RestrictSource RestrictSources;
        public Command[] With = new Command[0];

        public void Draw() {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(String.IsNullOrEmpty(Name) ? "Invalid goal" : $"Goal: {Name}", EditorStyles.boldLabel);
            Name = EditorGUILayout.TextField("Goal", Name);
            
            EditorGUILayout.Space();
            RestrictSources.Draw();
            EditorGUILayout.EndVertical();
        }

        public string ToCode() => $"new Model(\"{Name}\"){ArrToCode("With", With)}{RestrictSources.ToCode()};";

        private static string ArrToCode(string name, ICollection<IHandler> arr) => arr.Count > 1 ? $".{name}({string.Join(", ", arr.Select(r => r.ToCode()))})" : "";
    }
}
