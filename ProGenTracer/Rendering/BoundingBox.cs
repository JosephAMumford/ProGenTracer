//Created by Joseph Mumford 10/29/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Rendering
{
    public class BoundingBox
    {
        //Public Variables
        /// <summary>
        /// Scale factor of bounding box
        /// </summary>
        public Vector3 Scale = new Vector3();
        public Mesh Mesh = new Mesh();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BoundingBox()
        {

        }
        /// <summary>
        /// Create bounding box with size from Vector a
        /// 
        /// </summary>
        /// <param name="a"></param>
        public BoundingBox(Vector3 a)
        {

        }
        /// <summary>
        /// Update bounding box based on scale
        /// </summary>
        public void ResizeBoundingBox()
        {
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();
            Mesh.Clear();

            Vector3 d = new Vector3(Scale.x * 0.5, Scale.y * 0.5, Scale.z * 0.5);

            int index = 0;

            //Front
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);

            //Back
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);

            //Top
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);

            //Bottom
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);

            //Left       
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);

            //Right
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);

            Mesh.SetVertices(newVertices);
            Mesh.SetTriangles(newTriangles);
        }
    }
}
