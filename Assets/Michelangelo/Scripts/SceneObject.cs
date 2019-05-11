using System;
using Michelangelo.Model;
using Michelangelo.Model.Handlers;
using RSG;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Michelangelo.Scripts {
    public class SceneObject : ObjectBase {

        public Goal Goal;

        public override bool CanGenerate => !String.IsNullOrWhiteSpace(Goal.Name) && Goal.Size.x > 0 && Goal.Size.y > 0 && Goal.Size.z > 0;
        
        protected override IPromise<GenerateGrammarResponse> GenerateCallback() => MichelangeloSession.GenerateScene(Goal.ToCode());

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
        public static GameObject Construct(Goal goal) {
            var newObject = new GameObject(goal.Name);
            var michelangeloObject = newObject.AddComponent<SceneObject>();
            michelangeloObject.Goal = goal;
            return newObject;
        }
    }
}
