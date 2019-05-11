using System;
using System.IO;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Models {
    /// <summary>
    /// Class containing info and metadata about a Michelangelo grammar.
    /// </summary>
    [Serializable]
    public class Grammar {
        private static readonly string GrammarCodeFolderRelative = Path.Combine("Michelangelo", "GrammarSources");
        private static readonly string GrammarCodeFolder = Path.Combine(Application.dataPath, GrammarCodeFolderRelative);

        /// <summary>
        /// Placeholder grammar that is used instead of null.
        /// </summary>
        public static readonly Grammar Placeholder = new Grammar { name = "...", type = "..." };

        /// <summary>
        /// Raw code of the grammar. Can be empty if the grammar wasn't downloaded whole.
        /// </summary>
        public string code;

        /// <summary>
        /// Unique id of grammar.
        /// </summary>
        public string id;

        /// <summary>
        /// True if user that requested it is it's owner.
        /// </summary>
        public bool isOwner;

        /// <summary>
        /// Name of the grammar.
        /// </summary>
        public string name;

        /// <summary>
        /// True if the grammar is publicly shared in cloud.
        /// </summary>
        public bool shared;

        /// <summary>
        /// Tags of grammar. Not implemented.
        /// </summary>
        public string[] tags;

        /// <summary>
        /// Type of syntax the grammar is using. Only DOG is available.
        /// </summary>
        public string type;

        /// <summary>
        /// True if grammar is a tutorial grammar.
        /// </summary>
        public bool isTutorial;
        
        /// <summary>
        /// Last modified date saved in raw string.
        /// </summary>
        public string lastModified;

        /// <summary>
        /// Date of when was the grammar last modified.
        /// </summary>
        public DateTime LastModifiedDate => lastModifiedDate != DateTime.MinValue ? lastModifiedDate : lastModifiedDate = Convert.ToDateTime(lastModified);
        private DateTime lastModifiedDate;

        /// <summary>
        /// Reference to grammar's source code asset.
        /// </summary>
        public TextAsset SourceCode => sourceCode ? sourceCode : sourceCode = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine("Assets", SourceFilePathRelative));
        private TextAsset sourceCode;

        private string ClassName => name.ClassNameFriendly();
        private string FileName => $"{ClassName}Grammar.txt";
        private string SourceFilePathRelative => Path.Combine(GrammarCodeFolderRelative, FileName);

        /// <summary>
        /// File path to the source code file.
        /// </summary>
        public string SourceFilePath => Path.Combine(GrammarCodeFolder, FileName);

        internal static Grammar FromJSON(string json) => JsonUtility.FromJson<Grammar>(json);
        internal static Grammar[] FromJSONArray(string json) => JsonArray.FromJsonArray<Grammar>(json);
    }
}
