using System;
using Michelangelo.Model;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    public class Model : ObjectBase {

        private Goal goal;

        /// <summary>
        /// Generates new mesh for Michelangelo object asynchronously.
        /// </summary>
        /// <returns><see cref="IPromise"/> that contains mesh info and response message from server.</returns>
        public override IPromise<GenerateGrammarResponse> Generate() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructs new <see cref="GameObject"/> with correctly initialized <see cref="Model"/> with a goal.
        /// </summary>
        /// <param name="goal">Element goal that this model will generate.</param>
        /// <returns>Newly created <see cref="GameObject"/> with <see cref="Model"/> component initialized.</returns>
        public static GameObject Construct(Goal goal) {
            var newObject = new GameObject(goal.name);
            var michelangeloObject = newObject.AddComponent<Model>();
            michelangeloObject.goal = goal;
            return newObject;
        }
    }
}
