using System;
using UnityEngine;

namespace Michelangelo.Models {
    /// <summary>
    /// Holds information about Michelangelo user.
    /// </summary>
    [Serializable]
    public class UserInfo {
        /// <summary>
        /// Energy available to the user.
        /// </summary>
        public int energyAvailable;

        /// <summary>
        /// Maximum energy available to user.
        /// </summary>
        public int energyCapacity;

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string username;
        
        internal static UserInfo FromJson(string json) => JsonUtility.FromJson<UserInfo>(json);
    }
}
