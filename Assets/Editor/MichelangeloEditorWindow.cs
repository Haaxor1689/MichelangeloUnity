using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
	public class MichelangeloEditorWindow : EditorWindow {
		private static string email;
		private static string password;
		private static string errorMessage;

		[MenuItem("Window/Michelangelo")]
		public static void ShowWindow() {
			GetWindow<MichelangeloEditorWindow>("Michelangelo");
		}

		private void OnGUI() {
			if (Michelangelo.Session.WebAPI.IsLoggedIn) {
				LoggedIn();
			} else {
				NotLoggedIn();
			}
		}

		private void LoggedIn() {
			if (GUILayout.Button("Log Out")) {
				try {
					Michelangelo.Session.WebAPI.Logout();
				} catch (Michelangelo.Utility.ResponseParseException ex) {
					errorMessage = ex.Message;
				}
			}
			if (GUILayout.Button("Get Main Page")) {
				try {
					Michelangelo.Session.WebAPI.GetMainPage();
				} catch (Michelangelo.Utility.ResponseParseException ex) {
					errorMessage = ex.Message;
				}
			}
			if (GUILayout.Button("Create grammar")) {
				try {
					Michelangelo.Session.WebAPI.CreateGrammar();
				} catch (Michelangelo.Utility.ResponseParseException ex) {
					errorMessage = ex.Message;
				}
			}
		}

		private void NotLoggedIn() {
			EditorGUILayout.LabelField("Log In", EditorStyles.boldLabel);
			email = EditorGUILayout.TextField("User name or Email", email);
			password = EditorGUILayout.PasswordField("Password", password);
			if (GUILayout.Button("Log In")) {
				try {
					Michelangelo.Session.WebAPI.Login(email, password);
				} catch (Michelangelo.Utility.ResponseParseException ex) {
					errorMessage = ex.Message;
				}
			}
			if (errorMessage != null) {
				GUILayout.Label(errorMessage);
			}
		}
	}
}