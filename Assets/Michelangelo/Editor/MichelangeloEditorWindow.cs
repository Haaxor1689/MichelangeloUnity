using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Session;
using Michelangelo.Utility;
using RSG;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Michelangelo.Editor {
    public class MichelangeloEditorWindow : EditorWindow {
        private string errorMessage;
        
        private bool isLoading;
        private bool isUnreachable;
        private string loginEmail;
        private string loginPassword;

        private int grammarPage;
        private int sharedPage;
        private int tutorialPage;

        private Vector2 scrollPos;

        private int grammarsPerPage;

        [MenuItem("Window/Michelangelo")]
        public static void ShowWindow() => GetWindow<MichelangeloEditorWindow>("Michelangelo");

        private void OnGUI() {
            if (isUnreachable) {
                EditorGUILayout.HelpBox("Michelangelo web server seems to be unreachable\nPlease try again later...", MessageType.Error);
                if (GUILayout.Button("Refresh")) {
                    isUnreachable = false;
                }
                GUI.enabled = false;
            } else if (isLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                GUI.enabled = false;
            }

            if (WebAPI.IsAuthenticated) {
                if (!MichelangeloSession.IsLoggedIn || MichelangeloSession.User == null) {
                    MichelangeloSession.UpdateUserInfo().Then(_ => { Repaint(); }).Catch(HandleException);
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
            if (string.IsNullOrEmpty(errorMessage)) {
                return;
            }
            GUILayout.Space(20.0f);
            EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
            if (GUILayout.Button("Clear error message")) {
                errorMessage = null;
            }
        }

        private void LoggedIn() {
            EditorGUILayout.LabelField("User:", MichelangeloSession.User.username, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Energy:", MichelangeloSession.User.energyAvailable + "/" + MichelangeloSession.User.energyCapacity, EditorStyles.boldLabel);
            if (GUILayout.Button("Log Out")) {
                isLoading = true;
                MichelangeloSession.LogOut()
                                   .Then(() => {
                                       isLoading = false;
                                       errorMessage = null;
                                       Repaint();
                                   })
                                   .Catch(HandleException);
            }
            if (GUILayout.Button("Create Grammar")) {
                MichelangeloSession.CreateGrammar().Then(_ => { Repaint(); }).Catch(HandleException);
            }
            GUILayout.Space(20.0f);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            PrintGrammarArray(MichelangeloSession.MyGrammar, "Your designs", ref grammarPage);
            GUILayout.Space(20.0f);
            PrintGrammarArray(MichelangeloSession.SharedGrammar, "Shared by others", ref sharedPage);
            GUILayout.Space(20.0f);
            PrintGrammarArray(MichelangeloSession.TutorialGrammar, "Tutorials and Reference", ref tutorialPage);
            EditorGUILayout.EndScrollView();
            GUILayout.Space(20.0f);
            grammarsPerPage = EditorGUILayout.IntField(new GUIContent("Items per page", "0 = unlimited"), grammarsPerPage);
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
                MichelangeloSession.LogIn(loginEmail, loginPassword)
                                   .Then(_ => {
                                       isLoading = false;
                                       errorMessage = null;
                                       Repaint();
                                       RefreshAllArrays();
                                   })
                                   .Catch(HandleException);
            }
        }

        private void RefreshAllArrays() {
            Promise<Grammar[]>.All(MichelangeloSession.UpdateMyGrammarArray(),
                                  MichelangeloSession.UpdateSharedArray(),
                                  MichelangeloSession.UpdateTutorialArray())
                              .Then(_ => { Repaint(); })
                              .Catch(HandleException);
        }

        private void PrintGrammarArray(IReadOnlyList<Grammar> grammarArray, string sectionTitle, ref int pageNumber) {
            EditorGUILayout.LabelField(sectionTitle, EditorStyles.boldLabel);
            if (grammarArray == null || grammarArray.Count == 0) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                return;
            }

            if (grammarsPerPage <= 0) {
                foreach (var grammar in grammarArray) {
                    DrawGrammar(grammar);
                }
            } else {
                for (var i = pageNumber * grammarsPerPage; i < (pageNumber + 1) * grammarsPerPage && i < grammarArray.Count; ++i) {
                    DrawGrammar(grammarArray[i]);
                }

                var pageCount = (grammarArray.Count - 1) / grammarsPerPage;
                if (pageCount == 0) {
                    return;
                }
                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
                if (GUILayout.Button("Prev") && pageNumber != 0) {
                    --pageNumber;
                }
                EditorGUILayout.LabelField($"{pageNumber + 1}/{pageCount + 1}", new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter});
                if (GUILayout.Button("Next") && pageNumber < pageCount) {
                    ++pageNumber;
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawGrammar(Grammar grammar) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(grammar.name, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Type", grammar.type);
            EditorGUILayout.LabelField("Last Modified", grammar.LastModifiedDate.ToString(CultureInfo.CurrentCulture));
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            if (GUILayout.Button("Instantiate")) {
                MichelangeloSession.InstantiateGrammar(grammar.id).Catch(HandleException);
            }
            if (grammar.isOwner && GUILayout.Button("Delete")) {
                MichelangeloSession.DeleteGrammar(grammar.id).Then(() => { Repaint(); }).Catch(HandleException);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private void HandleException(Exception error) {
            errorMessage = error.Message;
            isLoading = false;

            var requestError = error as WebRequestException;
            if (requestError?.ResponseCode == 523) {
                isUnreachable = true;
            }
            Repaint();
            Debug.LogError(error);
        }

        [DidReloadScripts]
        private static void OnScriptsReloaded() {
            var window = Resources.FindObjectsOfTypeAll(typeof(MichelangeloEditorWindow)).FirstOrDefault() as MichelangeloEditorWindow;
            if (window == null || !WebAPI.IsAuthenticated) {
                return;
            }

            window.grammarPage = 0;
            window.sharedPage = 0;
            window.tutorialPage = 0;
            MichelangeloSession.UpdateUserInfo()
                               .ThenAll(_ => {
                                   if (window != null) {
                                       window.Repaint();
                                   }
                                   return new[] {
                                       MichelangeloSession.UpdateMyGrammarArray(),
                                       MichelangeloSession.UpdateSharedArray(),
                                       MichelangeloSession.UpdateTutorialArray()
                                   };
                               })
                               .Catch(error => {
                                   if (window != null) {
                                       window.HandleException(error);
                                   }
                               });
        }
    }
}
