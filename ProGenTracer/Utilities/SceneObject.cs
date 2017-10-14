//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    abstract class SceneObject
    {
        public Surface Surface;
        public abstract ISect Intersect(Ray ray);
        public abstract Vector3 Normal(Vector3 pos);
    }

    class Plane : SceneObject
    {
        public Vector3 normal;
        public double Offset;

        public override ISect Intersect(Ray ray)
        {
            double denom = Vector3.Dot(normal, ray.direction);
            if (denom > 0) return null;
            return new ISect()
            {
                Object = this,
                Ray = ray,
                Distance = (Vector3.Dot(normal, ray.start) + Offset) / (-denom)
            };
        }

        public override Vector3 Normal(Vector3 pos)
        {
            return normal;
        }
    }

    class Sphere : SceneObject
    {
        public Vector3 Center;
        public double Radius;

        public override ISect Intersect(Ray ray)
        {
            Vector3 eo = Center - ray.start;
            double v = Vector3.Dot(eo, ray.direction);
            double dist;
            if (v < 0)
            {
                dist = 0;
            }
            else
            {
                double disc = Math.Pow(Radius, 2) - (Vector3.Dot(eo, eo) - Math.Pow(v, 2));
                dist = disc < 0 ? 0 : v - Math.Sqrt(disc);
            }
            if (dist == 0) return null;
            return new ISect()
            {
                Object = this,
                Ray = ray,
                Distance = dist
            };
        }

        public override Vector3 Normal(Vector3 pos)
        {
            return Vector3.Normalize(pos - Center);
        }
    }
}
