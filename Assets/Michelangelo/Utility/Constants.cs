using System.IO;
using UnityEngine;

namespace Michelangelo.Utility {
    public static class Constants {
        public static string GrammarCodeFolderRelative => Path.Combine("Michelangelo", "GrammarSources");
        public static string GrammarCodeFolder => Path.Combine(Application.dataPath, GrammarCodeFolderRelative);

        public const string EditorPrefsPrefix = "Michelangelo_";
    }
}
