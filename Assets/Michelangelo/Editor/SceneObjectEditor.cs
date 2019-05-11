using System;
using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(SceneObject))]
    public class SceneObjectEditor : ObjectBaseEditor {
        private SceneObject Object => (SceneObject) target;

        protected override string GenerateButtonTooltip => Object.CanGenerate ? base.GenerateButtonTooltip : "You can't generate a scene with no valid goals or zero size.";

        protected override void RenderBody() {
            Object.Goal.Draw();
            if (string.IsNullOrEmpty(Object.Goal.Name)) {
                EditorGUILayout.HelpBox("At least one valid goal is required. Please specify it first.", MessageType.Info);
            }
        }
    }
}
