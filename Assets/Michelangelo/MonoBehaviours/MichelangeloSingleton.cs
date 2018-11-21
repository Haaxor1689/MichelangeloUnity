using System.Collections;
using UnityEngine;

namespace Michelangelo.MonoBehaviours {
    public class MichelangeloSingleton : MonoBehaviour {
        private static MichelangeloSingleton instance;
        private static bool isApplicationQuitting;

        public static void Coroutine(IEnumerator coroutine) {
            instance.StartCoroutine(coroutine);
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if (instance != null) {
                return;
            }

            var gameObject = new GameObject("MichelangeloSingleton");
            instance = gameObject.AddComponent<MichelangeloSingleton>();
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit() {
            isApplicationQuitting = true;
        }

        private void OnDestroy() {
            if (isApplicationQuitting) {
                return;
            }
            instance = null;
            Init();
        }
    }
}
