using Michelangelo.Models;
using Michelangelo.Models.Handlers;
using RSG;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Scripts {
    /// <summary>
    ///   Unity component that represents a Michelangelo scene object in scene.
    /// </summary>
    public class SceneObject : ObjectBase {
        /// <summary>
        ///   Model describing goal of this scene object.
        /// </summary>
        public Model Model;

        /// <inheritdoc />
        public override bool CanGenerate => !string.IsNullOrWhiteSpace(Model.Goal) && Model.Size.x > 0 && Model.Size.y > 0 && Model.Size.z > 0;

        /// <inheritdoc />
        protected override IPromise<GenerateGrammarResponse> GenerateCallback() => MichelangeloSession.GenerateScene(Model.ToCode());

        [MenuItem("Michelangelo/SceneObject", false, 12)]
        internal static void ShowWindow() {
            var obj = new GameObject("New Scene Object", typeof(SceneObject));
            Selection.objects = new Object[] { obj };
            Selection.activeObject = obj;
        }

        /// <summary>
        ///   Constructs new <see cref="GameObject" /> with correctly initialized <see cref="SceneObject" /> with a model.
        /// </summary>
        /// <param name="model">Element model that this scene object will generate.</param>
        /// <returns>Newly created <see cref="GameObject" /> with <see cref="SceneObject" /> component initialized.</returns>
        public static GameObject Construct(Model model) {
            var newObject = new GameObject(model.Goal);
            var michelangeloObject = newObject.AddComponent<SceneObject>();
            michelangeloObject.Model = model;
            return newObject;
        }
    }
}
