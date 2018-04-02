using System;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public class Grammar {
        public string id;
        public string name;
        public string[] tags;
        public string type;
        public string code;
        public string lastModified;
        public bool isOwner;
        public bool shared;

        public static Grammar FromJson(string json) {
            return JsonUtility.FromJson<Grammar>(json);
        }

        public static Grammar[] FromJsonArray(string json) {
            return Michelangelo.Utility.JsonArray.FromJsonArray<Grammar>(json);
        }

        public new string ToString() {
            var builder = new System.Text.StringBuilder();
            builder.Append("Grammar ");
            builder.Append(name);
            builder.Append("(");
            builder.Append(type);
            builder.Append("), modified ");
            builder.Append(lastModified);
            return builder.ToString();
        }
    }
}