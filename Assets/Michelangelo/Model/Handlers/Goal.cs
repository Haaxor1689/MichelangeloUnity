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
        public Vector3 Size = Vector3.one;
        public bool CenterOnPivot = false;

        public void Draw() {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(String.IsNullOrEmpty(Name) ? "Invalid goal" : $"Goal: {Name}", EditorStyles.boldLabel);
            Name = EditorGUILayout.TextField("Goal", Name);
            Size = EditorGUILayout.Vector3Field("Size", Size);
            CenterOnPivot = EditorGUILayout.Toggle("Center on pivot", CenterOnPivot);
            
            EditorGUILayout.Space();
            RestrictSources.Draw();
            EditorGUILayout.EndVertical();
        }

        public string ToCode() => $"new Model(\"{Name}\").With(Size{Size.ToString()}, Position{(CenterOnPivot ? new Vector3(-Size.x / 2, 0, -Size.z / 2).ToString() : Vector3.zero.ToString())}){RestrictSources.ToCode()};";
    }
}
