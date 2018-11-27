using System;
using UnityEngine;

// Disable warning for unused array variable
#pragma warning disable 0649
namespace Michelangelo.Utility {
    public static class JsonArray {
        private const string Prefix = "{\"array\":";

        public static T[] FromJsonArray<T>(string json) {
            var newJson = Prefix + json + "}";
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }
        
        public static string ToJsonArray<T>(T[] data) {
            var wrapper = new Wrapper<T> { array = data };
            var newJson = JsonUtility.ToJson(wrapper);
            return newJson.Substring(Prefix.Length, newJson.Length - Prefix.Length - 1);
        }
        
        [Serializable]
        private class Wrapper<T> {
            public T[] array;
        }
    }
}
#pragma warning restore 0649
