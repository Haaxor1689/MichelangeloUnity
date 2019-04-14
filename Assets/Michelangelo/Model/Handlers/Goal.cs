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
        public RestrictSource[] RestrictSources = new RestrictSource[0];
        public Command[] With = new Command[0];

        public void Draw() {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(Name, EditorStyles.boldLabel);
            Name = EditorGUILayout.TextField("Name", Name);
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Restrict", EditorStyles.boldLabel);
            foreach (var r in RestrictSources) {
                r.Draw();
            }
            if (GUILayout.Button("+")) {
                RestrictSources = RestrictSources.Add(new RestrictSource());
            }
            EditorGUILayout.EndVertical();
        }

        public string ToCode() => $"new Model(\"{Name}\"){ArrToCode("With", With)}{ArrToCode("Restrict", RestrictSources)};";

        private static string ArrToCode(string name, ICollection<IHandler> arr) => arr.Count > 1 ? $".{name}({string.Join(", ", arr.Select(r => r.ToCode()))})" : "";
    }
}
