using System;
using UnityEngine;

namespace Michelangelo.Model {
	[Serializable]
	public class UserInfo {
		public string username;
		public int energyAvailable;
		public int energyCapacity;

		public static UserInfo FromJson(string json) {
			return JsonUtility.FromJson<UserInfo>(json);
		}

		public new string ToString() {
			var builder = new System.Text.StringBuilder();
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