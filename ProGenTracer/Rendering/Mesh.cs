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
        public Vector3[] vertices;
        public Vector2[] uv;
        public int[] triangles;
        public Vector3[] normals;
        public Color[] colors;
       
        //public Vector4[] tangents;
        //bounding box

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Mesh()
        {

        }

        public void Clear()
        {
            vertices = null;
            uv = null;
            triangles = null;
            normals = null;
            colors = null;
        }

        public void SetVertices(List<Vector3> verticesList)
        {
            vertices = verticesList.ToArray();
        }

        public void SetUVs(List<Vector2> uvsList)
        {
            uv = uvsList.ToArray();
        }
        public void SetTriangles(List<int> trianglesList)
        {
            triangles = trianglesList.ToArray();
        }
        public void SetNormals(List<Vector3> normalsList)
        {
            normals = normalsList.ToArray();
        }
        public Vector3 GetNormal(int index)
        {
            Vector3 edge1 = Vector3.Normalize(vertices[triangles[index+1]] - vertices[triangles[index]]);
            Vector3 edge2 = Vector3.Normalize(vertices[triangles[index + 2]] - vertices[triangles[index + 1]]);
            return Vector3.Normalize(Vector3.Cross(edge1,edge2));
        }
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
        public void ComputeTangents()
        {

        }
        public void ComputeBounds()
        {

        }
    }
}
