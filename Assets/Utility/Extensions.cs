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

        public static UnityEngine.Mesh Mesh(this Michelangelo.Model.Render.PrimitiveType type) {
            switch (type) {
                case Michelangelo.Model.Render.PrimitiveType.Box:
                    return Utility.Primitives.Cube;
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool HasNegativeScale(this Matrix4x4 mat) {
            var X0 = new Vector3(Matrix4x4.identity.GetColumn(0).x, Matrix4x4.identity.GetColumn(0).y, Matrix4x4.identity.GetColumn(0).z);
            var Y0 = new Vector3(Matrix4x4.identity.GetColumn(1).x, Matrix4x4.identity.GetColumn(1).y, Matrix4x4.identity.GetColumn(1).z);
            var Z0 = new Vector3(Matrix4x4.identity.GetColumn(2).x, Matrix4x4.identity.GetColumn(2).y, Matrix4x4.identity.GetColumn(2).z);

            var sz0 = Vector3.Dot(Vector3.Cross(X0, Y0), Z0);
            var sy0 = Vector3.Dot(Vector3.Cross(Z0, X0), Y0);
            var sx0 = Vector3.Dot(Vector3.Cross(Y0, Z0), X0);

            var X1 = new Vector3(mat.GetColumn(0).x, mat.GetColumn(0).y, mat.GetColumn(0).z);
            var Y1 = new Vector3(mat.GetColumn(1).x, mat.GetColumn(1).y, mat.GetColumn(1).z);
            var Z1 = new Vector3(mat.GetColumn(2).x, mat.GetColumn(2).y, mat.GetColumn(2).z);

            var sz1 = Vector3.Dot(Vector3.Cross(X1, Y1), Z1);
            var sy1 = Vector3.Dot(Vector3.Cross(Z1, X1), Y1);
            var sx1 = Vector3.Dot(Vector3.Cross(Y1, Z1), X1);

            return (sx0 * sx1 < 0) || (sy0 * sy1 < 0) || (sz0 * sz1 < 0);
        }
    }
}