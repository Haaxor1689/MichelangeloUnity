using UnityEngine;

namespace Michelangelo.Utility {
    internal static class Primitives {
        public static readonly Mesh Box = _box();
        public static readonly Mesh Sphere = _sphere();
        public static readonly Mesh Torus = _torus();
        public static readonly Mesh Cone = _cone();
        public static readonly Mesh Cylinder = _cone(0.5f);
        public static readonly Mesh Pyramid = _cone(0.0f, 4);
        public static readonly Mesh Plane = _plane();

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

            Vector3[] normales = {
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
            var _00 = new Vector2(0f, 0f);
            var _10 = new Vector2(1f, 0f);
            var _01 = new Vector2(0f, 1f);
            var _11 = new Vector2(1f, 1f);

            Vector2[] uvs = {
                // Bottom
                _11,
                _01,
                _00,
                _10,

                // Left
                _11,
                _01,
                _00,
                _10,

                // Front
                _11,
                _01,
                _00,
                _10,

                // Back
                _11,
                _01,
                _00,
                _10,

                // Right
                _11,
                _01,
                _00,
                _10,

                // Top
                _11,
                _01,
                _00,
                _10
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
            mesh.normals = normales;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }

        private static Mesh _sphere() {
            var mesh = new Mesh();

            var radius = 0.5f;
            // Longitude |||
            var nbLong = 24;
            // Latitude ---
            var nbLat = 16;

            #region Vertices
            var vertices = new Vector3[(nbLong + 1) * nbLat + 2];
            var _pi = Mathf.PI;

            vertices[0] = Vector3.up * radius;
            for (var lat = 0; lat < nbLat; lat++) {
                var a1 = _pi * (lat + 1) / (nbLat + 1);
                var sin1 = Mathf.Sin(a1);
                var cos1 = Mathf.Cos(a1);

                for (var lon = 0; lon <= nbLong; lon++) {
                    var a2 = _pi * 2f * (lon == nbLong ? 0 : lon) / nbLong;
                    var sin2 = Mathf.Sin(a2);
                    var cos2 = Mathf.Cos(a2);

                    vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
                }
            }
            vertices[vertices.Length - 1] = Vector3.up * -radius;
            #endregion

            #region Normales		
            var normales = new Vector3[vertices.Length];
            for (var n = 0; n < vertices.Length; n++) {
                normales[n] = vertices[n].normalized;
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
            mesh.normals = normales;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }

        private static Mesh _torus() {
            var mesh = new Mesh();

            var radius1 = 0.5f;
            var radius2 = .2f;
            var nbRadSeg = 24;
            var nbSides = 18;

            #region Vertices		
            var vertices = new Vector3[(nbRadSeg + 1) * (nbSides + 1)];
            var _2pi = Mathf.PI * 2f;
            for (var seg = 0; seg <= nbRadSeg; seg++) {
                var currSeg = seg == nbRadSeg ? 0 : seg;

                var t1 = (float) currSeg / nbRadSeg * _2pi;
                var r1 = new Vector3(Mathf.Cos(t1) * radius1, 0f, Mathf.Sin(t1) * radius1);

                for (var side = 0; side <= nbSides; side++) {
                    var currSide = side == nbSides ? 0 : side;

                    var t2 = (float) currSide / nbSides * _2pi;
                    var r2 = Quaternion.AngleAxis(-t1 * Mathf.Rad2Deg, Vector3.up) * new Vector3(Mathf.Sin(t2) * radius2, Mathf.Cos(t2) * radius2);

                    vertices[side + seg * (nbSides + 1)] = r1 + r2;
                }
            }
            #endregion

            #region Normales		
            var normales = new Vector3[vertices.Length];
            for (var seg = 0; seg <= nbRadSeg; seg++) {
                var currSeg = seg == nbRadSeg ? 0 : seg;

                var t1 = (float) currSeg / nbRadSeg * _2pi;
                var r1 = new Vector3(Mathf.Cos(t1) * radius1, 0f, Mathf.Sin(t1) * radius1);

                for (var side = 0; side <= nbSides; side++) {
                    normales[side + seg * (nbSides + 1)] = (vertices[side + seg * (nbSides + 1)] - r1).normalized;
                }
            }
            #endregion

            #region UVs
            var uvs = new Vector2[vertices.Length];
            for (var seg = 0; seg <= nbRadSeg; seg++) {
                for (var side = 0; side <= nbSides; side++) {
                    uvs[side + seg * (nbSides + 1)] = new Vector2((float) seg / nbRadSeg, (float) side / nbSides);
                }
            }
            #endregion

            #region Triangles
            var nbFaces = vertices.Length;
            var nbTriangles = nbFaces * 2;
            var nbIndexes = nbTriangles * 3;
            var triangles = new int[nbIndexes];

            var i = 0;
            for (var seg = 0; seg <= nbRadSeg; seg++) {
                for (var side = 0; side <= nbSides - 1; side++) {
                    var current = side + seg * (nbSides + 1);
                    var next = side + (seg < nbRadSeg ? (seg + 1) * (nbSides + 1) : 0);

                    if (i < triangles.Length - 6) {
                        triangles[i++] = current;
                        triangles[i++] = next;
                        triangles[i++] = next + 1;

                        triangles[i++] = current;
                        triangles[i++] = next + 1;
                        triangles[i++] = current + 1;
                    }
                }
            }
            #endregion

            mesh.vertices = vertices;
            mesh.normals = normales;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }

        private static Mesh _cone(float topRadius = 0.0f, int nbSides = 18) {
            var mesh = new Mesh();

            var height = 1f;
            var bottomRadius = 0.5f;
            var nbHeightSeg = 1; // Not implemented yet

            var nbVerticesCap = nbSides + 1;

            #region Vertices
            // bottom + top + sides
            var vertices = new Vector3[nbVerticesCap + nbVerticesCap + nbSides * nbHeightSeg * 2 + 2];
            var vert = 0;
            var _2pi = Mathf.PI * 2f;

            // Bottom cap
            vertices[vert++] = new Vector3(0f, 0f, 0f);
            while (vert <= nbSides) {
                var rad = (float) vert / nbSides * _2pi;
                vertices[vert] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0f, Mathf.Sin(rad) * bottomRadius);
                vert++;
            }

            // Top cap
            vertices[vert++] = new Vector3(0f, height, 0f);
            while (vert <= nbSides * 2 + 1) {
                var rad = (float) (vert - nbSides - 1) / nbSides * _2pi;
                vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
                vert++;
            }

            // Sides
            var v = 0;
            while (vert <= vertices.Length - 4) {
                var rad = (float) v / nbSides * _2pi;
                vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
                vertices[vert + 1] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0, Mathf.Sin(rad) * bottomRadius);
                vert += 2;
                v++;
            }
            vertices[vert] = vertices[nbSides * 2 + 2];
            vertices[vert + 1] = vertices[nbSides * 2 + 3];
            #endregion

            #region Normales
            // bottom + top + sides
            var normales = new Vector3[vertices.Length];
            vert = 0;

            // Bottom cap
            while (vert <= nbSides) {
                normales[vert++] = Vector3.down;
            }

            // Top cap
            while (vert <= nbSides * 2 + 1) {
                normales[vert++] = Vector3.up;
            }

            // Sides
            v = 0;
            while (vert <= vertices.Length - 4) {
                var rad = (float) v / nbSides * _2pi;
                var cos = Mathf.Cos(rad);
                var sin = Mathf.Sin(rad);

                normales[vert] = new Vector3(cos, 0f, sin);
                normales[vert + 1] = normales[vert];

                vert += 2;
                v++;
            }
            normales[vert] = normales[nbSides * 2 + 2];
            normales[vert + 1] = normales[nbSides * 2 + 3];
            #endregion

            #region UVs
            var uvs = new Vector2[vertices.Length];

            // Bottom cap
            var u = 0;
            uvs[u++] = new Vector2(0.5f, 0.5f);
            while (u <= nbSides) {
                var rad = (float) u / nbSides * _2pi;
                uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                u++;
            }

            // Top cap
            uvs[u++] = new Vector2(0.5f, 0.5f);
            while (u <= nbSides * 2 + 1) {
                var rad = (float) u / nbSides * _2pi;
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
            mesh.normals = normales;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }

        private static Mesh _plane() {
            var mesh = new Mesh();

            var length = 1f;
            var width = 1f;
            var resX = 2; // 2 minimum
            var resZ = 2;

            #region Vertices		
            var vertices = new Vector3[resX * resZ];
            for (var z = 0; z < resZ; z++) {
                // [ -length / 2, length / 2 ]
                var zPos = ((float) z / (resZ - 1) - .5f) * length;
                for (var x = 0; x < resX; x++) {
                    // [ -width / 2, width / 2 ]
                    var xPos = ((float) x / (resX - 1) - .5f) * width;
                    vertices[x + z * resX] = new Vector3(xPos, 0f, zPos);
                }
            }
            #endregion

            #region Normales
            var normales = new Vector3[vertices.Length];
            for (var n = 0; n < normales.Length; n++) {
                normales[n] = Vector3.up;
            }
            #endregion

            #region UVs		
            var uvs = new Vector2[vertices.Length];
            for (var v = 0; v < resZ; v++) {
                for (var u = 0; u < resX; u++) {
                    uvs[u + v * resX] = new Vector2((float) u / (resX - 1), (float) v / (resZ - 1));
                }
            }
            #endregion

            #region Triangles
            var nbFaces = (resX - 1) * (resZ - 1);
            var triangles = new int[nbFaces * 6];
            var t = 0;
            for (var face = 0; face < nbFaces; face++) {
                // Retrieve lower left corner from face ind
                var i = face % (resX - 1) + face / (resZ - 1) * resX;

                triangles[t++] = i + resX;
                triangles[t++] = i + 1;
                triangles[t++] = i;

                triangles[t++] = i + resX;
                triangles[t++] = i + resX + 1;
                triangles[t++] = i + 1;
            }
            #endregion

            mesh.vertices = vertices;
            mesh.normals = normales;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            return mesh;
        }
    }
}
