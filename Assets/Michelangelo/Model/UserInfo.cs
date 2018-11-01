using System;
using System.Text;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public class UserInfo {
        public int energyAvailable;
        public int energyCapacity;
        public string username;
        
        public static UserInfo FromJson(string json) => JsonUtility.FromJson<UserInfo>(json);

        public new string ToString() {
            var builder = new StringBuilder();
            builder.Append("[Username: ");
            builder.Append(username);
            builder.Append(", Energy: ");
            builder.Append(energyAvailable);
            builder.Append("/");
            builder.Append(energyCapacity);
            builder.Append("]");
            return builder.ToString();
        }
    }
}
