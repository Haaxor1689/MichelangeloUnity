using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Michelangelo.Utility {
    public static class Extensions {
        public static void Then(this UnityWebRequest request, Action<UnityWebRequest> completion) {
            request.SendWebRequest().completed += operation => {
                try {
                    completion.Invoke((operation as UnityWebRequestAsyncOperation).webRequest);
                } finally {
                    request.Dispose();
                }
            };
        }

        public static UnityWebRequest NoRedirect(this UnityWebRequest request) {
            request.redirectLimit = 0;
            return request;
        }

        public static UnityWebRequest WithCookies(this UnityWebRequest request, string cookies) {
            request.SetRequestHeader("Cookie", cookies);
            return request;
        }

        public static string GetResponseBody(this UnityWebRequest request) {
            return request.downloadHandler.text;
        }

        public static string String(this UnityWebRequest request) {
            var builder = new System.Text.StringBuilder();
            builder.Append("Request URL: ");
            builder.Append(request.url);
            builder.Append("\nRequest Method: ");
            builder.Append(request.method);
            builder.Append("\nStatus Code: ");
            builder.Append(request.responseCode);
            builder.Append("\nResponse Headers\n---------------");
            foreach (var pair in request.GetResponseHeaders()) {
                builder.Append("\n");
                builder.Append(pair.Key);
                builder.Append(": ");
                builder.Append(pair.Value);
            }
            return builder.ToString();
        }
    }
}