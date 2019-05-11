using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Models;
using Michelangelo.Utility;
using UnityEngine;

namespace Michelangelo.Scripts {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class ParseTreeScript : MonoBehaviour {
        [SerializeField]
        private Mesh objectMesh;
        
        private MeshFilter MeshFilter => GetComponent<MeshFilter>();
        private MeshRenderer MeshRenderer => GetComponent<MeshRenderer>();

        private void CreateMesh(ParseTree parseTree, Models.ParseTreeNode node, IReadOnlyList<Material> grammarMaterials) {
            var submeshes = new List<Tuple<Mesh, int>>();
            foreach (var pair in node.GetLeafNodes(parseTree).Select(n => n.Shape)
                                     .GroupBy(p => p.MaterialID, p => p, (key, p) => new { Material = key, GeometricModel = p })) {
                var mesh = new Mesh();
                mesh.CombineMeshes(pair.GeometricModel.Select(model => new CombineInstance {
                    mesh = MeshUtilities.MeshFromGeometricModel(model),
                    transform = MeshUtilities.MatrixFromArray(model.Transform)
                }).ToArray());
                submeshes.Add(Tuple.Create(mesh, pair.Material));
            }
            objectMesh = new Mesh();
            var finalCombine = new List<CombineInstance>();
            var materials = new List<Material>();
            foreach (var m in submeshes) {
                finalCombine.Add(new CombineInstance { mesh = m.Item1, transform = Matrix4x4.identity });
                materials.Add(grammarMaterials[m.Item2]);
            }
            objectMesh.CombineMeshes(finalCombine.ToArray(), false);
            MeshFilter.sharedMesh = objectMesh;
            MeshRenderer.sharedMaterials = materials.ToArray();
            transform.localPosition = new Vector3(0, 0, 0);
        }

        public static GameObject Construct(Transform parent, ParseTree parseTree, Models.ParseTreeNode node, Material[] grammarMaterials) {
            var newObject = new GameObject(node.Name);
            newObject.transform.SetParent(parent);
            newObject.hideFlags = HideFlags.NotEditable;
            var nodeScript = newObject.AddComponent<ParseTreeScript>();
            nodeScript.MeshFilter.hideFlags = HideFlags.HideInInspector;
            nodeScript.MeshRenderer.hideFlags = HideFlags.HideInInspector;

            nodeScript.CreateMesh(parseTree, node, grammarMaterials);
            
            return newObject;
        }
    }
}