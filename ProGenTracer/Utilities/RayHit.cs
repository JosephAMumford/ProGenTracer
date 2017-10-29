//Created by Joseph Mumford 10/21/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class RayHit
    {
        //Public Variables
        public bool isHit = false;
        public Vector3 hitPoint = new Vector3();
        public Vector3 normal = new Vector3();
        public double ratio = 0;
        public double u = 0;
        public double v = 0;
        public double distance = 0;
        public Utilities.Color hitColor = new Utilities.Color();

        //Public Functions
        /// <summary>
        /// Compute collision with object and ray with magnitude t
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="t"></param>
        public void SetHitPoint(Ray ray, double t)
        {
            hitPoint = ray.Origin + ray.Direction * t;
        }

        /// <summary>
        /// Calculate with hit point
        /// </summary>
        /// <param name="point"></param>
        public void SetNormal(Vector3 point)
        {
            normal = Vector3.Normalize(hitPoint - point);
        }

        /// <summary>
        /// Calculate texture UV based on hit point, sphere only so remove later
        /// </summary>
        public void SetUV()
        {
            u = (1 + Math.Atan2(normal.z, normal.x) / Math.PI) * 0.5;
            v = Math.Acos(normal.y) / Math.PI;
        }

        /// <summary>
        /// Used for face lighting, remove at some point
        /// </summary>
        /// <param name="dir"></param>
        public void SetRatio(Vector3 dir)
        {
            ratio = Math.Max(0.0, Vector3.Dot(normal, -dir));
        }
    }
}
