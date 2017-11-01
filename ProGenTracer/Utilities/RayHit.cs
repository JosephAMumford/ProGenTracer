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
        /// <summary>
        /// Has ray hit any geometry
        /// </summary>
        public bool isHit = false;
        /// <summary>
        /// Point in World Space where ray hit
        /// </summary>
        public Vector3 hitPoint = new Vector3();
        /// <summary>
        /// Normal of hit point
        /// </summary>
        public Vector3 normal = new Vector3();
        /// <summary>
        /// Used in face lighting - DELETE
        /// </summary>
        public double ratio = 0;
        /// <summary>
        /// UV Coordinate of hit point
        /// </summary>
        public Vector2 uv = new Vector2();
        /// <summary>
        /// Distance of hit point from ray origin
        /// </summary>
        public double distance = 0;
        /// <summary>
        /// Color of hit point
        /// </summary>
        public Utilities.Color hitColor = new Utilities.Color();
        /// <summary>
        /// Id of scene object that ray hit
        /// </summary>
        public int HitObjectID;
        /// <summary>
        /// Distance value
        /// </summary>
        public double near;
        /// <summary>
        /// Triangle index of hit point
        /// </summary>
        public int index;

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
            uv.x = (1 + Math.Atan2(normal.z, normal.x) / Math.PI) * 0.5;
            uv.y = Math.Acos(normal.y) / Math.PI;
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
