using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model.MichelangeloApi;
using Michelangelo.Utility;
using UnityEngine;

namespace Michelangelo.Scripts {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class ParseTreeNode : MonoBehaviour {
        [SerializeField]
        private Mesh objectMesh;
        
        private MeshFilter MeshFilter => GetComponent<MeshFilter>();
        private MeshRenderer MeshRenderer => GetComponent<MeshRenderer>();
        // private IObjectBase parentObject => transform.parent.GetComponent<IObjectBase>();

        private void CreateMesh(ParseTree parseTree, NormalizedParseTreeModel node, Material[] grammarMaterials) {
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
            // objectMesh.CombineMeshes(finalCombine.ToArray());
            MeshFilter.sharedMesh = objectMesh;
            MeshRenderer.sharedMaterials = materials.ToArray();
            // MeshRenderer.sharedMaterials = new []{ materials[0] };
            transform.localPosition = new Vector3(0, 0, 0);
        }

        private void OnDrawGizmosSelected() {
            // if (!parentObject.IsInEditMode) {
            //     if (Selection.activeGameObject == gameObject) {
            //         Selection.objects = new Object[] { parentObject.gameObject };
            //     }
            //     return;
            // }
            //
            // if (Selection.activeGameObject == parentObject.gameObject) {
            //     return;
            // }
            //
            // // Draw vertices and normals
            // for (var i = 0; i < MeshFilter.sharedMesh.vertices.Length; i++) {
            //     var sharedMeshVertex = MeshFilter.sharedMesh.vertices[i];
            //     sharedMeshVertex.Scale(transform.localScale);
            //     sharedMeshVertex = transform.localRotation * sharedMeshVertex;
            //     sharedMeshVertex += transform.position;
            //     
            //     Gizmos.color = Color.green;
            //     Gizmos.DrawLine(sharedMeshVertex, sharedMeshVertex + transform.localRotation * MeshFilter.sharedMesh.normals[i] * 0.2f);
            //     Gizmos.color = Color.red;
            //     Gizmos.DrawSphere(sharedMeshVertex, 0.02f);
            // }
        }

        public static GameObject Construct(Transform parent, ParseTree parseTree, NormalizedParseTreeModel node, Material[] grammarMaterials) {
            var newObject = new GameObject(node.Name);
            newObject.hideFlags = HideFlags.NotEditable;
            newObject.transform.SetParent(parent);
            var nodeScript = newObject.AddComponent<ParseTreeNode>();
            nodeScript.MeshFilter.hideFlags = HideFlags.HideInInspector;
            nodeScript.MeshRenderer.hideFlags = HideFlags.HideInInspector;

            nodeScript.CreateMesh(parseTree, node, grammarMaterials);
            
            return newObject;
        }
    }
}