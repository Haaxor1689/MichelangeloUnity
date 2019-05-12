using System;
using System.Collections.Generic;

namespace Michelangelo.Utility {
    internal static partial class Extensions {
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

        public static T[] Add<T>(this IEnumerable<T> source, T value) {
            return new List<T>(source) { value }.ToArray();
        }
    }
}
