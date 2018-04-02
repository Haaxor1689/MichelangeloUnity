using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
	public class MichelangeloEditorWindow : EditorWindow {
		private static bool isLoading = false;

		private static string loginEmail;
		private static string loginPassword;

		private static Model.UserInfo user;

		private static string errorMessage;

		[MenuItem("Window/Michelangelo")]
		public static void ShowWindow() {
			GetWindow<MichelangeloEditorWindow>("Michelangelo");
		}

		private void OnGUI() {
			if (isLoading) {
				EditorGUILayout.LabelField("Please wait...", EditorStyles.boldLabel);
				return;
			}
			if (Michelangelo.Session.WebAPI.IsLoggedIn) {
				LoggedIn();
			} else {
				NotLoggedIn();
			}
			if (errorMessage != null) {
				GUILayout.Label(errorMessage);
			}
		}

		private void LoggedIn() {
			EditorGUILayout.LabelField("User:", user.username, EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Energy:", user.energyAvailable + "/" + user.energyCapacity, EditorStyles.boldLabel);
			if (GUILayout.Button("Log Out")) {
				isLoading = true;
				Repaint();
				Michelangelo.Session.WebAPI.Logout(error => {
					if (error != null) {
						errorMessage = error;
						isLoading = false;
						Repaint();
						return;
					}
					isLoading = false;
					Repaint();
				});
			}
			if (GUILayout.Button("Update User Info")) {
				Michelangelo.Session.WebAPI.GetUserInfo((response, error) => {
					if (error != null) {
						errorMessage = error;
						return;
					}
					user = response;
				});
			}
			if (GUILayout.Button("Get Main Page")) {
				Michelangelo.Session.WebAPI.GetMainPage(error => {
					if (error != null) {
						errorMessage = error;
						return;
					}
				});
			}
			if (GUILayout.Button("Create Grammar")) {
				Michelangelo.Session.WebAPI.CreateGrammar((response, error) => {
					if (error != null) {
						errorMessage = error;
						return;
					}
				});
			}
			if (GUILayout.Button("Get Grammar")) {
				Michelangelo.Session.WebAPI.GetGrammar((response, error) => {
					if (error != null) {
						errorMessage = error;
						return;
					}
				});
			}
			if (GUILayout.Button("Get Shared")) {
				Michelangelo.Session.WebAPI.GetShared((response, error) => {
					if (error != null) {
						errorMessage = error;
						return;
					}
				});
			}
			if (GUILayout.Button("Get Tutorial")) {
				Michelangelo.Session.WebAPI.GetTutorials((response, error) => {
					if (error != null) {
						errorMessage = error;
						return;
					}

				});
			}
		}

		private void NotLoggedIn() {
			EditorGUILayout.LabelField("Log In", EditorStyles.boldLabel);
			loginEmail = EditorGUILayout.TextField("User name or Email", loginEmail);
			loginPassword = EditorGUILayout.PasswordField("Password", loginPassword);
			if (GUILayout.Button("Log In")) {
				isLoading = true;
				Repaint();
				Michelangelo.Session.WebAPI.Login(loginEmail, loginPassword, error => {
					loginEmail = null;
					loginPassword = null;
					if (error != null) {
						errorMessage = error;
						isLoading = false;
						Repaint();
						return;
					}

					Michelangelo.Session.WebAPI.GetUserInfo((response, _) => {
						user = response;
						isLoading = false;
						Repaint();
					});
				});
			}
		}
	}
}