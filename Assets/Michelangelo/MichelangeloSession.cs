using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Models;
using Michelangelo.Scripts;
using Michelangelo.Session;
using Michelangelo.Utility;
using RSG;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Michelangelo {
    public static class MichelangeloSession {
        private const string UserInfoPrefsKey = Constants.EditorPrefsPrefix + "UserInfo";
        private const string GrammarListPrefsKey = Constants.EditorPrefsPrefix + "GrammarList";

        private static UserInfo user;
        private static Dictionary<string, Grammar> grammarList;

        private static UserInfo UserInfoFromPrefs => UserInfo.FromJson(EditorPrefs.GetString(UserInfoPrefsKey));
        private static Dictionary<string, Grammar> GrammarListFromPrefs => JsonArray.FromJsonArray<Grammar>(EditorPrefs.GetString(GrammarListPrefsKey)).ToDictionary(x => x.id);

        /// <summary>
        ///   Contains info about currently logged in user. Can be updated by calling <see cref="UpdateUserInfo" />.
        /// </summary>
        public static UserInfo User => user ?? (user = UserInfoFromPrefs);

        /// <summary>
        ///   Dictionary of currently downloaded grammars. Can be updated by calling <see cref="UpdateGrammarList" />.
        /// </summary>
        public static IReadOnlyDictionary<string, Grammar> GrammarList => grammarList ?? (grammarList = GrammarListFromPrefs);

        /// <summary>
        ///   True if session is currently authenticated with Michelangelo service.
        /// </summary>
        public static bool IsAuthenticated => WebAPI.IsAuthenticated;

        /// <summary>
        ///   True if there is a request currently being sent to Michelangelo service.
        /// </summary>
        public static bool IsLoading => WebAPI.IsLoading;

        private static void SaveGrammarList() => EditorPrefs.SetString(GrammarListPrefsKey, JsonArray.ToJsonArray(grammarList.Values.ToArray()));

        /// <summary>
        ///   Cancels any running Michelangelo service requests.
        /// </summary>
        public static void CancelGeneration() => WebAPI.CancelGeneration = true;

        /// <summary>
        ///   Logs in user to Michelangelo service. This is required to do before any other requests to Michelangelo service are
        ///   done.
        /// </summary>
        /// <param name="loginUsername">Username of Michelangelo account.</param>
        /// <param name="loginPassword">Password for Michelangelo account.</param>
        /// <returns>
        ///   When resolved, a <see cref="UserInfo" /> about logged in user. When rejected, an
        ///   <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise<UserInfo> LogIn(string loginUsername, string loginPassword) {
            return WebAPI.Login(loginUsername, loginPassword).Then(() => UpdateUserInfo());
        }

        /// <summary>
        ///   Logs out current user from Michelangelo service and removes saved user info and grammar data.
        /// </summary>
        /// <returns>
        ///   When rejected, an <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise LogOut() {
            return WebAPI.Logout()
                         .Then(() => {
                             EditorPrefs.SetString(UserInfoPrefsKey, "");
                             EditorPrefs.SetString(GrammarListPrefsKey, "");
                             user = null;
                             grammarList = null;
                         });
        }

        /// <summary>
        ///   Creates new grammar.
        /// </summary>
        /// <returns>
        ///   When resolved, a newly created <see cref="Grammar" /> object. When rejected, an
        ///   <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise<Grammar> CreateGrammar() {
            return WebAPI.CreateGrammar()
                         .Then(grammar => {
                             grammarList.Add(grammar.id, grammar);
                             SaveGrammarList();
                             return grammar;
                         });
        }

        /// <summary>
        ///   Updates <see cref="User" />.
        /// </summary>
        /// <returns>
        ///   When resolved, an updated <see cref="UserInfo" /> object. When rejected, an
        ///   <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise<UserInfo> UpdateUserInfo() {
            return WebAPI.GetUserInfo()
                         .Then(newUser => {
                             user = newUser;
                             EditorPrefs.SetString(UserInfoPrefsKey, JsonUtility.ToJson(user));
                             return newUser;
                         });
        }

        /// <summary>
        ///   Updates <see cref="GrammarList" />.
        /// </summary>
        /// <returns>
        ///   When resolved, an updated grammar dictionary object. When rejected, an
        ///   <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise<IReadOnlyDictionary<string, Grammar>> UpdateGrammarList() {
            grammarList = new Dictionary<string, Grammar>();

            Func<Grammar[], Grammar[]> appendGrammars = retrievedGrammars => {
                foreach (var grammar in retrievedGrammars) {
                    if (grammar.SourceCode != null) {
                        grammar.code = grammar.SourceCode.text;
                    }
                    grammarList.Add(grammar.id, grammar);
                }
                return retrievedGrammars;
            };
            return Promise<Grammar[]>.All(WebAPI.GetMyGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load own grammars.", x); }),
                                         WebAPI.GetSharedGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load shared grammars.", x); }),
                                         WebAPI.GetTutorialGrammarArray().Then(appendGrammars).Catch(x => { throw new Exception("Could not load tutorial grammars.", x); }))
                                     .Then<IReadOnlyDictionary<string, Grammar>>(_ => {
                                         SaveGrammarList();
                                         return grammarList;
                                     });
        }

        /// <summary>
        ///   Instantiates new <see cref="GrammarObject" /> linked to grammar with <see cref="grammarId" /> id.
        /// </summary>
        /// <param name="grammarId">Id of grammar that should be linked to newly instantiated <see cref="GrammarObject" />.</param>
        /// <returns>
        ///   When resolved, a reference to newly created <see cref="GameObject" />. When rejected, an
        ///   <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise<GameObject> InstantiateGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise<GameObject>.Rejected(new ApplicationException("Instantiate grammar request error:\nRequested grammar not found."));
            }
            var grammar = GrammarList[grammarId];

            var newObject = GrammarObject.Construct(grammar);
            Selection.objects = new Object[] { newObject };
            return Promise<GameObject>.Resolved(newObject);
        }

        internal static IPromise<GenerateGrammarResponse> GenerateGrammar(string grammarId) {
            return !GrammarList.ContainsKey(grammarId)
                ? Promise<GenerateGrammarResponse>.Rejected(new ApplicationException("Generate grammar request error:\nRequested grammar not found."))
                : WebAPI.GenerateGrammar(GrammarList[grammarId])
                        .Then(_ => { 
                            UpdateUserInfo();
                            return _;
                        });
        }

        internal static IPromise<GenerateGrammarResponse> GenerateScene(string code) {
            return WebAPI.GenerateScene(code)
                         .Then(_ => { 
                             UpdateUserInfo();
                             return _; });
        }

        /// <summary>
        ///   Deletes grammar with <see cref="grammarId" /> id.
        /// </summary>
        /// <param name="grammarId">Id of grammar that should be deleted.</param>
        /// <returns>
        ///   When rejected, an <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise DeleteGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise.Rejected(new ApplicationException("Delete grammar request error:\nRequested grammar not found."));
            }
            return WebAPI.DeleteGrammar(grammarId)
                         .Then(() => {
                             grammarList.Remove(grammarId);
                             SaveGrammarList();
                         });
        }

        /// <summary>
        ///   Updates local data about grammar with <see cref="grammarId" /> id.
        /// </summary>
        /// <param name="grammarId">Id of grammar that should updated.</param>
        /// <returns>
        ///   When resolved, an updated <see cref="Grammar" /> object. When rejected, an
        ///   <see cref="Exception" /> with info about error that occured.
        /// </returns>
        public static IPromise<Grammar> GetGrammar(string grammarId) {
            if (!GrammarList.ContainsKey(grammarId)) {
                return Promise<Grammar>.Rejected(new ApplicationException("Update grammar request error:\nRequested grammar not found."));
            }
            if (GrammarList[grammarId].isTutorial) {
                return WebAPI.GetTutorial(grammarId)
                             .Then(grammar => {
                                 grammarList[grammarId] = grammar;
                                 SaveGrammarList();
                                 return grammar;
                             });
            }
            return WebAPI.GetGrammar(grammarId)
                         .Then(grammar => {
                             grammarList[grammarId] = grammar;
                             SaveGrammarList();
                             return grammar;
                         });
        }
    }
}
