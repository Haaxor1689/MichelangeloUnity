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

        private static Dictionary<string, Grammar> grammarList;
        public static Dictionary<string, Grammar> GrammarList => grammarList ?? (grammarList = new Dictionary<string, Grammar>());

        public static IPromise<UserInfo> LogIn(string loginEmail, string loginPassword) {
            return WebAPI.Login(loginEmail, loginPassword).Then(() => UpdateUserInfo());
        }

        public static IPromise LogOut() {
            return WebAPI.Logout().Then(() => User = null);
        }

        public static IPromise<Grammar> CreateGrammar() {
            return WebAPI.CreateGrammar().Then(grammar => { GrammarList.Add(grammar.id, grammar); });
        }

        public static IPromise<UserInfo> UpdateUserInfo() {
            return WebAPI.GetUserInfo()
                         .Then(newUser => {
                             IsLoggedIn = true;
                             User = newUser;
                         });
        }

        public static IPromise<IEnumerable<Grammar[]>> RefreshGrammarList() {
            grammarList = new Dictionary<string, Grammar>();

            Action<Grammar[]> appendGrammars = x => {
                foreach (var g in x) {
                    grammarList.Add(g.id, g);
                }
            };
            return Promise<Grammar[]>.All(WebAPI.GetMyGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load own grammars.", x); }),
                                         WebAPI.GetSharedGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load shared grammars.", x); }),
                                         WebAPI.GetTutorialGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load tutorial grammars.", x); }));
        }

        public static IPromise InstantiateGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise.Rejected(new ApplicationException("Requested grammar not found."));
            }
            var grammar = GrammarList[grammarId];

            var newObject = new GameObject(grammar.name);
            var michelangeloObject = newObject.AddComponent<MichelangeloObject>();
            michelangeloObject.Grammar = grammar;
            Selection.objects = new Object[] { newObject };
            return Promise.Resolved();
        }

        public static IPromise<GenerateGrammarResponse> GenerateGrammar(string grammarId) {
            return !GrammarList.ContainsKey(grammarId) 
                ? Promise<GenerateGrammarResponse>.Rejected(new ApplicationException("Requested grammar not found.")) 
                : WebAPI.GenerateGrammar(GrammarList[grammarId]);
        }

        public static Grammar GetGrammar(string grammarId) {
            return GrammarList.ContainsKey(grammarId) ? GrammarList[grammarId] : Grammar.Placeholder;
        }

        public static IPromise DeleteGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise.Rejected(new ApplicationException("Requested grammar not found."));
            }
            return WebAPI.DeleteGrammar(grammarId).Then(() => { GrammarList.Remove(grammarId); });
        }

        public static IPromise<Grammar> UpdateGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise<Grammar>.Rejected(new ApplicationException("Requested grammar not found."));
            }
            if (GrammarList[grammarId].isTutorial) {
                return WebAPI.GetTutorial(grammarId).Then(grammar => { GrammarList[grammarId] = grammar; });
            }
            return WebAPI.GetGrammar(grammarId).Then(grammar => { GrammarList[grammarId] = grammar; });
        }
    }
}
