using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Scripts;
using Michelangelo.Session;
using Michelangelo.Utility;
using RSG;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Model {
    public static class MichelangeloSession {
        private const string UserInfoPrefsKey = Constants.EditorPrefsPrefix + "UserInfo";
        private const string GrammarListPrefsKey = Constants.EditorPrefsPrefix + "GrammarList";

        public static UserInfo User => user ?? (user = UserInfoFromPrefs);

        private static UserInfo UserInfoFromPrefs => UserInfo.FromJson(EditorPrefs.GetString(UserInfoPrefsKey));
        private static UserInfo user;
        
        public static IReadOnlyDictionary<string, Grammar> GrammarList => grammarList ?? (grammarList = GrammarListFromPrefs);

        private static Dictionary<string, Grammar> GrammarListFromPrefs => JsonArray.FromJsonArray<Grammar>(EditorPrefs.GetString(GrammarListPrefsKey)).ToDictionary(x => x.id);
        private static Dictionary<string, Grammar> grammarList;

        private static void SaveGrammarList() => EditorPrefs.SetString(GrammarListPrefsKey, JsonArray.ToJsonArray(grammarList.Values.ToArray()));

        public static IPromise<UserInfo> LogIn(string loginEmail, string loginPassword) {
            return WebAPI.Login(loginEmail, loginPassword).Then(() => UpdateUserInfo());
        }

        public static IPromise LogOut() {
            return WebAPI.Logout().Then(() => {
                EditorPrefs.SetString(UserInfoPrefsKey, "");
                EditorPrefs.SetString(GrammarListPrefsKey, "");
                user = null;
                grammarList = null;
            });
        }

        public static IPromise<Grammar> CreateGrammar() {
            return WebAPI.CreateGrammar()
                         .Then(grammar => {
                             grammarList.Add(grammar.id, grammar);
                             SaveGrammarList();
                         });
        }

        public static IPromise<UserInfo> UpdateUserInfo() {
            return WebAPI.GetUserInfo()
                         .Then(newUser => {
                             user = newUser;
                             EditorPrefs.SetString(UserInfoPrefsKey, JsonUtility.ToJson(user));
                         });
        }

        public static IPromise<IEnumerable<Grammar[]>> RefreshGrammarList() {
            grammarList = new Dictionary<string, Grammar>();

            Action<Grammar[]> appendGrammars = retrievedGrammars => {
                foreach (var grammar in retrievedGrammars) {
                    if (grammar.SourceCode != null) {
                        grammar.code = grammar.SourceCode.text;
                    }
                    grammarList.Add(grammar.id, grammar);
                }
            };
            return Promise<Grammar[]>.All(WebAPI.GetMyGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load own grammars.", x); }),
                                         WebAPI.GetSharedGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load shared grammars.", x); }),
                                         WebAPI.GetTutorialGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load tutorial grammars.", x); }))
                                     .Then(_ => SaveGrammarList());
        }

        public static IPromise InstantiateGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise.Rejected(new ApplicationException("Requested grammar not found."));
            }
            var grammar = GrammarList[grammarId];

            var newObject = GrammarObject.Construct(grammar);
            Selection.objects = new UnityEngine.Object[] { newObject };
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
            return WebAPI.DeleteGrammar(grammarId).Then(() => {
                grammarList.Remove(grammarId);
                SaveGrammarList();
            });
        }

        public static IPromise<Grammar> UpdateGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise<Grammar>.Rejected(new ApplicationException("Requested grammar not found."));
            }
            if (GrammarList[grammarId].isTutorial) {
                return WebAPI.GetTutorial(grammarId).Then(grammar => {
                    grammarList[grammarId] = grammar;
                    SaveGrammarList();
                });
            }
            return WebAPI.GetGrammar(grammarId).Then(grammar => {
                grammarList[grammarId] = grammar;
                SaveGrammarList();
            });
        }

        public static IPromise<GenerateGrammarResponse> GenerateScene(string code) => WebAPI.GenerateScene(code);
    }
}
