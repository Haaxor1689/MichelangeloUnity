﻿using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [MessagePackObject(true)]
    public class ParseTreeModel {
        private const uint MeshVertexCountLimit = 65534u;

        public uint[] ChildIndices;

        /// <summary>
        ///   My id within the tree
        /// </summary>
        public uint ID = uint.MaxValue;

        public string[][] Ontology;

        //Id of the rule which created this shape
        //public uint R = uint.MaxValue;
        public string Rule = string.Empty;

        //Human readable nme of the rule
        //public string RN = string.Empty;

        public GeometricModel[] Shape;
    }
}