using System;
using UnityEngine;

namespace Michelangelo.Models.Handlers {
    /// <summary>
    ///   Specifies the goal of generation.
    /// </summary>
    [Serializable]
    public class Model : IHandler {
        /// <summary>
        ///   When true, generated object will have it's pivot in the center of bottom side.
        /// </summary>
        public bool CenterOnPivot;

        /// <summary>
        ///   Case-insensitive string specifying the goal. Must be non-empty.
        /// </summary>
        public string Goal;

        /// <summary>
        ///   Restrictions for where the rules can be from.
        /// </summary>
        public RestrictSource RestrictSources;

        /// <summary>
        ///   Size of the bounding box of desired object
        /// </summary>
        public Vector3 Size = Vector3.one;

        /// <inheritdoc />
        public string ToCode() => $"new Model(\"{Goal}\").With(Size{Size.ToString()}, Position{(CenterOnPivot ? new Vector3(-Size.x / 2, 0, -Size.z / 2).ToString() : Vector3.zero.ToString())}){RestrictSources.ToCode()};";
    }
}
