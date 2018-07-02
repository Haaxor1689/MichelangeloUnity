using System;
using System.IO;
using System.Text;
using Michelangelo.GrammarSources;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public class Grammar {
        public static readonly Grammar Placeholder = new Grammar { name = "..." };
        public string code;
        public string id;
        public bool isOwner;
        public string lastModified;
        public string name;
        public bool shared;
        public string[] tags;
        public string type;

        private DateTime lastModifiedDate;
        public DateTime LastModifiedDate => lastModifiedDate != DateTime.MinValue ? lastModifiedDate : (lastModifiedDate = Convert.ToDateTime(lastModified));

        public string ClassName => name.ClassNameFriendly();
        public string SourceFilePathRelative => Path.Combine(Constants.GrammarCodeFolderRelative, $"{ClassName}Grammar.cs");
        public string SourceFilePath => Path.Combine(Constants.GrammarCodeFolder, $"{ClassName}Grammar.cs");
        public GrammarSourceBase SourceFile => AssetDatabase.LoadAssetAtPath<GrammarSourceBase>(SourceFilePathRelative);

        public static Grammar FromJSON(string json) => JsonUtility.FromJson<Grammar>(json);
        public static Grammar[] FromJSONArray(string json) => JsonArray.FromJsonArray<Grammar>(json);

        public new string ToString() {
            var builder = new StringBuilder();
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
