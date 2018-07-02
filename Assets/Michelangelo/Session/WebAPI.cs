using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Michelangelo.Model;
using Michelangelo.Model.Render;
using Michelangelo.Utility;
using RSG;
using SimpleJSON;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Michelangelo.Session {
    public class WebAPI : MonoBehaviour {
        private static readonly Regex RequestTokenRegex = new Regex("<form action=\"\\/Account\\/Log(in|Off)\" .*><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"[^\"]*");
        private static readonly string SetCookieName = "Set-Cookie";
        private static readonly string RequestTokenName = "__RequestVerificationToken";
        private static readonly string VerificationTokenName = ".AspNet.ApplicationCookie";
        private static readonly string MichelangeloRequestToken = "Michelangelo" + RequestTokenName;
        private static readonly string MichelangeloVerificationToken = "Michelangelo" + VerificationTokenName;

        #region Attributes
        private static StringStringDictionary cookies = new StringStringDictionary();
        private static WebAPI shared;

        private static WebAPI Shared {
            get {
                if (shared != null && (shared = FindObjectOfType(typeof(WebAPI)) as WebAPI) != null) {
                    return shared;
                }
                var singleton = new GameObject("MichelangeloWebAPISingleton") { hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector };
                return shared = singleton.AddComponent<WebAPI>();
            }
        }

        private static string CookiesString {
            get {
                var builder = new StringBuilder();
                foreach (var pair in cookies) {
                    builder
                        .Append(pair.Key)
                        .Append("=")
                        .Append(pair.Value)
                        .Append("; ");
                }
                return builder.ToString();
            }
        }

        public static bool IsAuthenticated {
            get {
                if (cookies.ContainsKey(VerificationTokenName) && cookies.ContainsKey(RequestTokenName)) {
                    return cookies[VerificationTokenName] != null && cookies[RequestTokenName] != null;
                }
                var verificationToken = EditorPrefs.GetString(MichelangeloVerificationToken);
                if (verificationToken == "") {
                    return false;
                }
                var requestToken = EditorPrefs.GetString(MichelangeloRequestToken);
                if (requestToken == "") {
                    return false;
                }
                cookies.Add(VerificationTokenName, verificationToken);
                cookies.Add(RequestTokenName, requestToken);
                return true;
            }
        }
        #endregion

        #region Login
        public static IPromise Login(string email, string password) => new Promise((resolve, reject) => Shared.StartCoroutine(LoginCoroutine(email, password, resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> LoginCoroutine(string email, string password, Action resolve, Action<Exception> reject) {
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
                try {
                    SetCookie(RequestTokenName, getRequest);
                    EditorPrefs.SetString(MichelangeloRequestToken, cookies[RequestTokenName]);
                } catch (ResponseParseException) {
                    reject(new ApplicationException("Login request error:\nFailed to get request verification token."));
                    yield break;
                }
                Debug.Log(getRequest.Info());

                var form = new WWWForm();
                try {
                    form.AddField(RequestTokenName, GetRequestToken(getRequest.GetResponseBody()));
                } catch (ResponseParseException) {
                    reject(new ApplicationException("Login request error:\nFailed to get login request token."));
                    yield break;
                }
                form.AddField("Email", email);
                form.AddField("Password", password);
                form.AddField("RememberMe", "false");

                using (var postRequest = UnityWebRequest.Post(URLConstants.LogInAPI, form).NoRedirect().WithCookies(CookiesString)) {
                    yield return postRequest.SendWebRequest();
                    if (postRequest.error != "Redirect limit exceeded" && CheckAndLogError(postRequest)) {
                        reject(GenerateException("Login request error:\n", postRequest));
                        yield break;
                    }
                    Debug.Log(postRequest.Info());
                    try {
                        SetCookie(VerificationTokenName, postRequest);
                        EditorPrefs.SetString(MichelangeloVerificationToken, cookies[VerificationTokenName]);
                    } catch (ResponseParseException) {
                        reject(new ApplicationException("Login request error:\nAuthentication failed."));
                        yield break;
                    }
                    resolve();
                }
            }
        }
        #endregion

        #region Logout
        public static IPromise Logout() => new Promise((resolve, reject) => Shared.StartCoroutine(LogoutCoroutine(resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> LogoutCoroutine(Action resolve, Action<Exception> reject) {
            using (var getRequest = UnityWebRequest.Get(URLConstants.MainPage).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Logout request error:\n", getRequest));
                    yield break;
                }

                Debug.Log(getRequest.Info());
                var form = new WWWForm();
                try {
                    form.AddField(RequestTokenName, GetRequestToken(getRequest.GetResponseBody()));
                } catch (ResponseParseException) {
                    reject(new ApplicationException("Logout request error:\nFailed to get logout request token."));
                    yield break;
                }

                using (var postRequest = UnityWebRequest.Post(URLConstants.LogOutAPI, form).NoRedirect().WithCookies(CookiesString)) {
                    yield return postRequest.SendWebRequest();

                    if (postRequest.error != "Redirect limit exceeded" && CheckAndLogError(postRequest)) {
                        reject(GenerateException("Logout request error:\n", postRequest));
                        yield break;
                    }
                    Debug.Log(postRequest.Info());
                    cookies = new StringStringDictionary();
                    EditorPrefs.DeleteKey(MichelangeloRequestToken);
                    EditorPrefs.DeleteKey(MichelangeloVerificationToken);
                    resolve();
                }
            }
        }
        #endregion

        #region GetMainPage
        public static IPromise GetMainPage() => new Promise((resolve, reject) => Shared.StartCoroutine(GetMainPageCoroutine(resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> GetMainPageCoroutine(Action resolve, Action<Exception> reject) {
            using (var getRequest = UnityWebRequest.Get(URLConstants.MainPage).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Main page request error:\n", getRequest));
                    yield break;
                }
                Debug.Log(getRequest.Info());
                resolve();
            }
        }
        #endregion

        #region GetUserInfo
        public static IPromise<UserInfo> GetUserInfo() => new Promise<UserInfo>((resolve, reject) => Shared.StartCoroutine(GetUserInfoCoroutine(resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> GetUserInfoCoroutine(Action<UserInfo> resolve, Action<Exception> reject) {
            using (var getRequest = UnityWebRequest.Get(URLConstants.MeAPI).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();
                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("User info request error:\n", getRequest));
                    yield break;
                }
                Debug.Log(getRequest.Info());
                resolve(UserInfo.FromJson(getRequest.GetResponseBody()));
            }
        }
        #endregion

        #region GetGrammarArray
        public static IPromise<Grammar[]> GetMyGrammarArray() 
            => new Promise<Grammar[]>((resolve, reject) => Shared.StartCoroutine(GetGrammarArrayCoroutine(URLConstants.GrammarAPI, resolve, reject)));

        public static IPromise<Grammar[]> GetSharedGrammarArray() 
            => new Promise<Grammar[]>((resolve, reject) => Shared.StartCoroutine(GetGrammarArrayCoroutine(URLConstants.SharedAPI, resolve, reject)));

        public static IPromise<Grammar[]> GetTutorialGrammarArray() 
            => new Promise<Grammar[]>((resolve, reject) => Shared.StartCoroutine(GetGrammarArrayCoroutine(URLConstants.TutorialAPI, resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> GetGrammarArrayCoroutine(string api, Action<Grammar[]> resolve, Action<Exception> reject) {
            using (var getRequest = UnityWebRequest.Get(api).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();

                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Grammar array request error:\n", getRequest));
                    yield break;
                }
                Debug.Log(getRequest.Info());
                resolve(Grammar.FromJSONArray(getRequest.GetResponseBody()));
            }
        }
        #endregion

        #region CreateGrammar
        public static IPromise<Grammar> CreateGrammar() => new Promise<Grammar>((resolve, reject) => Shared.StartCoroutine(CreateGrammarCoroutine(resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> CreateGrammarCoroutine(Action<Grammar> resolve, Action<Exception> reject) {
            using (var putRequest = UnityWebRequest.Put(URLConstants.GrammarAPI, "null").WithCookies(CookiesString)) {
                yield return putRequest.SendWebRequest();
                if (CheckAndLogError(putRequest)) {
                    reject(GenerateException("Create grammar request error:\n", putRequest));
                    yield break;
                }
                Debug.Log(putRequest.Info());
                resolve(Grammar.FromJSON(putRequest.GetResponseBody()));
            }
        }
        #endregion

        #region GetGrammar
        public static IPromise<Grammar> GetGrammar(string grammarId) => new Promise<Grammar>((resolve, reject) => Shared.StartCoroutine(GetGrammarCoroutine(URLConstants.GrammarAPI, grammarId, resolve, reject)));

        public static IPromise<Grammar> GetTutorial(string grammarId) => new Promise<Grammar>((resolve, reject) => Shared.StartCoroutine(GetGrammarCoroutine(URLConstants.TutorialAPI, grammarId, resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> GetGrammarCoroutine(string api, string grammarId, Action<Grammar> resolve, Action<Exception> reject) {
            using (var getRequest = UnityWebRequest.Get(api + "/" + grammarId).WithCookies(CookiesString)) {
                yield return getRequest.SendWebRequest();

                if (CheckAndLogError(getRequest)) {
                    reject(GenerateException("Get grammar request error:\n", getRequest));
                    yield break;
                }
                Debug.Log(getRequest.Info());
                resolve(Grammar.FromJSON(getRequest.GetResponseBody()));
            }
        }
        #endregion

        #region DeleteGrammar
        public static IPromise DeleteGrammar(string grammarId) => new Promise((resolve, reject) => Shared.StartCoroutine(DeleteGrammarCoroutine(grammarId, resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> DeleteGrammarCoroutine(string grammarId, Action resolve, Action<Exception> reject) {
            using (var deleteRequest = UnityWebRequest.Delete(URLConstants.GrammarAPI + "/" + grammarId).WithCookies(CookiesString)) {
                yield return deleteRequest.SendWebRequest();

                if (CheckAndLogError(deleteRequest)) {
                    reject(GenerateException("Delete grammar request error:\n", deleteRequest));
                    yield break;
                }
                Debug.Log(deleteRequest.Info());
                resolve();
            }
        }
        #endregion

        #region GenerateGrammar
        public static IPromise<ModelMesh> GenerateGrammar(Grammar grammar) => new Promise<ModelMesh>((resolve, reject) => Shared.StartCoroutine(GenerateGrammarCoroutine(grammar, resolve, reject)));

        private static IEnumerator<UnityWebRequestAsyncOperation> GenerateGrammarCoroutine(Grammar grammar, Action<ModelMesh> resolve, Action<Exception> reject) {
            var form = new WWWForm();
            form.AddField("ID", grammar.id);
            form.AddField("Name", grammar.name);
            form.AddField("Type", grammar.type);
            form.AddField("Code", grammar.code);
            form.AddField("OnlyNID", uint.MaxValue.ToString());
            form.AddField("Render", "false");

            using (var postRequest = UnityWebRequest.Post(URLConstants.GrammarAPI, form).WithCookies(CookiesString)) {
                yield return postRequest.SendWebRequest();
                if (CheckAndLogError(postRequest)) {
                    reject(GenerateException("Generate grammar request error:\n", postRequest));
                    yield break;
                }
                Debug.Log(postRequest.Info());
                string rawJson;
                bool isGenerating;
                var token = new Regex("\"img\":\"(?<t>.*?)\"").Match(postRequest.GetResponseBody()).Groups["t"].Value;
                do {
                    using (var getRequest = UnityWebRequest.Get(URLConstants.GrammarAPI + "/" + grammar.id + "/Response/" + token).WithCookies(CookiesString)) {
                        yield return getRequest.SendWebRequest();
                        if (CheckAndLogError(getRequest)) {
                            reject(GenerateException("Generate grammar request error:\n", getRequest));
                            yield break;
                        }

                        Debug.Log(getRequest.Info());
                        rawJson = getRequest.GetResponseBody();
                        var match = Regex.Match(rawJson, "\"e\":\"([^\"]+)\"");
                        if (match.Success) {
                            reject(new ApplicationException($"Server responded with error:\n{Regex.Replace(match.Groups[1].Value, "<br\\/>", "\n")}"));
                            yield break;
                        }
                        isGenerating = Regex.IsMatch(rawJson, "\"ml\":\\{\\},\"o\":\\[\\],");
                    }
                } while (isGenerating);
                resolve(new ModelMesh(rawJson));
            }
        }
        #endregion

        #region Helper methods
        private static string GetRequestToken(string source) => CheckNonEmpty(RequestTokenRegex.Match(source).ToString().Split('\"').Last());

        private static void SetCookie(string cookie,UnityWebRequest request) 
            => cookies[cookie] = CheckNonEmpty(new Regex(cookie + "=(?<c>[^;]*)").Match(request.GetResponseHeader(SetCookieName)).Groups["c"].Value);

        private static string CheckNonEmpty(string str) {
            if (string.IsNullOrEmpty(str)) {
                throw new ResponseParseException();
            }
            return str;
        }

        private static void PageFromResponse(string response) => File.WriteAllText(Path.Combine(Application.dataPath,"page.html"),response);

        private static void JSONFromResponse(string filename,string response) => File.WriteAllText(Path.Combine(Application.dataPath,filename + ".json"),response);

        private static bool CheckAndLogError(UnityWebRequest request) {
            if (!request.isNetworkError && !request.isHttpError) {
                return false;
            }

            Debug.LogError(request.Info());
            if (request.responseCode == 401) {
                cookies = new StringStringDictionary();
                EditorPrefs.DeleteKey(MichelangeloRequestToken);
                EditorPrefs.DeleteKey(MichelangeloVerificationToken);
                Debug.LogError("Unauthorized response code received. Deleting cookies...");
            }
            return true;
        }

        private static Exception GenerateException(string message, UnityWebRequest request) {
            if (request.isHttpError) {
                return new WebRequestException(message, request.responseCode);
            }
            return new ApplicationException(message + request.error);
        }
        #endregion
    }
}
