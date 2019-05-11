using Michelangelo.Editor.Draw;
using Michelangelo.Scripts;
using UnityEditor;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(SceneObject))]
    internal class SceneObjectEditor : ObjectBaseEditor {
        private SceneObject Object => (SceneObject) target;

        protected override string GenerateButtonTooltip => Object.CanGenerate ? base.GenerateButtonTooltip : "You can't generate a scene with no valid goals or zero size.";

        protected override void RenderBody() {
            Object.Model.Draw();
            if (string.IsNullOrEmpty(Object.Model.Goal)) {
                EditorGUILayout.HelpBox("At least one valid goal is required. Please specify it first.", MessageType.Info);
            }
        }
    }
}
