//Created by Joseph Mumford 10/22/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Rendering
{
    public class Mesh
    {
        //Public Variables
        /// <summary>
        /// Vector3 array of all vertices in mesh
        /// </summary>
        public Vector3[] vertices;
        /// <summary>
        /// Vector2 array of all uv coordinates in mesh.  
        /// Must equal veertices count
        /// </summary>
        public Vector2[] uv;
        /// <summary>
        /// Int array of all triangle vertex references
        /// </summary>
        public int[] triangles;
        /// <summary>
        /// Vector3 array of all vertex normals
        /// </summary>
        public Vector3[] normals;
        /// <summary>
        /// Color array of all vertex colors
        /// </summary>
        public Color[] colors;   
        //public Vector4[] tangents;
        //bounding box

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Mesh()
        {

        }
        /// <summary>
        /// Clear all mesh information
        /// </summary>
        public void Clear()
        {
            vertices = null;
            uv = null;
            triangles = null;
            normals = null;
            colors = null;
        }
        /// <summary>
        /// Set vertices array from List<vector3>
        /// </summary>
        /// <param name="verticesList"></param>
        public void SetVertices(List<Vector3> verticesList)
        {
            vertices = verticesList.ToArray();
        }
        /// <summary>
        /// Set uvs array from List<Vector2>
        /// </summary>
        /// <param name="uvsList"></param>
        public void SetUVs(List<Vector2> uvsList)
        {
            uv = uvsList.ToArray();
        }
        /// <summary>
        /// Set triangles array from List<int>
        /// </summary>
        /// <param name="trianglesList"></param>
        public void SetTriangles(List<int> trianglesList)
        {
            triangles = trianglesList.ToArray();
        }
        /// <summary>
        /// Set normals array from List<Vector3>
        /// </summary>
        /// <param name="normalsList"></param>
        public void SetNormals(List<Vector3> normalsList)
        {
            normals = normalsList.ToArray();
        }
        /// <summary>
        /// Get face normal from triangle at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector3 GetNormal(int index)
        {
            Vector3 edge1 = Vector3.Normalize(vertices[triangles[index+1]] - vertices[triangles[index]]);
            Vector3 edge2 = Vector3.Normalize(vertices[triangles[index + 2]] - vertices[triangles[index + 1]]);
            return Vector3.Normalize(Vector3.Cross(edge1,edge2));
        }
        /// <summary>
        /// Compute vertex normals from list of triangle faces.  Copies face normal
        /// currently.  Work on better interpolated function
        /// </summary>
        public void ComputeNormals()
        {
            normals = new Vector3[vertices.Length];
            int numberOfTriangles = triangles.Length / 3;
            int index = 0;
            for (int i = 0; i < numberOfTriangles; i++)
            {
                Vector3 n = GetNormal(index);

                normals[triangles[index]] = n;
                normals[triangles[index + 1]] = n;
                normals[triangles[index + 2]] = n;

                index += 3;
            }
        }
        /// <summary>
        /// Compute tangents.  NOT IMPLEMNETED
        /// </summary>
        public void ComputeTangents()
        {

        }
        /// <summary>
        /// Compute bounding information.  NOT IMPLEMENTED
        /// </summary>
        public void ComputeBounds()
        {

        }
    }
}
