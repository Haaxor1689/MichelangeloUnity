using System;
using System.Collections.Generic;
using System.Linq;

namespace Michelangelo.Models.Handlers {
    /// <summary>
    /// Restriction types for grammar rule sources.
    /// </summary>
    [Flags]
    public enum SourceType {
        /// <summary>
        /// Allows all sources.
        /// </summary>
        Unrestricted = 0,
        /// <summary>
        /// Restricts sources to only rules in grammars owned by requesting user.
        /// </summary>
        Mine = 1,
        /// <summary>
        /// Restricts sources to only rules in grammars owned by listed teams.
        /// </summary>
        Team = 2,
        /// <summary>
        /// Restricts sources to only rules in grammars owned by listed projects.
        /// </summary>
        Project = 4,
        /// <summary>
        /// Restricts to only rules from current grammar.
        /// </summary>
        All = ~0
    }

    /// <summary>
    /// Contains info about restrictions for possible rule origins.
    /// </summary>
    [Serializable]
    public class RestrictSource : IHandler {
        /// <summary>
        /// Flags field restricting sources.
        /// </summary>
        public SourceType SourceType;

        /// <summary>
        /// List of whitelisted teams to source from if Team flag is present in <see cref="SourceType"/>.
        /// </summary>
        public string[] Teams = new string[1];

        /// <summary>
        /// List of whitelisted projects to source from if Project flag is present in <see cref="SourceType"/>.
        /// </summary>
        public string[] Projects = new string[1];

        /// <inheritdoc />
        public string ToCode() {
            if (SourceType == 0) {
                return "";
            }
            var list = new List<string>();
            if (SourceType.HasFlag(SourceType.Mine)) {
                list.Add("Source.Mine");
            }
            if (SourceType.HasFlag(SourceType.Team)) {
                list.Add($"Source.Team({String.Join(", ", Teams.Select(s => $"\"{s}\""))})");
            }
            if (SourceType.HasFlag(SourceType.Project)) {
                list.Add($"Source.Project({String.Join(", ", Projects.Select(s => $"\"{s}\""))})");
            }
            return $".Restrict({String.Join(", ", list)})";
        }
    }
}
