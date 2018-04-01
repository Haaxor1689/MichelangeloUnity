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

namespace Michelangelo {
	public static class WebAPI {
		#region Regexes
		private static readonly Regex LoginTokenRegex = new Regex("<form action=\"\\/Account\\/Login\" .*><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"[^\"]*");
		#endregion

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

				SetCookie(HeaderConstants.RequestVerificationTokenCookieName, getRequest);

				var form = new WWWForm();
				form.AddField(HeaderConstants.RequestVerificationTokenCookieName, GetLoginToken(getRequest.GetResponseBody()));
				form.AddField("Email", email);
				form.AddField("Password", password);
				form.AddField("RememberMe", "false");

				UnityWebRequest.Post(URLConstants.LogInAPI, form).WithCookies(cookiesString).Then(postRequest => {
					if (postRequest.isNetworkError || postRequest.isHttpError) {
						Debug.LogError("Login request error: " + postRequest.error);
						return;
					}
					SetCookie(HeaderConstants.ApplicationCookieName, postRequest);
					Debug.Log(cookiesString);
				});
			});
		}

		private static string GetLoginToken(string source) {
			return LoginTokenRegex
				.Match(source)
				.ToString()
				.Split('\"')
				.Last();
		}

		private static void SetCookie(string cookie, UnityWebRequest request) {
			Debug.Log(request.GetResponseHeader(HeaderConstants.SetCookie));
			cookies[cookie] = new Regex(cookie + "=[^;]*")
				.Match(request
					.GetResponseHeader(HeaderConstants.SetCookie))
				.ToString()
				.Split('=')
				.Last();
		}
	}
}