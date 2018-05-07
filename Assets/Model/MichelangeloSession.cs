using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model.Render;
using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model {
    public static class MichelangeloSession {
        public static bool isLoading { get; private set; }
        public static bool isLoggedIn { get; private set; }

        public static UserInfo user { get; private set; }

        public static List<Grammar> myGrammar { get; private set; }
        public static List<Grammar> sharedGrammar { get; private set; }
        public static List<Grammar> tutorialGrammar { get; private set; }

        public static IEnumerable<Grammar> allGrammars {
            get {
                IEnumerable<Grammar> union = null;
                if (myGrammar != null) {
                    union = (union ?? myGrammar).Union(myGrammar);
                }
                if (sharedGrammar != null) {
                    union = (union ?? sharedGrammar).Union(sharedGrammar);
                }
                if (tutorialGrammar != null) {
                    union = (union ?? tutorialGrammar).Union(tutorialGrammar);
                }
                return union;
            }
        }

        public static event Action<string> taskDone;
        public static event Action<ModelMesh> modelGenerated;

        public static void LogIn(string loginEmail, string loginPassword) {
            isLoading = true;
            Michelangelo.Session.WebAPI.Login(loginEmail, loginPassword, error => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                UpdateUserInfo();
            });
        }

        public static void LogOut() {
            isLoading = true;
            Michelangelo.Session.WebAPI.Logout(error => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                user = null;
                TaskDone(null);
            });

        }

        public static void CreateGrammar() {
            Michelangelo.Session.WebAPI.CreateGrammar((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                if (myGrammar != null) {
                    myGrammar.Add(response);
                } else {
                    myGrammar = new List<Grammar> { response };
                }
                TaskDone(null);
            });
        }

        public static void UpdateUserInfo() {
            isLoading = true;
            Michelangelo.Session.WebAPI.GetUserInfo((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }

                user = response;
                isLoggedIn = true;
                UpdateMyGrammarArray();
                UpdateSharedArray();
                UpdateTutorialArray();
                TaskDone(null);
            });
        }

        public static void UpdateMyGrammarArray() {
            Michelangelo.Session.WebAPI.GetMyGrammar((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                myGrammar = response.ToList();
                TaskDone(null);
            });
        }

        public static void UpdateSharedArray() {
            Michelangelo.Session.WebAPI.GetShared((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                sharedGrammar = response.ToList();
                TaskDone(null);
            });
        }

        public static void UpdateTutorialArray() {
            Michelangelo.Session.WebAPI.GetTutorials((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                tutorialGrammar = response.ToList();
                TaskDone(null);
            });
        }

        public static void InstantiateGrammar(string grammarId) {
            var g = allGrammars.First(x => x.id == grammarId);
            if (g == null) {
                TaskDone("Requested grammar not found.");
                return;
            }

            var newObject = new GameObject(g.name);
            var michelangeloObject = newObject.AddComponent<MichelangeloObject>();
            michelangeloObject.SetGrammar(g);
            Selection.objects = new UnityEngine.Object[] { newObject };

        }

        public static void GenerateGrammar(string grammarId) {
            var g = allGrammars.First(x => x.id == grammarId);
            if (g == null) {
                TaskDone("Requested grammar not found.");
                return;
            }

            isLoading = true;
            Michelangelo.Session.WebAPI.GenerateGrammar(g, (model, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                modelGenerated(model);
                TaskDone(null);
            });
        }

        public static Grammar GetGrammar(string grammarId) {
            Grammar ret = null;
            if (myGrammar != null && (ret = myGrammar.FirstOrDefault(x => x.id == grammarId)) != null)
                return ret;
            if (sharedGrammar != null && (ret = sharedGrammar.FirstOrDefault(x => x.id == grammarId)) != null)
                return ret;
            if (tutorialGrammar != null && (ret = tutorialGrammar.FirstOrDefault(x => x.id == grammarId)) != null)
                return ret;

            return Grammar.Placeholder;
        }

        public static void DeleteGrammar(string grammarId) {
            isLoading = true;
            Michelangelo.Session.WebAPI.DeleteGrammar(grammarId, (error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                myGrammar.Remove(myGrammar.First(x => x.id == grammarId));
                TaskDone(null);
            });
        }

        public static void UpdateGrammar(string grammarId) {
            Grammar grammar;
            if (myGrammar != null && (grammar = myGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                updateMyGrammar(grammarId, myGrammar.IndexOf(grammar));
            } else if (sharedGrammar != null && (grammar = sharedGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                updateSharedGrammar(grammarId, sharedGrammar.IndexOf(grammar));
            } else if (tutorialGrammar != null && (grammar = tutorialGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                updateTutorialGrammar(grammarId, tutorialGrammar.IndexOf(grammar));
            }
        }

        #region UpdateGrammarHelpers
        private static void updateMyGrammar(string grammarId, int index) {
            isLoading = true;
            Michelangelo.Session.WebAPI.GetGrammar(grammarId, (grammar, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                myGrammar[index] = grammar;
                TaskDone(null);
            });
        }

        private static void updateSharedGrammar(string grammarId, int index) {
            isLoading = true;
            Michelangelo.Session.WebAPI.GetGrammar(grammarId, (grammar, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                sharedGrammar[index] = grammar;
                TaskDone(null);
            });
        }

        private static void updateTutorialGrammar(string grammarId, int index) {
            isLoading = true;
            Michelangelo.Session.WebAPI.GetTutorial(grammarId, (grammar, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                tutorialGrammar[index] = grammar;
                TaskDone(null);
            });
        }
        #endregion

        private static void TaskDone(string error) {
            isLoading = false;
            taskDone(error);
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() {
            if (Michelangelo.Session.WebAPI.IsAuthenticated) {
                UpdateUserInfo();
            }
        }
    }
}