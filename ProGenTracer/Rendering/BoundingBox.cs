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

        public void ResizeBoundingBox()
        {
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();
            Mesh.Clear();

            Vector3 d = new Vector3(Scale.x * 0.5, Scale.y * 0.5, Scale.z * 0.5);

            //Front
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            //Back
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            //Top
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            //Bottom
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            //Left
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            //Right
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            Mesh.SetVertices(newVertices);
            Mesh.SetTriangles(newTriangles);
        }
    }
}
