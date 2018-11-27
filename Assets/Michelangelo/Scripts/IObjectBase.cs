using Michelangelo.Model;
using RSG;
using UnityEngine;

namespace Michelangelo.Scripts {
    public interface IObjectBase {
        bool IsInEditMode { get; }
        GameObject gameObject { get; }

        IPromise<GenerateGrammarResponse> Generate();
    }
}