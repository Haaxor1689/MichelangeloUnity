using System;
using Michelangelo.Model.MichelangeloApi;
using Michelangelo.Utility;
using UnityEngine;

namespace Michelangelo.Model {
    [Serializable]
    public class MeshGizmoData {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public GeometricModel Model;

        private Mesh mesh;
        public Mesh Mesh => mesh ? mesh : (mesh = MeshUtilities.MeshFromGeometricModel(Model));

    }
}
