using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Michelangelo.Utility {
    public static class Extensions {
        public static void Then(this UnityWebRequest request, Action<UnityWebRequest> completion) {
            request.SendWebRequest().completed += operation => {
                completion.Invoke((operation as UnityWebRequestAsyncOperation).webRequest);
                request.Dispose();
            };
        }

        public static UnityWebRequest WithCookies(this UnityWebRequest request, string cookies) {
            request.SetRequestHeader("Cookie", cookies);
            return request;
        }

        public static string GetResponseBody(this UnityWebRequest request) {
            return request.downloadHandler.text;
        }
    }
}