using System;

namespace Michelangelo.Utility {
    public class ResponseParseException : Exception {
        public ResponseParseException() : base() { }
        public ResponseParseException(string message, ResponseParseException old) : base(message, old) { }
    }
}