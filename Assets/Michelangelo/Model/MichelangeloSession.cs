using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model.Render;
using Michelangelo.Session;
using RSG;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Michelangelo.Model {
    public static class MichelangeloSession {
        public static bool IsLoggedIn { get; private set; }

        public static UserInfo User { get; private set; }

        public static List<Grammar> MyGrammar { get; private set; }
        public static List<Grammar> SharedGrammar { get; private set; }
        public static List<Grammar> TutorialGrammar { get; private set; }

        public static IEnumerable<Grammar> AllGrammars {
            get {
                IEnumerable<Grammar> union = null;
                if (MyGrammar != null) {
                    union = (MyGrammar).Union(MyGrammar);
                }
                if (SharedGrammar != null) {
                    union = (union ?? SharedGrammar).Union(SharedGrammar);
                }
                if (TutorialGrammar != null) {
                    union = (union ?? TutorialGrammar).Union(TutorialGrammar);
                }
                return union;
            }
        }

        public static IPromise<UserInfo> LogIn(string loginEmail, string loginPassword) {
            return WebAPI.Login(loginEmail, loginPassword).Then(() => UpdateUserInfo());
        }

        public static IPromise LogOut() {
            return WebAPI.Logout().Then(() => User = null);
        }

        public static IPromise<Grammar> CreateGrammar() {
            return WebAPI.CreateGrammar()
                         .Then(grammar => {
                             if (MyGrammar == null) {
                                 MyGrammar = new List<Grammar>();
                             }
                             MyGrammar.Add(grammar);
                         });
        }

        public static IPromise<UserInfo> UpdateUserInfo() {
            return WebAPI.GetUserInfo()
                         .Then(newUser => {
                             IsLoggedIn = true;
                             User = newUser;
                         });
        }

        public static IPromise<Grammar[]> UpdateMyGrammarArray() {
            return WebAPI.GetMyGrammarArray().Then(response => { MyGrammar = response.ToList(); });
        }

        public static IPromise<Grammar[]> UpdateSharedArray() {
            return WebAPI.GetSharedGrammarArray().Then(response => { SharedGrammar = response.ToList(); });
        }

        public static IPromise<Grammar[]> UpdateTutorialArray() {
            return WebAPI.GetTutorialGrammarArray().Then(response => { TutorialGrammar = response.ToList(); });
        }

        public static IPromise InstantiateGrammar(string grammarId) {
            var g = AllGrammars.First(x => x.id == grammarId);
            if (g == null) {
                return Promise.Rejected(new ApplicationException("Requested grammar not found."));
            }

            var newObject = new GameObject(g.name);
            var michelangeloObject = newObject.AddComponent<MichelangeloObject>();
            michelangeloObject.Grammar = g;
            Selection.objects = new Object[] { newObject };
            return Promise.Resolved();
        }

        public static IPromise<ModelMesh> GenerateGrammar(string grammarId) {
            var g = AllGrammars.FirstOrDefault(x => x.id == grammarId);
            if (g == null) {
                return Promise<ModelMesh>.Rejected(new ApplicationException("Requested grammar not found."));
            }

            return WebAPI.GenerateGrammar(g);
        }

        public static Grammar GetGrammar(string grammarId) {
            Grammar ret;
            if (MyGrammar != null && (ret = MyGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return ret;
            }
            if (SharedGrammar != null && (ret = SharedGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return ret;
            }
            if (TutorialGrammar != null && (ret = TutorialGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return ret;
            }

            return Grammar.Placeholder;
        }

        public static IPromise DeleteGrammar(string grammarId) {
            return WebAPI.DeleteGrammar(grammarId).Then(() => { MyGrammar.Remove(MyGrammar.First(x => x.id == grammarId)); });
        }

        public static IPromise<Grammar> UpdateGrammar(string grammarId) {
            Grammar grammar;
            if (MyGrammar != null && (grammar = MyGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return UpdateMyGrammar(grammarId, MyGrammar.IndexOf(grammar));
            }
            if (SharedGrammar != null && (grammar = SharedGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return UpdateSharedGrammar(grammarId, SharedGrammar.IndexOf(grammar));
            }
            if (TutorialGrammar != null && (grammar = TutorialGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return UpdateTutorialGrammar(grammarId, TutorialGrammar.IndexOf(grammar));
            }

            if (MyGrammar != null && SharedGrammar != null && TutorialGrammar != null) {
                return Promise<Grammar>.Rejected(new ApplicationException("Grammar not found."));
            }
            return Promise<Grammar>.Resolved(Grammar.Placeholder);
        }

        #region UpdateGrammarHelpers
        private static IPromise<Grammar> UpdateMyGrammar(string grammarId, int index) {
            return WebAPI.GetGrammar(grammarId).Then(grammar => { MyGrammar[index] = grammar; });
        }

        private static IPromise<Grammar> UpdateSharedGrammar(string grammarId, int index) {
            return WebAPI.GetGrammar(grammarId).Then(grammar => { SharedGrammar[index] = grammar; });
        }

        private static IPromise<Grammar> UpdateTutorialGrammar(string grammarId, int index) {
            return WebAPI.GetTutorial(grammarId).Then(grammar => { TutorialGrammar[index] = grammar; });
        }
        #endregion
    }
}
