using System;
using Michelangelo.Models.MichelangeloApi;
using UnityEngine;

namespace Michelangelo.Utility {
    [Serializable]
    internal class MeshGizmoData {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public GeometricModel Model;

        private Mesh mesh;
        public Mesh Mesh => mesh ? mesh : mesh = MeshUtilities.MeshFromGeometricModel(Model);

    }
}
