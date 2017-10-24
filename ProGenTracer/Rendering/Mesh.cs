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
    class Mesh
    {
        //Public Variables
        public string name;
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
        public void ComputeNormals()
        {

        }
        public void ComputeTangents()
        {

        }
        public void ComputeBounds()
        {

        }
    }
}
