using System.Collections;
using UnityEngine;

namespace Michelangelo.Scripts {
    public class MichelangeloSingleton : MonoBehaviour {
        private const string MichelangeloSingletonName = "MichelangeloSingleton";

        private static MichelangeloSingleton instance;
        private static MichelangeloSingleton Instance => instance ? instance : (instance = Construct());

        public static void Coroutine(IEnumerator coroutine) {
            Instance.StartCoroutine(coroutine);
        }
        
        private static MichelangeloSingleton Construct() {
            var gameObject = new GameObject {
                name = MichelangeloSingletonName,
                hideFlags = HideFlags.HideAndDontSave
            };
            return gameObject.AddComponent<MichelangeloSingleton>();
        }
    }
}
