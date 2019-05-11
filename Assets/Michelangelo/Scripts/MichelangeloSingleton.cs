using System.Collections;
using MessagePack.Resolvers;
using UnityEngine;

namespace Michelangelo.Scripts {
    internal class MichelangeloSingleton : MonoBehaviour {
        private const string MichelangeloSingletonName = "MichelangeloSingleton";

        private static MichelangeloSingleton instance;
        private static MichelangeloSingleton Instance => instance ? instance : instance = Construct();

        public static void Coroutine(IEnumerator coroutine) {
            Instance.StartCoroutine(coroutine);
        }

        private static MichelangeloSingleton Construct() {
            var gameObject = new GameObject { name = MichelangeloSingletonName, hideFlags = HideFlags.HideAndDontSave };

            SetupMessagePackResolvers();

            return gameObject.AddComponent<MichelangeloSingleton>();
        }

        private static void SetupMessagePackResolvers() {
            // CompositeResolver is singleton helper for use custom resolver.
            // Of course you can also make custom resolver.
            CompositeResolver.RegisterAndSetAsDefault(
                // use generated resolver first, and combine many other generated/custom resolvers
                GeneratedResolver.Instance,

                // finally, use builtin/primitive resolver(don't use StandardResolver, it includes dynamic generation)
                BuiltinResolver.Instance,
                AttributeFormatterResolver.Instance,
                PrimitiveObjectResolver.Instance);
        }
    }
}
