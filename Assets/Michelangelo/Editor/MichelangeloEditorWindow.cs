using System;
using System.Globalization;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Session;
using Michelangelo.Utility;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Michelangelo.Editor {
    public class MichelangeloEditorWindow : EditorWindow {
        private string errorMessage;

        private int grammarPage;
        private int grammarsPerPage;

        private bool isLoading;
        private bool isUnreachable;

        private string loginEmail;
        private string loginPassword;

        private Vector2 scrollPos;
        private GrammarSource selectedSource;
        private GrammarType selectedType;

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
                    MichelangeloSession.RefreshGrammarList();
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
            GUILayout.Space(20.0f);
            PrintGrammarList();
            GUILayout.Space(20.0f);
            if (GUILayout.Button("Refresh")) {
                isLoading = true;
                MichelangeloSession.UpdateUserInfo().Then(_ => isLoading = false).Catch(HandleException);
                MichelangeloSession.RefreshGrammarList();
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
                                       MichelangeloSession.RefreshGrammarList();
                                   })
                                   .Catch(HandleException);
            }
        }

        private void PrintGrammarList() {
            EditorGUILayout.LabelField("Grammars", EditorStyles.boldLabel);
            if (MichelangeloSession.GrammarList.Count == 0) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                return;
            }
            if (GUILayout.Button("Create Grammar")) {
                MichelangeloSession.CreateGrammar().Then(_ => { Repaint(); }).Catch(HandleException);
            }
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            var pageCount = 0;
            var filteredGrammarList = MichelangeloSession.GrammarList.Values.OrderByDescending(x => x.LastModifiedDate).Where(FilterGrammar);
            if (grammarsPerPage <= 0) {
                foreach (var grammar in filteredGrammarList) {
                    DrawGrammar(grammar);
                }
            } else {
                pageCount = (filteredGrammarList.Count() - 1) / grammarsPerPage;
                if (pageCount < grammarPage) {
                    grammarPage = pageCount;
                }

                foreach (var grammar in filteredGrammarList.Skip(grammarPage * grammarsPerPage).Take(grammarsPerPage)) {
                    DrawGrammar(grammar);
                }
            }
            EditorGUILayout.EndScrollView();

            if (pageCount > 0) {
                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
                if (GUILayout.Button("Prev") && grammarPage != 0) {
                    --grammarPage;
                }
                EditorGUILayout.LabelField($"{grammarPage + 1}/{pageCount + 1}", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
                if (GUILayout.Button("Next") && grammarPage < pageCount) {
                    ++grammarPage;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.LabelField("Filters", EditorStyles.boldLabel);
            selectedSource = (GrammarSource) EditorGUILayout.EnumPopup("Source", selectedSource);
            selectedType = (GrammarType) EditorGUILayout.EnumPopup("Type", selectedType);
            grammarsPerPage = EditorGUILayout.IntField(new GUIContent("Items per page", "0 = unlimited"), grammarsPerPage);
        }

        private bool FilterGrammar(Grammar g) => TypeFilter(g) && SourceFilter(g);

        private bool TypeFilter(Grammar g) => selectedType == GrammarType.All ||
                                              selectedType == GrammarType.ACGAX && g.type == "ACGAX" ||
                                              selectedType == GrammarType.DOG && g.type == "DOG";

        private bool SourceFilter(Grammar g) => selectedSource == GrammarSource.All ||
                                                selectedSource == GrammarSource.Mine && g.isOwner ||
                                                selectedSource == GrammarSource.Shared && g.shared ||
                                                selectedSource == GrammarSource.Tutorial && g.isTutorial;

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
            MichelangeloSession.UpdateUserInfo()
                               .Then(_ => {
                                   if (window != null) {
                                       window.Repaint();
                                   }
                               })
                               .Catch(error => {
                                   if (window != null) {
                                       window.HandleException(error);
                                   }
                               });

            MichelangeloSession.RefreshGrammarList()
                               .Then(_ => {
                                   if (window != null) {
                                       window.Repaint();
                                   }
                               })
                               .Catch(error => {
                                   if (window != null) {
                                       window.HandleException(error);
                                   }
                               });
        }
    }
}
