using UnityEngine;

// Disable warning for unused array variable
#pragma warning disable 0649
namespace Michelangelo.Utility {
    public class JsonArray {
        public static T[] FromJsonArray<T>(string json) {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T> {
            public T[] array;
        }
    }
}
#pragma warning restore 0649