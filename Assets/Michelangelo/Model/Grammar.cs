using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public class Grammar {
        public static readonly Grammar Placeholder = new Grammar { name = "...", type = "..." };
        public string code;
        public string id;
        public bool isOwner;
        public string lastModified;
        public string name;
        public bool shared;
        public string[] tags;
        public string type;
        public bool isTutorial;

        private DateTime lastModifiedDate;
        public DateTime LastModifiedDate => lastModifiedDate != DateTime.MinValue ? lastModifiedDate : lastModifiedDate = Convert.ToDateTime(lastModified);

        private TextAsset sourceCode;
        public TextAsset SourceCode => sourceCode ? sourceCode : sourceCode = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine("Assets", SourceFilePathRelative));

        public string ClassName => name.ClassNameFriendly();
        public string FileName => $"{ClassName}Grammar.txt";
        public string SourceFilePathRelative => Path.Combine(Constants.GrammarCodeFolderRelative, FileName);
        public string SourceFilePath => Path.Combine(Constants.GrammarCodeFolder, FileName);

        public bool CreateSourceFile() {
            if (File.Exists(SourceFilePath)) {
                Debug.LogWarning($"Replacing old code file for \"{name}\".");
            }

            File.Copy(Path.Combine(Constants.GrammarCodeFolder, "_empty_Grammar.cs"), SourceFilePath);
            File.WriteAllText(SourceFilePath, code);
            AssetDatabase.Refresh();
            return true;
        }

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
