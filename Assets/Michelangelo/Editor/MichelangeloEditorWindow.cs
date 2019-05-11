using System;
using System.Linq;
using Michelangelo.Models;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Editor {
    internal class MichelangeloEditorWindow : EditorWindow {
        private const string ForgottenPasswordUrl = @"https://michelangelo.graphics/Account/ForgotPassword";
        private const string RegisterUrl = @"https://michelangelo.graphics/Account/Register";

        private string errorMessage;

        private int grammarPage;
        private int grammarsPerPage = 5;
        
        private bool isUnreachable;

        private string loginEmail;
        private string loginPassword;

        private bool localOnly;
        private Vector2 scrollPos;
        private string nameFilter;
        private bool hasChangedNameFilter;
        private GrammarSource sourceFilter;

        [MenuItem("Michelangelo/Michelangelo", false, 0)]
        internal static void ShowWindow() => GetWindow<MichelangeloEditorWindow>("Michelangelo");

        public static void OpenMichelangeloWindowButton() {
            if (GUILayout.Button("Open Michelangelo window", GUILayout.Height(40.0f))) {
                ShowWindow();
            }
        }

        private void OnGUI() {
            if (isUnreachable) {
                EditorGUILayout.HelpBox("Michelangelo web server seems to be unreachable\nPlease try again later...", MessageType.Error);
                if (GUILayout.Button("Refresh")) {
                    isUnreachable = false;
                }
                GUI.enabled = false;
            } else if (MichelangeloSession.IsLoading) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                GUI.enabled = false;
            }

            if (MichelangeloSession.IsAuthenticated) {
                if (MichelangeloSession.User == null) {
                    MichelangeloSession.UpdateUserInfo().Then(_ => { Repaint(); }).Catch(OnRejected);
                    MichelangeloSession.UpdateGrammarList();
                    return;
                }

                LoggedIn();
            } else {
                NotLoggedIn();
            }

            EditorGUILayout.Space();
            if (RequestErrorMessage.IsRequestError(errorMessage)) {
                RequestErrorMessage.Draw(ref errorMessage);
            }
        }

        private void LoggedIn() {
            EditorGUILayout.LabelField("User:", MichelangeloSession.User.username, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Energy:", MichelangeloSession.User.energyAvailable + "/" + MichelangeloSession.User.energyCapacity, EditorStyles.boldLabel);
            if (GUILayout.Button("Log Out")) {
                LogOut();
            }
            GUILayout.Space(20.0f);
            PrintGrammarList();
            GUILayout.Space(20.0f);
            if (GUILayout.Button("Refresh")) {
                Refresh();
            }
        }

        private void NotLoggedIn() {
            EditorGUILayout.LabelField("Log In", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            loginEmail = EditorGUILayout.TextField("User name or Email", loginEmail);
            loginPassword = EditorGUILayout.PasswordField("Password", loginPassword);
            EditorGUILayout.Space();
            if (GUILayout.Button("Log In")) {
                LogIn();
            }
            EditorGUILayout.Space();
            if (LinkLabel.Draw("Register as a new user.")) {
                Application.OpenURL(RegisterUrl);
            }
            if (LinkLabel.Draw("Forgot your password?")) {
                Application.OpenURL(ForgottenPasswordUrl);
            }
        }

        private void LogIn() {
            MichelangeloSession.LogIn(loginEmail, loginPassword)
                               .Then(_ => {
                                   errorMessage = null;
                                   Repaint();
                                   MichelangeloSession.UpdateGrammarList();
                               })
                               .Catch(OnRejected);
        }

        private void LogOut() {
            MichelangeloSession.LogOut()
                               .Then(() => {
                                   errorMessage = null;
                                   Repaint();
                               })
                               .Catch(OnRejected);
        }

        private void Refresh() {
            MichelangeloSession.UpdateUserInfo().Catch(e => OnRejected(e));
            MichelangeloSession.UpdateGrammarList();
        }

        private void PrintGrammarList() {
            EditorGUILayout.LabelField("Grammars", EditorStyles.boldLabel);
            if (MichelangeloSession.GrammarList.Count == 0) {
                EditorGUILayout.HelpBox("Loading, please wait...", MessageType.Info);
                return;
            }
            if (GUILayout.Button("Create Grammar")) {
                MichelangeloSession.CreateGrammar().Then(_ => { Repaint(); }).Catch(OnRejected);
            }
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            var pageCount = 0;
            var filteredGrammarList = MichelangeloSession.GrammarList.Values.OrderByDescending(x => x.LastModifiedDate).Where(FilterGrammar).ToList();
            if (grammarsPerPage <= 0) {
                foreach (var grammar in filteredGrammarList) {
                    grammar.Draw(Repaint, OnRejected, true);
                }
            } else {
                pageCount = (filteredGrammarList.Count() - 1) / grammarsPerPage;
                if (pageCount < grammarPage) {
                    grammarPage = pageCount;
                }

                foreach (var grammar in filteredGrammarList.Skip(grammarPage * grammarsPerPage).Take(grammarsPerPage)) {
                    grammar.Draw(Repaint, OnRejected, true);
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

            DrawNameFilter();
            sourceFilter = (GrammarSource) EditorGUILayout.EnumPopup("Source", sourceFilter);
            grammarsPerPage = EditorGUILayout.IntField(new GUIContent("Items per page", "0 = unlimited"), grammarsPerPage);
            localOnly = EditorGUILayout.Toggle(new GUIContent("Local only", "Filter out not downloaded grammars."), localOnly);
        }

        private bool FilterGrammar(Grammar g) => SourceFilter(g) && NameFilter(g) && LocalOnlyFilter(g);

        private bool NameFilter(Grammar g) => string.IsNullOrEmpty(nameFilter) || g.name.IndexOf(nameFilter, StringComparison.InvariantCultureIgnoreCase) != -1;

        private bool SourceFilter(Grammar g) => sourceFilter == GrammarSource.All ||
                                                sourceFilter == GrammarSource.Mine && g.isOwner ||
                                                sourceFilter == GrammarSource.Shared && g.shared ||
                                                sourceFilter == GrammarSource.Tutorial && g.isTutorial;

        private bool LocalOnlyFilter(Grammar g) => !localOnly || g.SourceCode != null;

        private void DrawNameFilter() {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Width(EditorGUIUtility.labelWidth - GUI.skin.box.padding.left));
            GUI.SetNextControlName("NameFilter");
            var newNameFilter = GUILayout.TextField(nameFilter, GUILayout.ExpandWidth(true));
            if (hasChangedNameFilter) {
                GUI.FocusControl("NameFilter");
                hasChangedNameFilter = false;
            }
            if (newNameFilter != nameFilter) {
                hasChangedNameFilter = true;
                nameFilter = newNameFilter;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnRejected(Exception error) {
            errorMessage = error.Message;

            var requestError = error as WebRequestException;
            if (requestError?.ResponseCode == 523) {
                isUnreachable = true;
            }
            Repaint();
            Debug.LogError(error);
        }
    }
}
