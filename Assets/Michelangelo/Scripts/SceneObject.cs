using System;
using Michelangelo.Models;
using Michelangelo.Models.Handlers;
using RSG;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Michelangelo.Scripts {
    public class SceneObject : ObjectBase {

        public Model Model;

        public override bool CanGenerate => !String.IsNullOrWhiteSpace(Model.Goal) && Model.Size.x > 0 && Model.Size.y > 0 && Model.Size.z > 0;
        
        protected override IPromise<GenerateGrammarResponse> GenerateCallback() => MichelangeloSession.GenerateScene(Model.ToCode());

        [MenuItem("Michelangelo/SceneObject", false, 12)]
        public static void ShowWindow() {
            var obj = new GameObject("New Scene Object", typeof(SceneObject));
            Selection.objects = new Object[] { obj };
            Selection.activeObject = obj;
        }

        /// <summary>
        /// Constructs new <see cref="GameObject"/> with correctly initialized <see cref="SceneObject"/> with a goal.
        /// </summary>
        /// <param name="goal">Element goal that this model will generate.</param>
        /// <returns>Newly created <see cref="GameObject"/> with <see cref="SceneObject"/> component initialized.</returns>
        public static GameObject Construct(Models.Handlers.Model goal) {
            var newObject = new GameObject(goal.Goal);
            var michelangeloObject = newObject.AddComponent<SceneObject>();
            michelangeloObject.Model = goal;
            return newObject;
        }
    }
}
