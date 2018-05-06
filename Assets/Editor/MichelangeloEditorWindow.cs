using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
	public class MichelangeloEditorWindow : EditorWindow {
		private string loginEmail;
		private string loginPassword;

		private string errorMessage;

		private Vector2 grammarScrollPos;
		private Vector2 sharedScrollPos;
		private Vector2 tutorialScrollPos;

		[MenuItem("Window/Michelangelo")]
		public static void ShowWindow() {
			GetWindow<MichelangeloEditorWindow>("Michelangelo");
		}

		private void OnEnable() {
			MichelangeloSession.taskDone += TaskDone;
		}

		private void OnDisable() {
			MichelangeloSession.taskDone -= TaskDone;
		}

		private void OnGUI() {
			if (MichelangeloSession.isLoading) {
				EditorGUILayout.LabelField("Please wait...", EditorStyles.boldLabel);
				return;
			}
			if (Michelangelo.Session.WebAPI.IsAuthenticated) {
				if (!MichelangeloSession.isLoggedIn || MichelangeloSession.user == null) {
					MichelangeloSession.UpdateUserInfo();
					return;
				}

				LoggedIn();
			} else {
				NotLoggedIn();
			}
			if (errorMessage != null && errorMessage != "") {
				var style = new GUIStyle(EditorStyles.textField);
				style.normal.textColor = Color.red;

				GUILayout.Space(20.0f);
				GUILayout.Label(errorMessage, style);
			}
		}

		private void LoggedIn() {
			EditorGUILayout.LabelField("User:", MichelangeloSession.user.username, EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Energy:", MichelangeloSession.user.energyAvailable + "/" + MichelangeloSession.user.energyCapacity, EditorStyles.boldLabel);
			if (GUILayout.Button("Log Out")) {
				MichelangeloSession.LogOut();
			}
			if (GUILayout.Button("Create Grammar")) {
				MichelangeloSession.CreateGrammar();
			}
			GUILayout.Space(20.0f);
			PrintGrammarArray(MichelangeloSession.myGrammar, "Your designs", ref grammarScrollPos);
			GUILayout.Space(20.0f);
			PrintGrammarArray(MichelangeloSession.sharedGrammar, "Shared by others", ref sharedScrollPos);
			GUILayout.Space(20.0f);
			PrintGrammarArray(MichelangeloSession.tutorialGrammar, "Tutorials and Reference", ref tutorialScrollPos);
			GUILayout.Space(20.0f);
			if (GUILayout.Button("Refresh")) {
				MichelangeloSession.UpdateUserInfo();
			}
		}

		private void NotLoggedIn() {
			EditorGUILayout.LabelField("Log In", EditorStyles.boldLabel);
			loginEmail = EditorGUILayout.TextField("User name or Email", loginEmail);
			loginPassword = EditorGUILayout.PasswordField("Password", loginPassword);
			if (GUILayout.Button("Log In")) {
				MichelangeloSession.LogIn(loginEmail, loginPassword);
			}
		}

		private void PrintGrammarArray(List<Grammar> grammarArray, string title, ref Vector2 scrollPos) {
			EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
			if (grammarArray == null || grammarArray.Count == 0) {
				EditorGUILayout.LabelField("Loading...");
				return;
			}
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(Mathf.Min(90, 23 * grammarArray.Count)));
			foreach (var item in grammarArray) {
				EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
				EditorGUILayout.LabelField(item.name);
				if (GUILayout.Button("Instantiate")) {
					MichelangeloSession.InstantiateGrammar(item.id);
				}
				if (item.isOwner && GUILayout.Button("X")) {
					MichelangeloSession.DeleteGrammar(item.id);
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndScrollView();
		}

		private void TaskDone(string error) {
			errorMessage = error;
			Repaint();
		}
	}
}