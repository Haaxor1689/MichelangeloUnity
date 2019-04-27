using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace Michelangelo.Utility {
    public static class Extensions {
        public static UnityWebRequest NoRedirect(this UnityWebRequest request) {
            request.redirectLimit = 0;
            return request;
        }

        public static UnityWebRequest WithCookies(this UnityWebRequest request, string cookies) {
            request.SetRequestHeader("Cookie", cookies);
            return request;
        }

        public static string GetResponseBody(this UnityWebRequest request) => request.downloadHandler?.text;

        public static string Info(this UnityWebRequest request) {
            var builder = new StringBuilder();
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
            if (request.GetResponseHeaders() != null) {
                builder.Append("\nResponse Headers\n---------------");
                foreach (var pair in request.GetResponseHeaders()) {
                    builder.Append("\n");
                    builder.Append(pair.Key);
                    builder.Append(": ");
                    builder.Append(pair.Value);
                }
                builder.Append("\n---------------");
            }
            if (request.GetResponseBody() != null) {
                builder.Append("\nResponse body:");
                builder.Append(request.GetResponseBody());
            }
            builder.Append("\n---------------");
            return builder.ToString();
        }

        public static bool HasNegativeScale(this Matrix4x4 mat) {
            var x0 = new Vector3(Matrix4x4.identity.GetColumn(0).x, Matrix4x4.identity.GetColumn(0).y, Matrix4x4.identity.GetColumn(0).z);
            var y0 = new Vector3(Matrix4x4.identity.GetColumn(1).x, Matrix4x4.identity.GetColumn(1).y, Matrix4x4.identity.GetColumn(1).z);
            var z0 = new Vector3(Matrix4x4.identity.GetColumn(2).x, Matrix4x4.identity.GetColumn(2).y, Matrix4x4.identity.GetColumn(2).z);

            var sz0 = Vector3.Dot(Vector3.Cross(x0, y0), z0);
            var sy0 = Vector3.Dot(Vector3.Cross(z0, x0), y0);
            var sx0 = Vector3.Dot(Vector3.Cross(y0, z0), x0);

            var x1 = new Vector3(mat.GetColumn(0).x, mat.GetColumn(0).y, mat.GetColumn(0).z);
            var y1 = new Vector3(mat.GetColumn(1).x, mat.GetColumn(1).y, mat.GetColumn(1).z);
            var z1 = new Vector3(mat.GetColumn(2).x, mat.GetColumn(2).y, mat.GetColumn(2).z);

            var sz1 = Vector3.Dot(Vector3.Cross(x1, y1), z1);
            var sy1 = Vector3.Dot(Vector3.Cross(z1, x1), y1);
            var sx1 = Vector3.Dot(Vector3.Cross(y1, z1), x1);

            return sx0 * sx1 < 0 || sy0 * sy1 < 0 || sz0 * sz1 < 0;
        }

        public static string ClassNameFriendly(this string name) {
            var className = name.Replace(" ", "");
            if (Regex.IsMatch(className, "^[^\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}]")) {
                className = "_" + className;
            }
            return Regex.Replace(className, "[^\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]", "_");
        }

        public static Quaternion ExtractRotation(this Matrix4x4 matrix) {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        public static Vector3 ExtractPosition(this Matrix4x4 matrix) {
            Vector3 position;
            position.x = matrix.m03;
            position.y = matrix.m13;
            position.z = matrix.m23;
            return position;
        }

        public static Vector3 ExtractScale(this Matrix4x4 matrix) {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
            if (matrix.HasNegativeScale()) {
                scale.x *= -1;
            }
            return scale;
        }

        public static T[] RemoveAt<T>(this T[] source, int index) {
            var dest = new T[source.Length - 1];
            if (index > 0) {
                Array.Copy(source, 0, dest, 0, index);
            }

            if (index < source.Length - 1) {
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            }

            return dest;
        }

        public static T[] Add<T>(this T[] source, T value) {
            var dest = new List<T>(source);
            dest.Add(value);
            return dest.ToArray();
        }

        public static Value GetValueOrDefault<Key, Value>(this IDictionary<Key, Value> dictionary, Key key, Value defaultValue) {
            Value value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
