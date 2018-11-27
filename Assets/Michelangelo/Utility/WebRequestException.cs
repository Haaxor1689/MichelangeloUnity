using System;

namespace Michelangelo.Utility {
    public class WebRequestException : Exception {
        public readonly long ResponseCode;

        public WebRequestException(string message, long responseCode) : base(message + MessageFromResponseCode(responseCode)) {
            ResponseCode = responseCode;
        }

        private static string MessageFromResponseCode(long responseCode) {
            switch (responseCode) {
            case 400: return "400 Bad Request";
            case 401: return "401 Unauthorized";
            case 402: return "402 Payment Required";
            case 403: return "403 Forbidden";
            case 404: return "404 Not Found";
            case 405: return "405 Method Not Allowed";
            case 406: return "406 Not Acceptable";
            case 407: return "407 Proxy Authentication Required";
            case 408: return "408 Request Timeout";
            case 409: return "409 Conflict";
            case 500: return "500 Internal Server Error";
            case 501: return "501 Not Implemented";
            case 502: return "502 Bad Gateway";
            case 503: return "503 Service Unavailable";
            case 520: return "520 Unknown Error";
            case 521: return "521 Web Server Is Down";
            case 522: return "522 Connection Timed Out";
            case 523: return "523 Origin Is Unreachable";
            case 524: return "524 A Timeout Occurred";
            case 525: return "525 SSL Handshake Failed";
            case 526: return "526 Invalid SSL Certificate";
            case 527: return "527 Railgun Error";
            default: return responseCode.ToString();
            }
        }
    }
}
