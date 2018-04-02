using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Michelangelo;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Michelangelo.Session {
	public static class WebAPI {
		private static readonly Regex RequestTokenRegex = new Regex("<form action=\"\\/Account\\/Log(in|Off)\" .*><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"[^\"]*");
		private static readonly string SetCookieName = "Set-Cookie";
		private static readonly string RequestTokenName = "__RequestVerificationToken";
		private static readonly string VerificationTokenName = ".AspNet.ApplicationCookie";

		[SerializeField]
		private static Dictionary<string, string> cookies = new Dictionary<string, string>();

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

		public static bool IsLoggedIn { get { return cookies.ContainsKey(VerificationTokenName) && cookies[VerificationTokenName] != null; } }

		public static void Login(string email, string password) {
			if (email == null || password == null) {
				Debug.LogError("Fill out both email and password before logging in.");
				return;
			}

			UnityWebRequest.Get(URLConstants.LogInAPI).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					Debug.Log(getRequest.error);
					return;
				}

				SetCookie(RequestTokenName, getRequest);
				Debug.Log(getRequest.String());

				var form = new WWWForm();
				try {
					form.AddField(RequestTokenName, GetRequestToken(getRequest.GetResponseBody()));
				} catch (ResponseParseException ex) {
					throw new ResponseParseException("Failed to get login request token.", ex);
				}
				form.AddField("Email", email);
				form.AddField("Password", password);
				form.AddField("RememberMe", "false");

				UnityWebRequest.Post(URLConstants.LogInAPI, form).NoRedirect().WithCookies(cookiesString).Then(postRequest => {
					if ((postRequest.isNetworkError || postRequest.isHttpError) && postRequest.error != "Redirect limit exceeded") {
						Debug.LogError("Login request error: " + postRequest.error);
						return;
					}
					Debug.Log(postRequest.String());
					SetCookie(VerificationTokenName, postRequest);
					PageFromResponse(postRequest.GetResponseBody());
				});
			});
		}

		public static void Logout() {
			UnityWebRequest.Get(URLConstants.MainPage).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					Debug.Log(getRequest.error);
					return;
				}
				Debug.Log(getRequest.String());

				var form = new WWWForm();
				try {
					form.AddField(RequestTokenName, GetRequestToken(getRequest.GetResponseBody()));
				} catch (ResponseParseException ex) {
					throw new ResponseParseException("Failed to get logout request token.", ex);
				}

				UnityWebRequest.Post(URLConstants.LogOutAPI, form).NoRedirect().WithCookies(cookiesString).Then(postRequest => {
					if ((postRequest.isNetworkError || postRequest.isHttpError) && postRequest.error != "Redirect limit exceeded") {
						Debug.LogError("Logout request error: " + postRequest.error);
						return;
					}
					Debug.Log(postRequest.String());
					SetCookie(RequestTokenName, postRequest);
					PageFromResponse(postRequest.GetResponseBody());
				});
			});
		}

		public static void GetMainPage() {
			UnityWebRequest.Get(URLConstants.MainPage).WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					Debug.Log(getRequest.error);
					return;
				}
				Debug.Log(getRequest.String());
				PageFromResponse(getRequest.GetResponseBody());
			});
		}

		public static void CreateGrammar() {
			UnityWebRequest.Put(URLConstants.OwnGrammarAPI, "a").WithCookies(cookiesString).Then(getRequest => {
				if (getRequest.isNetworkError || getRequest.isHttpError) {
					Debug.Log(getRequest.error);
					return;
				}
				Debug.Log(getRequest.String());
				JSONFromResponse(getRequest.GetResponseBody());
			});
		}

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
	}
}