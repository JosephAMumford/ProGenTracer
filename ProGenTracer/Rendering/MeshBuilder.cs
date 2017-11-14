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
    /// <summary>
    /// MeshBuilder generates procedural mesh objects
    /// </summary>
    public class MeshBuilder
    {

        //Generate Cube

        //Generate plane

        //Generate platonic solids

        public static Mesh GenerateSphere()
        {
            Mesh newMesh = new Mesh();
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();

            double SphereRadius = 0.5;

            double dTheta = (2 * Math.PI) / 16;
            double dPhi = (Math.PI) / 8;

            int index = 0;

            for (int i = 0; i < 16; i++)         //theta
            {
                for (int j = 0; j < 8; j++)      //phi
                {
                    double x;
                    double y;
                    double z;

                    x = SphereRadius * Math.Sin(i * dTheta) * Math.Cos(j * dPhi);
                    y = SphereRadius * Math.Cos(i * dTheta);
                    z = SphereRadius * Math.Sin(i * dTheta) * Math.Sin(j * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    x = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Cos(j * dPhi);
                    y = SphereRadius * Math.Cos((i + 1) * dTheta);
                    z = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Sin(j * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    x = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Cos((j + 1) * dPhi);
                    y = SphereRadius * Math.Cos((i + 1) * dTheta);
                    z = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Sin((j + 1) * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    x = SphereRadius * Math.Sin(i * dTheta) * Math.Cos((j + 1) * dPhi);
                    y = SphereRadius * Math.Cos(i * dTheta);
                    z = SphereRadius * Math.Sin(i * dTheta) * Math.Sin((j + 1) * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    index = newVertices.Count - 4;

                    newTriangles.Add(index);
                    newTriangles.Add(index + 1);
                    newTriangles.Add(index + 2);
                    newTriangles.Add(index);
                    newTriangles.Add(index + 2);
                    newTriangles.Add(index + 3);

                }
            }

            newMesh.SetVertices(newVertices);
            newMesh.SetTriangles(newTriangles);

            return newMesh;
        }
    }
}
