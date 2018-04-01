using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
	public class MichelangeloEditorWindow : EditorWindow {
		private static string email;
		private static string password;

		[MenuItem("Window/Michelangelo")]
		public static void ShowWindow() {
			GetWindow<MichelangeloEditorWindow>("Michelangelo");
		}

		private void OnGUI() {
			GUILayout.Label("Log in.");
			email = EditorGUILayout.TextField("User name or Email", email);
			password = EditorGUILayout.PasswordField("Password", password);
			if (GUILayout.Button("Log In")) {
				Michelangelo.WebAPI.Login(email, password);
			}
		}
	}
}