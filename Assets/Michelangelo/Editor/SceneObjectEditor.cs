using Michelangelo.Scripts;
using UnityEditor;

namespace Michelangelo.Editor {
    [CustomEditor(typeof(SceneObject))]
    public class SceneObjectEditor : ObjectBaseEditor {
        private new SceneObject Object => (SceneObject) target;

        protected override void RenderBody() {
            Object.Goal.Draw();
        }
    }
}
