using System;

namespace Michelangelo.Utility {
    public class ResponseParseException : Exception {
        public ResponseParseException(string message = null) : base(message) { }
        public ResponseParseException(string message, ResponseParseException old) : base(message, old) { }
    }
}
