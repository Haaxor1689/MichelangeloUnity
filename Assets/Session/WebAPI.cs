using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Michelangelo;
using Michelangelo.Model;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using ErrorMessage = System.String;

namespace Michelangelo.Session {
	public static class WebAPI {
		private static readonly Regex RequestTokenRegex = new Regex("<form action=\"\\/Account\\/Log(in|Off)\" .*><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"[^\"]*");
		private static readonly string SetCookieName = "Set-Cookie";
		private static readonly string RequestTokenName = "__RequestVerificationToken";
		private static readonly string VerificationTokenName = ".AspNet.ApplicationCookie";

		private static StringStringDictionary cookies = new StringStringDictionary();

		private static string cookiesString {
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

		public static bool IsLoggedIn {
			get {
				return cookies.ContainsKey(VerificationTokenName) && cookies[VerificationTokenName] != null;
			}
		}

		public static void Login(string email, string password, Action<ErrorMessage> completion = null) {
			if (email == null || password == null) {
				if (completion != null) completion("Fill out both email and password before logging in.");
				return;
			}

			UnityWebRequest.Get(URLConstants.LogInAPI).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(getRequest.error);
					return;
				}
				try {
					SetCookie(RequestTokenName, getRequest);
				} catch (ResponseParseException) {
					if (completion != null) completion("Failed to get request verification token.");
					return;
				}
				Debug.Log(getRequest.Info());

				var form = new WWWForm();
				try {
					form.AddField(RequestTokenName, GetRequestToken(getRequest.GetResponseBody()));
				} catch (ResponseParseException) {
					if (completion != null) completion("Failed to get login request token.");
					return;
				}
				form.AddField("Email", email);
				form.AddField("Password", password);
				form.AddField("RememberMe", "false");

				UnityWebRequest.Post(URLConstants.LogInAPI, form).NoRedirect().WithCookies(cookiesString).Then(postRequest => {
					if ((postRequest.isNetworkError || postRequest.isHttpError) && postRequest.error != "Redirect limit exceeded") {
						if (completion != null) completion("Login request error: " + postRequest.error);
						return;
					}
					Debug.Log(postRequest.Info());
					try {
						SetCookie(VerificationTokenName, postRequest);
					} catch (ResponseParseException) {
						if (completion != null) completion("Authentication failed.");
						return;
					}
					if (completion != null) completion(null);
				});
			});
		}

		public static void Logout(Action<ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.MainPage).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());

				var form = new WWWForm();
				try {
					form.AddField(RequestTokenName, GetRequestToken(getRequest.GetResponseBody()));
				} catch (ResponseParseException) {
					if (completion != null) completion("Failed to get logout request token.");
					return;
				}

				UnityWebRequest.Post(URLConstants.LogOutAPI, form).NoRedirect().WithCookies(cookiesString).Then(postRequest => {
					if ((postRequest.isNetworkError || postRequest.isHttpError) && postRequest.error != "Redirect limit exceeded") {
						if (completion != null) completion("Logout request error: " + postRequest.error);
						return;
					}
					Debug.Log(postRequest.Info());
					cookies = new StringStringDictionary();
					if (completion != null) completion(null);
				});
			});
		}

		public static void GetMainPage(Action<ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.MainPage).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				PageFromResponse(getRequest.GetResponseBody());
				if (completion != null) completion(null);
			});
		}

		public static void GetUserInfo(Action<UserInfo, ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.MeAPI).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(UserInfo.FromJson(getRequest.GetResponseBody()), null);
			});
		}

		public static void CreateGrammar(Action<Grammar, ErrorMessage> completion = null) {
			UnityWebRequest.Put(URLConstants.GrammarAPI, "null").WithCookies(cookiesString).Then(putRequest => {
				if (putRequest.isNetworkError || putRequest.isHttpError) {
					if (completion != null) completion(null, putRequest.error);
					return;
				}
				Debug.Log(putRequest.Info());
				if (completion != null) completion(Grammar.FromJson(putRequest.GetResponseBody()), null);
			});
		}

		public static void GetGrammar(Action<Grammar[], ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.GrammarAPI).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(Grammar.FromJsonArray(getRequest.GetResponseBody()), null);
			});
		}

		public static void GetShared(Action<Grammar[], ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.SharedAPI).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(Grammar.FromJsonArray(getRequest.GetResponseBody()), null);
			});
		}

		public static void GetTutorials(Action<Grammar[], ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.TutorialAPI).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(Grammar.FromJsonArray(getRequest.GetResponseBody()), null);
			});
		}

		#region Helper methods
		private static string GetRequestToken(string source) {
			return CheckNonEmpty(RequestTokenRegex
				.Match(source)
				.ToString()
				.Split('\"')
				.Last());
		}

		private static void SetCookie(string cookie, UnityWebRequest request) {
			cookies[cookie] = CheckNonEmpty(new Regex(cookie + "=[^;]*")
				.Match(request.GetResponseHeader(SetCookieName))
				.ToString()
				.Split('=')
				.Last());
		}

		private static string CheckNonEmpty(string str) {
			if (str == null || str == "")
				throw new ResponseParseException();
			return str;
		}

		private static void PageFromResponse(string response) {
			System.IO.File.WriteAllText(System.IO.Path.Combine(Application.dataPath, "page.html"), response);
		}

		private static void JSONFromResponse(string response) {
			System.IO.File.WriteAllText(System.IO.Path.Combine(Application.dataPath, "response.json"), response);
		}
		#endregion
	}
}