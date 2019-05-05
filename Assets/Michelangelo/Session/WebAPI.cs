using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Michelangelo.Model;
using Michelangelo.Scripts;
using Michelangelo.Utility;
using RSG;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using MessagePack;
using Michelangelo.Model.MichelangeloApi;

namespace Michelangelo.Session {
    public static class WebAPI {
        private static readonly Regex RequestTokenRegex = new Regex("<form action=\"\\/Account\\/Log(in|Off)\" .*><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"[^\"]*");

        private const string SetCookieName = "Set-Cookie";
        private const string RequestTokenName = "__RequestVerificationToken";
        private const string VerificationTokenName = ".AspNet.ApplicationCookie";
        private const string VerificationTokenPrefsKey = Constants.EditorPrefsPrefix + VerificationTokenName;

        private static string VerificationTokenCookie {
            get { return EditorPrefs.GetString(VerificationTokenPrefsKey); }
            set { EditorPrefs.SetString(VerificationTokenPrefsKey, value); }
        }
        
        private static string CookiesString => $"{VerificationTokenName}={VerificationTokenCookie};";
        public static bool IsAuthenticated => !string.IsNullOrEmpty(VerificationTokenCookie);

        public static bool IsLoading;
        public static bool CancelGeneration;
        
        #region Login
        public static IPromise Login(string email, string password) => new Promise((resolve, reject) => MichelangeloSingleton.Coroutine(LoginCoroutine(email, password, Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> LoginCoroutine(string email, string password, Action resolve, Action<Exception> reject) {
            IsLoading = true;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                reject(new ApplicationException("Login request error:\nFill out both email and password before logging in."));
                yield break;
            }

            using (var getRequest = UnityWebRequest.Get(URLConstants.LogInAPI)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Login request error:\n", getRequest));
                    yield break;
                }

                var requestToken = GetRequestToken(getRequest.GetResponseBody());
                if (string.IsNullOrEmpty(requestToken)) {
                    reject(new ApplicationException("Login request error:\nFailed to get login request token."));
                    yield break;
                }

                var form = new WWWForm();
                form.AddField(RequestTokenName, requestToken);
                form.AddField("Email", email);
                form.AddField("Password", password);
                form.AddField("RememberMe", "false");

                using (var postRequest = UnityWebRequest.Post(URLConstants.LogInAPI, form).NoRedirect().WithCookies(CookiesString)) {
                    yield return postRequest.SendWebRequest();
                    if (postRequest.error != "Redirect limit exceeded" && CheckAndLogError(postRequest)) {
                        reject(GenerateException("Login request error:\n", postRequest));
                        yield break;
                    }

                    VerificationTokenCookie = GetCookie(VerificationTokenName, postRequest);
                    if (string.IsNullOrEmpty(VerificationTokenCookie)) {
                        reject(new ApplicationException("Login request error:\nAuthentication failed."));
                        yield break;
                    }
                    resolve();
                }
            }
        }
        #endregion

        #region Logout
        public static IPromise Logout() => new Promise((resolve, reject) => MichelangeloSingleton.Coroutine(LogoutCoroutine(Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> LogoutCoroutine(Action resolve, Action<Exception> reject) {
            IsLoading = true;
            using (var getRequest = UnityWebRequest.Get(URLConstants.MainPage).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Logout request error:\n", getRequest));
                    yield break;
                }

                var requestToken = GetRequestToken(getRequest.GetResponseBody());
                if (string.IsNullOrEmpty(requestToken)) {
                    reject(new ApplicationException("Logout request error:\nFailed to get logout request token."));
                    yield break;
                }

                var form = new WWWForm();
                form.AddField(RequestTokenName, requestToken);

                using (var postRequest = UnityWebRequest.Post(URLConstants.LogOutAPI, form).NoRedirect().WithCookies(CookiesString)) {
                    yield return postRequest.SendWebRequest();
                    if (postRequest.error != "Redirect limit exceeded" && CheckAndLogError(postRequest)) {
                        reject(GenerateException("Logout request error:\n", postRequest));
                        yield break;
                    }

                    DeleteCookies();
                    resolve();
                }
            }
        }
        #endregion

        #region GetUserInfo
        public static IPromise<UserInfo> GetUserInfo() => new Promise<UserInfo>((resolve, reject) => MichelangeloSingleton.Coroutine(GetUserInfoCoroutine(Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> GetUserInfoCoroutine(Action<UserInfo> resolve, Action<Exception> reject) {
            IsLoading = true;
            using (var getRequest = UnityWebRequest.Get(URLConstants.MeAPI).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("User info request error:\n", getRequest));
                    yield break;
                }
                resolve(UserInfo.FromJson(getRequest.GetResponseBody()));
            }
        }
        #endregion

        #region GetGrammarArray
        public static IPromise<Grammar[]> GetMyGrammarArray() => new Promise<Grammar[]>((resolve, reject) => MichelangeloSingleton.Coroutine(GetGrammarArrayCoroutine(URLConstants.GrammarAPI, Wrap(resolve), Wrap(reject))));
        public static IPromise<Grammar[]> GetSharedGrammarArray() => new Promise<Grammar[]>((resolve, reject) => MichelangeloSingleton.Coroutine(GetGrammarArrayCoroutine(URLConstants.SharedAPI, Wrap(resolve), Wrap(reject))));
        public static IPromise<Grammar[]> GetTutorialGrammarArray() => new Promise<Grammar[]>((resolve, reject) => MichelangeloSingleton.Coroutine(GetGrammarArrayCoroutine(URLConstants.TutorialAPI, Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> GetGrammarArrayCoroutine(string api, Action<Grammar[]> resolve, Action<Exception> reject) {
            IsLoading = true;
            using (var getRequest = UnityWebRequest.Get(api).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Grammar array request error:\n", getRequest));
                    yield break;
                }

                var grammarArray = Grammar.FromJSONArray(getRequest.GetResponseBody());
                if (api == URLConstants.TutorialAPI) {
                    foreach (var grammar in grammarArray) {
                        grammar.isTutorial = true;
                    }
                }
                resolve(grammarArray);
            }
        }
        #endregion

        #region CreateGrammar
        public static IPromise<Grammar> CreateGrammar() => new Promise<Grammar>((resolve, reject) => MichelangeloSingleton.Coroutine(CreateGrammarCoroutine(Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> CreateGrammarCoroutine(Action<Grammar> resolve, Action<Exception> reject) {
            IsLoading = true;
            using (var putRequest = UnityWebRequest.Put(URLConstants.GrammarAPI, "null").WithCookies(CookiesString)) {
                yield return putRequest.SendWebRequest();
                if (CheckAndLogError(putRequest)) {
                    reject(GenerateException("Create grammar request error:\n", putRequest));
                    yield break;
                }
                resolve(Grammar.FromJSON(putRequest.GetResponseBody()));
            }
        }
        #endregion

        #region GetGrammar
        public static IPromise<Grammar> GetGrammar(string grammarId) => new Promise<Grammar>((resolve, reject) => MichelangeloSingleton.Coroutine(GetGrammarCoroutine(URLConstants.GrammarAPI, grammarId, Wrap(resolve), Wrap(reject))));
        public static IPromise<Grammar> GetTutorial(string grammarId) => new Promise<Grammar>((resolve, reject) => MichelangeloSingleton.Coroutine(GetGrammarCoroutine(URLConstants.TutorialAPI, grammarId, Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> GetGrammarCoroutine(string api, string grammarId, Action<Grammar> resolve, Action<Exception> reject) {
            IsLoading = true;
            using (var getRequest = UnityWebRequest.Get(api + "/" + grammarId).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Get grammar request error:\n", getRequest));
                    yield break;
                }
                resolve(Grammar.FromJSON(getRequest.GetResponseBody()));
            }
        }
        #endregion

        #region DeleteGrammar
        public static IPromise DeleteGrammar(string grammarId) => new Promise((resolve, reject) => MichelangeloSingleton.Coroutine(DeleteGrammarCoroutine(grammarId, Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> DeleteGrammarCoroutine(string grammarId, Action resolve, Action<Exception> reject) {
            IsLoading = true;
            using (var deleteRequest = UnityWebRequest.Delete(URLConstants.GrammarAPI + "/" + grammarId).WithCookies(CookiesString)) {
                yield return deleteRequest.SendWebRequest();
                if (CheckAndLogError(deleteRequest)) {
                    reject(GenerateException("Delete grammar request error:\n", deleteRequest));
                    yield break;
                }
                resolve();
            }
        }
        #endregion
        
        #region GenerateGrammar
        public static IPromise<GenerateGrammarResponse> GenerateGrammar(Grammar grammar) => new Promise<GenerateGrammarResponse>((resolve, reject) => MichelangeloSingleton.Coroutine(GenerateGrammarCoroutine(grammar, Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> GenerateGrammarCoroutine(Grammar grammar, Action<GenerateGrammarResponse> resolve, Action<Exception> reject) {
            IsLoading = true;
            var form = new WWWForm();
            form.AddField("ID", grammar.id);
            form.AddField("Name", grammar.name);
            form.AddField("Type", grammar.type);
            form.AddField("Code", grammar.SourceCode?.text ?? grammar.code);
            form.AddField("OnlyNID", uint.MaxValue.ToString());
            form.AddField("Render", "false");

            using (var postRequest = UnityWebRequest.Post(URLConstants.GrammarAPI, form).WithCookies(CookiesString)) {
                yield return postRequest.SendWebRequest();
                if (CheckAndLogError(postRequest)) {
                    reject(GenerateException("Generate grammar request error:\n", postRequest));
                    yield break;
                }
                
                var response = MessagePackSerializer.Deserialize<PostResponseModel>(postRequest.downloadHandler.data);
                MichelangeloSingleton.Coroutine(GetResponseCoroutine(grammar.id, response.IMG, resolve, reject));
            }
        }
        #endregion

        #region GenerateScene
        public static IPromise<GenerateGrammarResponse> GenerateScene(string code) => new Promise<GenerateGrammarResponse>((resolve, reject) => MichelangeloSingleton.Coroutine(GenerateSceneCoroutine(code, Wrap(resolve), Wrap(reject))));
        private static IEnumerator<UnityWebRequestAsyncOperation> GenerateSceneCoroutine(string code, Action<GenerateGrammarResponse> resolve, Action<Exception> reject) {
            IsLoading = true;
            var form = new WWWForm();
            form.AddField("Type", "DOG");
            form.AddField("Code", code);

            using (var postRequest = UnityWebRequest.Post(URLConstants.SceneAPI, form).WithCookies(CookiesString)) {
                yield return postRequest.SendWebRequest();
                if (CheckAndLogError(postRequest)) {
                    reject(GenerateException("Generate scene request error:\n", postRequest));
                    yield break;
                }
                
                var response = MessagePackSerializer.Deserialize<PostResponseModel>(postRequest.downloadHandler.data);
                MichelangeloSingleton.Coroutine(GetResponseCoroutine(response.Info, response.IMG, resolve, reject));
            }
        }
        #endregion
        
        #region GetResponse
        private static IEnumerator<UnityWebRequestAsyncOperation> GetResponseCoroutine(string id, string token, Action<GenerateGrammarResponse> resolve, Action<Exception> reject) {
            PostResponseModel response;
            bool isGenerating;
            do {
                using (var getRequest = UnityWebRequest.Get(URLConstants.GrammarAPI + "/" + id + "/Response/" + token).WithCookies(CookiesString)) {
                    yield return getRequest.SendWebRequest();
                    if (CancelGeneration) {
                        CancelGeneration = false;
                        reject(new ApplicationException("Generate grammar request error:\nGeneration canceled by user."));
                        yield break;
                    }
                    if (CheckAndLogError(getRequest)) {
                        reject(GenerateException("Generate grammar request error:\n", getRequest));
                        yield break;
                    }
                    response = MessagePackSerializer.Deserialize<PostResponseModel>(getRequest.downloadHandler.data);
                    isGenerating = response.Objects == null || response.Objects?.Length == 0;
                    if (isGenerating && !String.IsNullOrEmpty(response.Errors)) {
                        reject(new ApplicationException(Regex.Replace(response.Errors, "<br\\/>", "\n")));
                        yield break;
                    }
                }
            } while (isGenerating);
            resolve(new GenerateGrammarResponse {
                ParseTree = new ParseTree(response.ParseTree),
                Materials = response.Materials,
                ErrorMessage = Regex.Replace(response.Errors, "<br\\/>", "\n")
            });
        }
        #endregion

        #region Helper methods
        private static string GetRequestToken(string source) => RequestTokenRegex.Match(source).ToString().Split('\"').Last();
        private static string GetCookie(string cookie, UnityWebRequest request) {
            var cookieHeader = request.GetResponseHeader(SetCookieName);
            return string.IsNullOrEmpty(cookieHeader) ? null : new Regex(cookie + "=(?<c>[^;]*)").Match(cookieHeader).Groups["c"].Value;
        }

        private static bool CheckAndLogError(UnityWebRequest request) {
            if (!request.isNetworkError && !request.isHttpError) {
                Debug.Log(request.Info());
                return false;
            }
            Debug.LogError(request.Info());

            if (request.responseCode == 401) {
                DeleteCookies();
                Debug.LogError("Unauthorized response code received. Deleting cookies...");
            }
            return true;
        }

        private static Exception GenerateException(string message, UnityWebRequest request) => 
            request.isHttpError ? (Exception) new WebRequestException(message, request.responseCode) : new ApplicationException(message + request.error);

        private static void DeleteCookies() {
            VerificationTokenCookie = null;
        }

        private static Action Wrap(Action action) => () => { IsLoading = false; action(); };
        private static Action<T1> Wrap<T1>(Action<T1> action) => (t1) => { IsLoading = false; action(t1); };
        #endregion
    }
}
