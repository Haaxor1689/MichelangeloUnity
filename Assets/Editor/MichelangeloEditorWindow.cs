using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
	public class MichelangeloEditorWindow : EditorWindow {
		private static bool isLoading = false;
		private static bool isLoggedIn = false;

		private static string loginEmail;
		private static string loginPassword;

		private static string errorMessage;

		private static UserInfo user;

		private static Grammar[] grammar;
		private static Grammar[] shared;
		private static Grammar[] tutorial;

		private static Vector2 grammarScrollPos;
		private static Vector2 sharedScrollPos;
		private static Vector2 tutorialScrollPos;

		[MenuItem("Window/Michelangelo")]
		public static void ShowWindow() {
			GetWindow<MichelangeloEditorWindow>("Michelangelo");
		}

		private void OnGUI() {
			if (isLoading) {
				EditorGUILayout.LabelField("Please wait...", EditorStyles.boldLabel);
				return;
			}
			if (Michelangelo.Session.WebAPI.IsAuthenticated) {
				if (!isLoggedIn) {
					isLoading = true;
					UpdateUserInfo();
					return;
				}
				LoggedIn();
			} else {
				NotLoggedIn();
			}
			if (errorMessage != null) {
				var style = new GUIStyle(EditorStyles.textField);
				style.normal.textColor = Color.red;

				GUILayout.Label(errorMessage, style);
			}
		}

		private void LoggedIn() {
			EditorGUILayout.LabelField("User:", user.username, EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Energy:", user.energyAvailable + "/" + user.energyCapacity, EditorStyles.boldLabel);
			if (GUILayout.Button("Log Out")) {
				errorMessage = null;
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
					isLoggedIn = false;
					Repaint();
				});
			}
			if (GUILayout.Button("Create Grammar")) {
				errorMessage = null;
				Michelangelo.Session.WebAPI.CreateGrammar((response, error) => {
					if (error != null) {
						errorMessage = error;
						return;
					}
					if (grammar != null) {
						var list = grammar.ToList();
						list.Add(response);
						grammar = list.ToArray();
					} else {
						grammar = new Grammar[] { response };
					}
				});
			}
			GUILayout.Space(20.0f);
			PrintGrammarArray(grammar, "Your designs", ref grammarScrollPos);
			GUILayout.Space(20.0f);
			PrintGrammarArray(shared, "Shared by others", ref sharedScrollPos);
			GUILayout.Space(20.0f);
			PrintGrammarArray(tutorial, "Tutorials and Reference", ref tutorialScrollPos);
		}

		private void NotLoggedIn() {
			EditorGUILayout.LabelField("Log In", EditorStyles.boldLabel);
			loginEmail = EditorGUILayout.TextField("User name or Email", loginEmail);
			loginPassword = EditorGUILayout.PasswordField("Password", loginPassword);
			if (GUILayout.Button("Log In")) {
				errorMessage = null;
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
					UpdateUserInfo();
				});
			}
		}

		private void PrintGrammarArray(Grammar[] grammarArray, string title, ref Vector2 scrollPos) {
			if (grammarArray == null) {
				return;
			}

			EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));
			foreach (var item in grammarArray) {
				EditorGUILayout.LabelField(item.name, item.type + " " + item.lastModified);
			}
			EditorGUILayout.EndScrollView();
		}

		private void UpdateUserInfo() {
			Michelangelo.Session.WebAPI.GetUserInfo((response, _) => {
				user = response;
				isLoading = false;
				isLoggedIn = true;
				Repaint();
				UpdateGrammarArray();
				UpdateSharedArray();
				UpdateTutorialArray();
			});
		}

		private void UpdateGrammarArray() {
			errorMessage = null;
			Michelangelo.Session.WebAPI.GetGrammar((response, error) => {
				if (error != null) {
					errorMessage = error;
					return;
				}
				grammar = response;
				Repaint();
			});
		}

		private void UpdateSharedArray() {
			errorMessage = null;
			Michelangelo.Session.WebAPI.GetShared((response, error) => {
				if (error != null) {
					errorMessage = error;
					return;
				}
				shared = response;
				Repaint();
			});
		}

		private void UpdateTutorialArray() {
			errorMessage = null;
			Michelangelo.Session.WebAPI.GetTutorials((response, error) => {
				if (error != null) {
					errorMessage = error;
					return;
				}
				tutorial = response;
				Repaint();
			});
		}
	}
}