using System;
using Michelangelo.Model.Render;
using UnityEditor;
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

        public static string Info(this UnityWebRequest request) {
            var builder = new System.Text.StringBuilder();
            builder.Append("Request URL: ");
            builder.Append(request.url);
            builder.Append("\nRequest Method: ");
            builder.Append(request.method);
            builder.Append("\nStatus Code: ");
            builder.Append(request.responseCode);
            if (request.GetRequestHeader("Cookie") != null) {
                builder.Append("\nCookies\n---------------\n");
                builder.Append(request.GetRequestHeader("Cookie").Replace(' ', '\n'));
                builder.Append("---------------");
            }
            builder.Append("\nResponse Headers\n---------------");
            foreach (var pair in request.GetResponseHeaders()) {
                builder.Append("\n");
                builder.Append(pair.Key);
                builder.Append(": ");
                builder.Append(pair.Value);
            }
            builder.Append("\n---------------");
            builder.Append("\nResponse bode:");
            builder.Append(request.GetResponseBody());
            return builder.ToString();
        }

        public static UnityEngine.Mesh Mesh(this PrimitiveType type) {
            switch (type) {
                case PrimitiveType.Box:
                    return Utility.Primitives.Cube;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}