using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Michelangelo.Utility {
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal static class Primitives {
        public static readonly Mesh Box = _box();
        public static readonly Mesh Sphere = _sphere();
        public static readonly Mesh Cylinder = _cone(0.5f);
        public static readonly Mesh Cone = _cone();
        public static readonly Mesh Icosahedron = new Mesh(); // Not implemented
        public static readonly Mesh Empty = new Mesh();

        public static uint GetVertexCount(string primitive) {
            switch (primitive) {
                case "Box": return 3 * 4 * 6;
                case "Sphere": return 25 * 16 * 3 + 6;
                case "Cylinder":
                case "Cone": return 76 * 3;
                case "Empty": return 0;
                default:
                    Debug.LogError("Unknown primitive type " + primitive);
                    return 0;
            }
        }

        private static Mesh _box() {
            var mesh = new Mesh();

            const float length = 1f;
            const float width = 1f;
            const float height = 1f;

            #region Vertices
            var p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);
            var p1 = new Vector3(length * .5f, -width * .5f, height * .5f);
            var p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);
            var p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);

            var p4 = new Vector3(-length * .5f, width * .5f, height * .5f);
            var p5 = new Vector3(length * .5f, width * .5f, height * .5f);
            var p6 = new Vector3(length * .5f, width * .5f, -height * .5f);
            var p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);

            Vector3[] vertices = {
                // Bottom
                p0,
                p1,
                p2,
                p3,

                // Left
                p7,
                p4,
                p0,
                p3,

                // Front
                p4,
                p5,
                p1,
                p0,

                // Back
                p6,
                p7,
                p3,
                p2,

                // Right
                p5,
                p6,
                p2,
                p1,

                // Top
                p7,
                p6,
                p5,
                p4
            };
            #endregion

            #region Normales
            var up = Vector3.up;
            var down = Vector3.down;
            var front = Vector3.forward;
            var back = Vector3.back;
            var left = Vector3.left;
            var right = Vector3.right;

            Vector3[] normals = {
                // Bottom
                down,
                down,
                down,
                down,

                // Left
                left,
                left,
                left,
                left,

                // Front
                front,
                front,
                front,
                front,

                // Back
                back,
                back,
                back,
                back,

                // Right
                right,
                right,
                right,
                right,

                // Top
                up,
                up,
                up,
                up
            };
            #endregion

            #region UVs
            var v00 = new Vector2(0f, 0f);
            var v10 = new Vector2(1f, 0f);
            var v01 = new Vector2(0f, 1f);
            var v11 = new Vector2(1f, 1f);

            Vector2[] uvs = {
                // Bottom
                v11,
                v01,
                v00,
                v10,

                // Left
                v11,
                v01,
                v00,
                v10,

                // Front
                v11,
                v01,
                v00,
                v10,

                // Back
                v11,
                v01,
                v00,
                v10,

                // Right
                v11,
                v01,
                v00,
                v10,

                // Top
                v11,
                v01,
                v00,
                v10
            };
            #endregion

            #region Triangles
            int[] triangles = {
                // Bottom
                3,
                1,
                0,
                3,
                2,
                1,

                // Left
                3 + 4 * 1,
                1 + 4 * 1,
                0 + 4 * 1,
                3 + 4 * 1,
                2 + 4 * 1,
                1 + 4 * 1,

                // Front
                3 + 4 * 2,
                1 + 4 * 2,
                0 + 4 * 2,
                3 + 4 * 2,
                2 + 4 * 2,
                1 + 4 * 2,

                // Back
                3 + 4 * 3,
                1 + 4 * 3,
                0 + 4 * 3,
                3 + 4 * 3,
                2 + 4 * 3,
                1 + 4 * 3,

                // Right
                3 + 4 * 4,
                1 + 4 * 4,
                0 + 4 * 4,
                3 + 4 * 4,
                2 + 4 * 4,
                1 + 4 * 4,

                // Top
                3 + 4 * 5,
                1 + 4 * 5,
                0 + 4 * 5,
                3 + 4 * 5,
                2 + 4 * 5,
                1 + 4 * 5
            };
            #endregion

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }

        private static Mesh _sphere() {
            var mesh = new Mesh();

            const float radius = 0.5f;
            // Longitude |||
            const int nbLong = 24;
            // Latitude ---
            const int nbLat = 16;

            #region Vertices
            var vertices = new Vector3[(nbLong + 1) * nbLat + 2];

            vertices[0] = Vector3.up * radius;
            for (var lat = 0; lat < nbLat; lat++) {
                var a1 = Mathf.PI * (lat + 1) / (nbLat + 1);
                var sin1 = Mathf.Sin(a1);
                var cos1 = Mathf.Cos(a1);

                for (var lon = 0; lon <= nbLong; lon++) {
                    var a2 = Mathf.PI * 2f * (lon == nbLong ? 0 : lon) / nbLong;
                    var sin2 = Mathf.Sin(a2);
                    var cos2 = Mathf.Cos(a2);

                    vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
                }
            }
            vertices[vertices.Length - 1] = Vector3.up * -radius;
            #endregion

            #region Normales		
            var normals = new Vector3[vertices.Length];
            for (var n = 0; n < vertices.Length; n++) {
                normals[n] = vertices[n].normalized;
            }
            #endregion

            #region UVs
            var uvs = new Vector2[vertices.Length];
            uvs[0] = Vector2.up;
            uvs[uvs.Length - 1] = Vector2.zero;
            for (var lat = 0; lat < nbLat; lat++) {
                for (var lon = 0; lon <= nbLong; lon++) {
                    uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float) lon / nbLong, 1f - (float) (lat + 1) / (nbLat + 1));
                }
            }
            #endregion

            #region Triangles
            var nbFaces = vertices.Length;
            var nbTriangles = nbFaces * 2;
            var nbIndexes = nbTriangles * 3;
            var triangles = new int[nbIndexes];

            //Top Cap
            var i = 0;
            for (var lon = 0; lon < nbLong; lon++) {
                triangles[i++] = lon + 2;
                triangles[i++] = lon + 1;
                triangles[i++] = 0;
            }

            //Middle
            for (var lat = 0; lat < nbLat - 1; lat++) {
                for (var lon = 0; lon < nbLong; lon++) {
                    var current = lon + lat * (nbLong + 1) + 1;
                    var next = current + nbLong + 1;

                    triangles[i++] = current;
                    triangles[i++] = current + 1;
                    triangles[i++] = next + 1;

                    triangles[i++] = current;
                    triangles[i++] = next + 1;
                    triangles[i++] = next;
                }
            }

            //Bottom Cap
            for (var lon = 0; lon < nbLong; lon++) {
                triangles[i++] = vertices.Length - 1;
                triangles[i++] = vertices.Length - (lon + 2) - 1;
                triangles[i++] = vertices.Length - (lon + 1) - 1;
            }
            #endregion

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }

        private static Mesh _cone(float topRadius = 0.0f, int nbSides = 18) {
            var mesh = new Mesh();

            const float height = 1f;
            const float bottomRadius = 0.5f;
            const int nbHeightSeg = 1; // Not implemented yet

            var nbVerticesCap = nbSides + 1;

            #region Vertices
            // bottom + top + sides
            var vertices = new Vector3[nbVerticesCap + nbVerticesCap + nbSides * nbHeightSeg * 2 + 2];
            var vertex = 0;
            const float pi2 = Mathf.PI * 2f;

            // Bottom cap
            vertices[vertex++] = new Vector3(0f, 0f, 0f);
            while (vertex <= nbSides) {
                var rad = (float) vertex / nbSides * pi2;
                vertices[vertex] = new Vector3(Mathf.Cos(rad) * bottomRadius, -height / 2.0f, Mathf.Sin(rad) * bottomRadius);
                vertex++;
            }

            // Top cap
            vertices[vertex++] = new Vector3(0f, height / 2.0f, 0f);
            while (vertex <= nbSides * 2 + 1) {
                var rad = (float) (vertex - nbSides - 1) / nbSides * pi2;
                vertices[vertex] = new Vector3(Mathf.Cos(rad) * topRadius, height / 2.0f, Mathf.Sin(rad) * topRadius);
                vertex++;
            }

            // Sides
            var v = 0;
            while (vertex <= vertices.Length - 4) {
                var rad = (float) v / nbSides * pi2;
                vertices[vertex] = new Vector3(Mathf.Cos(rad) * topRadius, height / 2.0f, Mathf.Sin(rad) * topRadius);
                vertices[vertex + 1] = new Vector3(Mathf.Cos(rad) * bottomRadius, -height / 2.0f, Mathf.Sin(rad) * bottomRadius);
                vertex += 2;
                v++;
            }
            vertices[vertex] = vertices[nbSides * 2 + 2];
            vertices[vertex + 1] = vertices[nbSides * 2 + 3];
            #endregion

            #region Normales
            // bottom + top + sides
            var normals = new Vector3[vertices.Length];
            vertex = 0;

            // Bottom cap
            while (vertex <= nbSides) {
                normals[vertex++] = Vector3.down;
            }

            // Top cap
            while (vertex <= nbSides * 2 + 1) {
                normals[vertex++] = Vector3.up;
            }

            // Sides
            v = 0;
            while (vertex <= vertices.Length - 4) {
                var rad = (float) v / nbSides * pi2;
                var cos = Mathf.Cos(rad);
                var sin = Mathf.Sin(rad);

                normals[vertex] = new Vector3(cos, 0f, sin);
                normals[vertex + 1] = normals[vertex];

                vertex += 2;
                v++;
            }
            normals[vertex] = normals[nbSides * 2 + 2];
            normals[vertex + 1] = normals[nbSides * 2 + 3];
            #endregion

            #region UVs
            var uvs = new Vector2[vertices.Length];

            // Bottom cap
            var u = 0;
            uvs[u++] = new Vector2(0.5f, 0.5f);
            while (u <= nbSides) {
                var rad = (float) u / nbSides * pi2;
                uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                u++;
            }

            // Top cap
            uvs[u++] = new Vector2(0.5f, 0.5f);
            while (u <= nbSides * 2 + 1) {
                var rad = (float) u / nbSides * pi2;
                uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                u++;
            }

            // Sides
            var uSides = 0;
            while (u <= uvs.Length - 4) {
                var t = (float) uSides / nbSides;
                uvs[u] = new Vector3(t, 1f);
                uvs[u + 1] = new Vector3(t, 0f);
                u += 2;
                uSides++;
            }
            uvs[u] = new Vector2(1f, 1f);
            uvs[u + 1] = new Vector2(1f, 0f);
            #endregion

            #region Triangles
            var nbTriangles = nbSides + nbSides + nbSides * 2;
            var triangles = new int[nbTriangles * 3 + 3];

            // Bottom cap
            var tri = 0;
            var i = 0;
            while (tri < nbSides - 1) {
                triangles[i] = 0;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = tri + 2;
                tri++;
                i += 3;
            }
            triangles[i] = 0;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = 1;
            tri++;
            i += 3;

            // Top cap
            //tri++;
            while (tri < nbSides * 2) {
                triangles[i] = tri + 2;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = nbVerticesCap;
                tri++;
                i += 3;
            }

            triangles[i] = nbVerticesCap + 1;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = nbVerticesCap;
            tri++;
            i += 3;
            tri++;

            // Sides
            while (tri <= nbTriangles) {
                triangles[i] = tri + 2;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = tri + 0;
                tri++;
                i += 3;

                triangles[i] = tri + 1;
                triangles[i + 1] = tri + 2;
                triangles[i + 2] = tri + 0;
                tri++;
                i += 3;
            }
            #endregion

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }
    }
}
