namespace Michelangelo.Models.Handlers {
    /// <summary>
    ///   Interface for handlers representing parts of Michelangelo's C# script syntax.
    /// </summary>
    public interface IHandler {
        /// <summary>
        ///   Serializes the handler into it's Michelangelo's C# grammar script representation.
        /// </summary>
        /// <returns>A string containing code representing this object.</returns>
        string ToCode();
    }
}
