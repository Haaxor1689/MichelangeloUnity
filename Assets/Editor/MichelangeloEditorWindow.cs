using System;
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

		private bool isLoading;
		private string errorMessage;

		private Vector2 grammarScrollPos;
		private Vector2 sharedScrollPos;
		private Vector2 tutorialScrollPos;

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
				if (!MichelangeloSession.isLoggedIn || MichelangeloSession.user == null) {
					MichelangeloSession.UpdateUserInfo().Then(_ => {
						Repaint();
					}).Catch(HandleError);
					RefreshAllArrays();
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
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label(errorMessage, style);
				if (GUILayout.Button("X")) {
					errorMessage = null;
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		private void LoggedIn() {
			EditorGUILayout.LabelField("User:", MichelangeloSession.user.username, EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Energy:", MichelangeloSession.user.energyAvailable + "/" + MichelangeloSession.user.energyCapacity, EditorStyles.boldLabel);
			if (GUILayout.Button("Log Out")) {
				isLoading = true;
				MichelangeloSession.LogOut().Then(() => {
					isLoading = false;
					Repaint();
				}).Catch(HandleError);
			}
			if (GUILayout.Button("Create Grammar")) {
				MichelangeloSession.CreateGrammar().Then(_ => {
					Repaint();
				}).Catch(HandleError);
			}
			GUILayout.Space(20.0f);
			PrintGrammarArray(MichelangeloSession.myGrammar, "Your designs", ref grammarScrollPos);
			GUILayout.Space(20.0f);
			PrintGrammarArray(MichelangeloSession.sharedGrammar, "Shared by others", ref sharedScrollPos);
			GUILayout.Space(20.0f);
			PrintGrammarArray(MichelangeloSession.tutorialGrammar, "Tutorials and Reference", ref tutorialScrollPos);
			GUILayout.Space(20.0f);
			if (GUILayout.Button("Refresh")) {
				isLoading = true;
				MichelangeloSession.UpdateUserInfo().Then(_ => isLoading = false).Catch(HandleError);
				RefreshAllArrays();
			}
		}

		private void NotLoggedIn() {
			EditorGUILayout.LabelField("Log In", EditorStyles.boldLabel);
			loginEmail = EditorGUILayout.TextField("User name or Email", loginEmail);
			loginPassword = EditorGUILayout.PasswordField("Password", loginPassword);
			if (GUILayout.Button("Log In")) {
				isLoading = true;
				MichelangeloSession.LogIn(loginEmail, loginPassword).Then(_ => {
					isLoading = false;
					Repaint();
					RefreshAllArrays();
				}).Catch(HandleError);
			}
		}

		private void RefreshAllArrays() {
			RSG.Promise<Grammar[]>.All(
				MichelangeloSession.UpdateMyGrammarArray(),
				MichelangeloSession.UpdateSharedArray(),
				MichelangeloSession.UpdateTutorialArray()).Then(_ => {
				Repaint();
			}).Catch(HandleError);
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
					MichelangeloSession.InstantiateGrammar(item.id).Catch(HandleError);
				}
				if (item.isOwner && GUILayout.Button("X")) {
					MichelangeloSession.DeleteGrammar(item.id).Then(() => {
						Repaint();
					}).Catch(HandleError);
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndScrollView();
		}

		private void HandleError(Exception error) {
			errorMessage = error.Message;
			isLoading = false;
			Repaint();
			Debug.LogError(error);
		}

		[UnityEditor.Callbacks.DidReloadScripts]
		private static void OnScriptsReloaded() {
			if (Michelangelo.Session.WebAPI.IsAuthenticated) {
				var window = Resources.FindObjectsOfTypeAll(typeof(MichelangeloEditorWindow)).FirstOrDefault() as MichelangeloEditorWindow;
				MichelangeloSession.UpdateUserInfo().ThenAll(_ => {
					if (window != null) window.Repaint();
					return new RSG.IPromise<Grammar[]>[] {
						/* fixformat ignore:start */
						MichelangeloSession.UpdateMyGrammarArray(),
						MichelangeloSession.UpdateSharedArray(),
						MichelangeloSession.UpdateTutorialArray()
						/* fixformat ignore:end */
					};
				}).Catch(error => {
					if (window != null) window.HandleError(error);
				});
			}
		}
	}
}