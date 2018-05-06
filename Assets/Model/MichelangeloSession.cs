using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public static class MichelangeloSession {
        public static bool isLoading { get; private set; }
        public static bool isLoggedIn { get; private set; }

        public static UserInfo user { get; private set; }

        public static List<Grammar> grammar { get; private set; }
        public static List<Grammar> shared { get; private set; }
        public static List<Grammar> tutorial { get; private set; }

        public static event Action<string> taskDone;

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
                if (grammar != null) {
                    grammar.Add(response);
                } else {
                    grammar = new List<Grammar> { response };
                }
                TaskDone(null);
            });
        }

        public static void UpdateUserInfo() {
            Michelangelo.Session.WebAPI.GetUserInfo((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }

                user = response;
                isLoggedIn = true;
                TaskDone(null);
                UpdateGrammarArray();
                UpdateSharedArray();
                UpdateTutorialArray();
            });
        }

        public static void UpdateGrammarArray() {
            Michelangelo.Session.WebAPI.GetGrammar((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                grammar = response.ToList();
                TaskDone(null);
            });
        }

        public static void UpdateSharedArray() {
            Michelangelo.Session.WebAPI.GetShared((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                shared = response.ToList();
                TaskDone(null);
            });
        }

        public static void UpdateTutorialArray() {
            Michelangelo.Session.WebAPI.GetTutorials((response, error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                tutorial = response.ToList();
                TaskDone(null);
            });
        }

        public static void DeleteGrammar(string id) {
            Michelangelo.Session.WebAPI.DeleteGrammar(id, (error) => {
                if (error != null) {
                    TaskDone(error);
                    return;
                }
                grammar.Remove(grammar.First(x => x.id == id));
                TaskDone(null);
            });
        }

        private static void TaskDone(string error) {
            isLoading = false;
            taskDone(error);
        }
    }
}