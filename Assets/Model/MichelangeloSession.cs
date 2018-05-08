using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model.Render;
using Michelangelo.Scripts;
using RSG;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model {
    public static class MichelangeloSession {
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

        public static IPromise<UserInfo> LogIn(string loginEmail, string loginPassword) {
            return Michelangelo.Session.WebAPI.Login(loginEmail, loginPassword).Then(() => UpdateUserInfo());
        }

        public static IPromise LogOut() {
            return Michelangelo.Session.WebAPI.Logout().Then(() => user = null);
        }

        public static IPromise<Grammar> CreateGrammar() {
            return Michelangelo.Session.WebAPI.CreateGrammar().Then(grammar => {
                if (myGrammar == null) {
                    myGrammar = new List<Grammar>();
                }
                myGrammar.Add(grammar);
            });
        }

        public static IPromise<UserInfo> UpdateUserInfo() {
            return Michelangelo.Session.WebAPI.GetUserInfo().Then(newUser => {
                isLoggedIn = true;
                user = newUser;
            });
        }

        public static IPromise<Grammar[]> UpdateMyGrammarArray() {
            return Michelangelo.Session.WebAPI.GetMyGrammarArray().Then((response) => {
                myGrammar = response.ToList();
            });
        }

        public static IPromise<Grammar[]> UpdateSharedArray() {
            return Michelangelo.Session.WebAPI.GetSharedGrammarArray().Then((response) => {
                sharedGrammar = response.ToList();
            });
        }

        public static IPromise<Grammar[]> UpdateTutorialArray() {
            return Michelangelo.Session.WebAPI.GetTutorialGrammarArray().Then((response) => {
                tutorialGrammar = response.ToList();
            });
        }

        public static IPromise InstantiateGrammar(string grammarId) {
            var g = allGrammars.First(x => x.id == grammarId);
            if (g == null) return Promise.Rejected(new ApplicationException("Requested grammar not found."));

            var newObject = new GameObject(g.name);
            var michelangeloObject = newObject.AddComponent<MichelangeloObject>();
            michelangeloObject.SetGrammar(g);
            Selection.objects = new UnityEngine.Object[] { newObject };
            return Promise.Resolved();
        }

        public static IPromise<ModelMesh> GenerateGrammar(string grammarId) {
            var g = allGrammars.First(x => x.id == grammarId);
            if (g == null) return Promise<ModelMesh>.Rejected(new ApplicationException("Requested grammar not found."));

            return Michelangelo.Session.WebAPI.GenerateGrammar(g);
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

        public static IPromise DeleteGrammar(string grammarId) {
            return Michelangelo.Session.WebAPI.DeleteGrammar(grammarId).Then(() => {
                myGrammar.Remove(myGrammar.First(x => x.id == grammarId));
            });
        }

        public static IPromise<Grammar> UpdateGrammar(string grammarId) {
            Grammar grammar;
            if (myGrammar != null && (grammar = myGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return updateMyGrammar(grammarId, myGrammar.IndexOf(grammar));
            }
            if (sharedGrammar != null && (grammar = sharedGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return updateSharedGrammar(grammarId, sharedGrammar.IndexOf(grammar));
            }
            if (tutorialGrammar != null && (grammar = tutorialGrammar.FirstOrDefault(x => x.id == grammarId)) != null) {
                return updateTutorialGrammar(grammarId, tutorialGrammar.IndexOf(grammar));
            }
            return Promise<Grammar>.Rejected(new ApplicationException("Grammar not found."));
        }

        #region UpdateGrammarHelpers
        private static IPromise<Grammar> updateMyGrammar(string grammarId, int index) {
            return Michelangelo.Session.WebAPI.GetGrammar(grammarId).Then(grammar => {
                myGrammar[index] = grammar;
            });
        }

        private static IPromise<Grammar> updateSharedGrammar(string grammarId, int index) {
            return Michelangelo.Session.WebAPI.GetGrammar(grammarId).Then(grammar => {
                sharedGrammar[index] = grammar;
            });
        }

        private static IPromise<Grammar> updateTutorialGrammar(string grammarId, int index) {
            return Michelangelo.Session.WebAPI.GetTutorial(grammarId).Then(grammar => {
                tutorialGrammar[index] = grammar;
            });
        }
        #endregion
    }
}