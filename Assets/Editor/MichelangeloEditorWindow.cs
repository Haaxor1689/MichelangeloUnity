using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
	public class MichelangeloEditorWindow : EditorWindow {
		private string loginEmail;
		private string loginPassword;

		private bool isLoading;
		private string errorMessage;
		private bool isUnreachable;

		private Vector2 grammarScrollPos;
		private Vector2 sharedScrollPos;
		private Vector2 tutorialScrollPos;

		[MenuItem("Window/Michelangelo")]
		public static void ShowWindow() {
			GetWindow<MichelangeloEditorWindow>("Michelangelo");
		}

		private void OnGUI() {
			if (isUnreachable) {
				EditorGUILayout.LabelField("Michelangelo web server seems to be unreachable", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Please try again in a few minutes");
				if (GUILayout.Button("Refresh")) {
					isUnreachable = false;
				}
				DrawErrorMessage();
				return;
			}
			if (isLoading) {
				EditorGUILayout.LabelField("Please wait...", EditorStyles.boldLabel);
				return;
			}
			if (Michelangelo.Session.WebAPI.IsAuthenticated) {
				if (!MichelangeloSession.isLoggedIn || MichelangeloSession.user == null) {
					MichelangeloSession.UpdateUserInfo().Then(_ => {
						Repaint();
					}).Catch(HandleException);
					RefreshAllArrays();
					return;
				}

				LoggedIn();
			} else {
				NotLoggedIn();
			}
			DrawErrorMessage();
		}

		private void DrawErrorMessage() {
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
				}).Catch(HandleException);
			}
			if (GUILayout.Button("Create Grammar")) {
				MichelangeloSession.CreateGrammar().Then(_ => {
					Repaint();
				}).Catch(HandleException);
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
				MichelangeloSession.UpdateUserInfo().Then(_ => isLoading = false).Catch(HandleException);
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
				}).Catch(HandleException);
			}
		}

		private void RefreshAllArrays() {
			RSG.Promise<Grammar[]>.All(
				MichelangeloSession.UpdateMyGrammarArray(),
				MichelangeloSession.UpdateSharedArray(),
				MichelangeloSession.UpdateTutorialArray()).Then(_ => {
				Repaint();
			}).Catch(HandleException);
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
					MichelangeloSession.InstantiateGrammar(item.id).Catch(HandleException);
				}
				if (item.isOwner && GUILayout.Button("X")) {
					MichelangeloSession.DeleteGrammar(item.id).Then(() => {
						Repaint();
					}).Catch(HandleException);
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndScrollView();
		}

		private void HandleException(Exception error) {
			errorMessage = error.Message;
			isLoading = false;

			var requestError = error as WebRequestException;
			if (requestError != null && requestError.responseCode == 523) {
				isUnreachable = true;
			}
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
					if (window != null) window.HandleException(error);
				});
			}
		}
	}
}