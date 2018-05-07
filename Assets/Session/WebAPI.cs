﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Michelangelo;
using Michelangelo.Model;
using Michelangelo.Model.Render;
using Michelangelo.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using ErrorMessage = System.String;
using SimpleJSON;

namespace Michelangelo.Session {
	public static class WebAPI {
		private static readonly Regex RequestTokenRegex = new Regex("<form action=\"\\/Account\\/Log(in|Off)\" .*><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"[^\"]*");
		private static readonly string SetCookieName = "Set-Cookie";
		private static readonly string RequestTokenName = "__RequestVerificationToken";
		private static readonly string VerificationTokenName = ".AspNet.ApplicationCookie";
		private static readonly string MichelangeloRequestToken = "Michelangelo" + RequestTokenName;
		private static readonly string MichelangeloVerificationToken = "Michelangelo" + VerificationTokenName;

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

		public static bool IsAuthenticated {
			get {
				if (cookies.ContainsKey(VerificationTokenName) && cookies.ContainsKey(RequestTokenName))
					return cookies[VerificationTokenName] != null && cookies[RequestTokenName] != null;
				var verificationToken = EditorPrefs.GetString(MichelangeloVerificationToken);
				if (verificationToken == "") return false;
				var requestToken = EditorPrefs.GetString(MichelangeloRequestToken);
				if (requestToken == "") return false;
				cookies.Add(VerificationTokenName, verificationToken);
				cookies.Add(RequestTokenName, requestToken);
				return true;
			}
		}

		public static void Login(string email, string password, Action<ErrorMessage> completion = null) {
			if (email == null || password == null) {
				if (completion != null) completion("Fill out both email and password before logging in.");
				return;
			}

			UnityWebRequest.Get(URLConstants.LogInAPI).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(getRequest.error);
					return;
				}
				try {
					SetCookie(RequestTokenName, getRequest);
					EditorPrefs.SetString(MichelangeloRequestToken, cookies[RequestTokenName]);
				} catch (ResponseParseException) {
					if (completion != null) completion("Failed to get request verification token.");
					return;
				}
				Debug.Log(getRequest.Info());
				PageFromResponse(getRequest.GetResponseBody());

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
					if (postRequest.error != "Redirect limit exceeded" && CheckAndLogError(postRequest)) {
						if (completion != null) completion("Login request error: " + postRequest.error);
						return;
					}
					Debug.Log(postRequest.Info());
					try {
						SetCookie(VerificationTokenName, postRequest);
						EditorPrefs.SetString(MichelangeloVerificationToken, cookies[VerificationTokenName]);
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
				if (CheckAndLogError(getRequest)) {
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
					if (postRequest.error != "Redirect limit exceeded" && CheckAndLogError(postRequest)) {
						if (completion != null) completion("Logout request error: " + postRequest.error);
						return;
					}
					Debug.Log(postRequest.Info());
					cookies = new StringStringDictionary();
					EditorPrefs.DeleteKey(MichelangeloRequestToken);
					EditorPrefs.DeleteKey(MichelangeloVerificationToken);
					if (completion != null) completion(null);
				});
			});
		}

		public static void GetMainPage(Action<ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.MainPage).WithCookies(cookiesString).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
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
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(UserInfo.FromJson(getRequest.GetResponseBody()), null);
			});
		}

		public static void CreateGrammar(Action<Grammar, ErrorMessage> completion = null) {
			UnityWebRequest.Put(URLConstants.GrammarAPI, "null").WithCookies(cookiesString).Then(putRequest => {
				if (CheckAndLogError(putRequest)) {
					if (completion != null) completion(null, putRequest.error);
					return;
				}
				Debug.Log(putRequest.Info());
				JSONFromResponse("newGrammar", putRequest.GetResponseBody());
				if (completion != null) completion(Grammar.FromJson(putRequest.GetResponseBody()), null);
			});
		}

		public static void GetMyGrammar(Action<Grammar[], ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.GrammarAPI).WithCookies(cookiesString).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				JSONFromResponse("grammars", getRequest.GetResponseBody());
				if (completion != null) completion(Grammar.FromJsonArray(getRequest.GetResponseBody()), null);
			});
		}

		public static void GetShared(Action<Grammar[], ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.SharedAPI).WithCookies(cookiesString).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				JSONFromResponse("shared", getRequest.GetResponseBody());
				if (completion != null) completion(Grammar.FromJsonArray(getRequest.GetResponseBody()), null);
			});
		}

		public static void GetTutorials(Action<Grammar[], ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.TutorialAPI).WithCookies(cookiesString).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				JSONFromResponse("tutorials", getRequest.GetResponseBody());
				if (completion != null) completion(Grammar.FromJsonArray(getRequest.GetResponseBody()), null);
			});
		}

		public static void GetGrammar(string grammarId, Action<Grammar, ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.GrammarAPI + "/" + grammarId).WithCookies(cookiesString).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(Grammar.FromJson(getRequest.GetResponseBody()), null);
			});
		}

		public static void GenerateGrammar(Grammar grammar, Action<ModelMesh, ErrorMessage> completion = null) {
			var form = new WWWForm();
			form.AddField("ID", grammar.id);
			form.AddField("Name", grammar.name);
			form.AddField("Type", grammar.type);
			form.AddField("Code", grammar.code);

			UnityWebRequest.Post(URLConstants.GrammarAPI, form).WithCookies(cookiesString).Then(postRequest => {
				if (CheckAndLogError(postRequest)) {
					if (completion != null) completion(null, "Generate request error: " + postRequest.error);
					return;
				}

				Debug.Log(postRequest.Info());
				var token = new Regex("\"img\":\"(?<t>.*?)\"").Match(postRequest.GetResponseBody()).Groups["t"].Value;
				UnityWebRequest.Get(URLConstants.GrammarAPI + "/" + grammar.id + "/Response/" + token).WithCookies(cookiesString).Then(getRequest => {
					if (CheckAndLogError(getRequest)) {
						if (completion != null) completion(null, "Generate request error: " + getRequest.error);
						return;
					}

					Debug.Log(getRequest.Info());
					JSONFromResponse("generated", getRequest.GetResponseBody());
					var data = JSON.Parse(getRequest.GetResponseBody());
					var primitives = new List<Primitive>();
					foreach (var obj in data["o"]) {
						var type = obj.Value["g"].Value;
						primitives.Add(new Primitive(
							obj.Value["m"].AsInt,
							type,
							MatrixFromJSON(obj.Value["t"]),
							MeshFromJSON(obj.Value)
						));
					}

					var materials = new List<Color>();
					foreach (var obj in data["ml"]) {
						var groups = new Regex(@"\[(?<r>[\d\.]+), (?<g>[\d\.]+), (?<b>[\d\.]+)\]").Match(obj.Value.Value).Groups;
						float r, g, b;
						if (!float.TryParse(groups["r"].Value, out r) ||
							!float.TryParse(groups["g"].Value, out g) ||
							!float.TryParse(groups["b"].Value, out b)) {
							materials.Add(new Color(1, 1, 1));
						} else {
							materials.Add(new Color(r, g, b));
						}
					}
					completion(new ModelMesh(primitives.ToArray(), materials.ToArray()), null);
				});
			});
		}

		public static void GetTutorial(string grammarId, Action<Grammar, ErrorMessage> completion = null) {
			UnityWebRequest.Get(URLConstants.TutorialAPI + "/" + grammarId).WithCookies(cookiesString).Then(getRequest => {
				if (CheckAndLogError(getRequest)) {
					if (completion != null) completion(null, getRequest.error);
					return;
				}
				Debug.Log(getRequest.Info());
				if (completion != null) completion(Grammar.FromJson(getRequest.GetResponseBody()), null);
			});
		}

		public static void DeleteGrammar(string grammarId, Action<ErrorMessage> completion = null) {
			UnityWebRequest.Delete(URLConstants.GrammarAPI + "/" + grammarId).WithCookies(cookiesString).Then(deleteRequest => {
				if (CheckAndLogError(deleteRequest)) {
					if (completion != null) completion(deleteRequest.error);
					return;
				}
				Debug.Log(deleteRequest.Info());
				if (completion != null) completion(null);
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
			cookies[cookie] = CheckNonEmpty(new Regex(cookie + "=(?<c>[^;]*)")
				.Match(request.GetResponseHeader(SetCookieName)).Groups["c"].Value);
		}

		private static string CheckNonEmpty(string str) {
			if (str == null || str == "")
				throw new ResponseParseException();
			return str;
		}

		private static void PageFromResponse(string response) {
			System.IO.File.WriteAllText(System.IO.Path.Combine(Application.dataPath, "page.html"), response);
		}

		private static void JSONFromResponse(string filename, string response) {
			System.IO.File.WriteAllText(System.IO.Path.Combine(Application.dataPath, filename + ".json"), response);
		}

		private static bool CheckAndLogError(UnityWebRequest request) {
			if (!request.isNetworkError && !request.isHttpError) {
				return false;
			}

			Debug.LogError(request.Info());
			var builder = new StringBuilder();
			builder.Append("Error ");
			builder.Append(request.responseCode.ToString());
			builder.Append(": ");
			builder.Append(request.error.ToString());
			Debug.LogError(builder.ToString());
			if (request.responseCode == 401) {
				cookies = new StringStringDictionary();
				EditorPrefs.DeleteKey(MichelangeloRequestToken);
				EditorPrefs.DeleteKey(MichelangeloVerificationToken);
				Debug.LogError("Unauthorized response code received. Deleting cookies...");
			}
			return true;
		}
		#endregion
		#region Generated model helpers
		private static Matrix4x4 MatrixFromJSON(JSONNode json) {
			var matrix = new Matrix4x4(
				new Vector4(json[0], json[4], json[8], json[12]),
				new Vector4(json[1], json[5], json[9], json[13]),
				new Vector4(json[2], json[6], json[10], json[14]),
				new Vector4(json[3], json[7], json[11], json[15])
			);
			if (matrix.HasNegativeScale()) {
				var scaleMatrix = Matrix4x4.identity;
				scaleMatrix.m00 = -1;
				matrix *= scaleMatrix;
			}
			return matrix;
		}
		private static Mesh MeshFromJSON(JSONNode json) {
			JSONNode v = null;
			if (json["g"].Value != "Mesh" || (v = json["v"]) == null) {
				return null;
			}
			var mesh = new Mesh();

			var vertices = new List<Vector3>();
			var iter = v["points"].Values;
			while (iter.MoveNext()) {
				var x = iter.Current.AsFloat;
				iter.MoveNext();
				var y = iter.Current.AsFloat;
				iter.MoveNext();
				var z = iter.Current.AsFloat;
				vertices.Add(new Vector3(x, y, z));
			}
			mesh.vertices = vertices.ToArray();

			var triangles = new List<int>();
			foreach (var i in v["indices"].Values) {
				triangles.Add(i.AsInt);
			}
			mesh.triangles = triangles.ToArray();

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			return mesh;
		}
		#endregion
	}
}