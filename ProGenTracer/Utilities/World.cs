//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class World
    {
        //Change the following to scene objects
        public Vector3[] position;
        public Vector3[] size;
        public Utilities.Color[] colors;
        public int NumberOfObjects;

        public Light[] lights;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public World()
        {

        }

        public World(int num)
        {
            position = new Vector3[num];
            size = new Vector3[num];
            colors = new Utilities.Color[num];
            NumberOfObjects = num;
        }

        public Utilities.Color lineIntersection(Ray ray)
        {
            Utilities.Color newColor = new Utilities.Color();

            return newColor;
        }

        public RayHit inter(Ray ray, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            RayHit hit = new RayHit();
            double kEpsilon = 0.00001;

            Vector3 v0v1 = v1 - v0;
            Vector3 v0v2 = v2 - v0;
            Vector3 pvec = Vector3.Cross(ray.Direction, v0v2);
            double det = Vector3.Dot(v0v1, pvec);

            if(det < kEpsilon)
            {
                hit.isHit = false;
                return hit; 
            }

            if (Math.Abs(det) < kEpsilon)
            {
                hit.isHit = false;
                return hit;
            }

            double invDet = 1 / det;
            Vector3 tvec = ray.Origin - v0;

            double u1 = Vector3.Dot(tvec, pvec) * invDet;

            if (u1 < 0 || u1 > 1)
            {
                hit.isHit = false;
                return hit;
            }

            Vector3 qvec = Vector3.Cross(tvec, v0v1);
            double v = Vector3.Dot(ray.Direction, qvec) * invDet;
            if(v < 0 || u1 + v > 1)
            {
                hit.isHit = false;
                return hit;
            }

            hit.distance = Vector3.Dot(v0v2, qvec) * invDet;

            hit.isHit = true;
            return hit;
        }

        public RayHit intersectTri(Ray ray, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            RayHit hit = new RayHit();
            double kEpsilon = 0.00001;
            //Compute Normal
            Vector3 v0v1 = v1 - v0;
            Vector3 v0v2 = v2 - v0;
            Vector3 n = Vector3.Cross(v0v1, v0v2);
            double denom = Vector3.Dot(n, n);

            double NdotRayDirection = Vector3.Dot(n, ray.Direction);

            if(Math.Abs(NdotRayDirection) < kEpsilon)
            {
                hit.isHit = false;
                return hit;
            }

            double d = Vector3.Dot(n, v0);

            ray.Distance = (Vector3.Dot(n, ray.Origin) + d) / NdotRayDirection;

            if(ray.Distance < 0)
            {
                hit.isHit = false;
                return hit;
            }

            Vector3 P = ray.Origin + (ray.Direction * ray.Distance);

            //Step 2 inside outside test
            Vector3 C = new Vector3();  //vector perpendicular to triangle plane

            //edge 0
            Vector3 edge0 = v1 - v0;
            Vector3 vp0 = P - v0;
            C = Vector3.Cross(edge0, vp0);
            if (Vector3.Dot(n, C) < 0)
            {
                hit.isHit = false;
                return hit;
            }

            //edge 1
            Vector3 edge1 = v2 - v1;
            Vector3 vp1 = P - v1;
            C = Vector3.Cross(edge1, vp1);
            if (Vector3.Dot(n, C) < 0)
            {
                hit.isHit = false;
                return hit;
            }

            //edge 2
            Vector3 edge2 = v0 - v2;
            Vector3 vp2 = P - v2;
            C = Vector3.Cross(n, vp2);
            if (Vector3.Dot(n, C) < 0)
            {
                hit.isHit = false;
                return hit;
            }

            hit.isHit = true;
            hit.uv.x /= denom;
            hit.uv.y /= denom;
            hit.distance = ray.Distance;

            return hit;
        }

        public Utilities.Color intersectTriangle(Vector3 orig, Vector3 dir, double t)
        {
            Vector3 v0 = new Vector3(-1,-1,5);
            Vector3 v1 = new Vector3(-1,-1,5);
            Vector3 v2 = new Vector3(0,1,5);
            Color pixelColor = new Color();
            double kEpsilon = 0.00001;
            //Compute Normal
            Vector3 v0v1 = v1 - v0;
            Vector3 v0v2 = v2 - v0;
            Vector3 n = Vector3.Cross(v0v1, v0v2);

            double area2 = n.Magnitude();


            bool result = true;

            //Find P
            //Check if ray and plane are parallel
            double NdotRayDirection = Vector3.Dot(n, dir);
            //if (Math.Abs(NdotRayDirection) < kEpsilon)      //almost zero
            //    return false;   //parallel

            if (Math.Abs(NdotRayDirection) > kEpsilon)      //almost zero
            {
                //compute d parameter using equation 2
                double d = Vector3.Dot(n, v0);

                //compute t equation 3
                t = (Vector3.Dot(n, orig) + d) / NdotRayDirection;
                //check if the triangle is in behind ray
                //if (t < 0) return false;
                if (t > 0)
                {
                    //Compute intersection point using equation 1
                    Vector3 P = orig + dir * t;

                    //Step 2 inside outside test
                    Vector3 C = new Vector3();  //vector perpendicular to triangle plane

                    //edge 0
                    Vector3 edge0 = v1 - v0;
                    Vector3 vp0 = P - v0;
                    C = Vector3.Cross(edge0, vp0);
                    //if (Vector3.Dot(n, C) < 0) return false; //P is on the right side
                    if (Vector3.Dot(n, C) < 0) result = false;

                    //edge 1
                    Vector3 edge1 = v2 - v1;
                    Vector3 vp1 = P - v1;
                    C = Vector3.Cross(edge1, vp1);
                    //if (Vector3.Dot(n, C) < 0) return false;
                    if (Vector3.Dot(n, C) < 0) result = false;

                    //edge 2
                    Vector3 edge2 = v0 - v2;
                    Vector3 vp2 = P - v2;
                    C = Vector3.Cross(n, vp2);
                    //if (Vector3.Dot(n, C) < 0) return false;
                    if (Vector3.Dot(n, C) < 0) result = false;

                    //return true;  //this ray hits the triangle
                    if (result == true)
                    {
                        pixelColor = new Color(0.0, 1.0, 0.0);
                    }
                }


            }



            return pixelColor;
        }

        public Utilities.Color intersect(Vector3 orig, Vector3 dir, double t)
        {
            RayHit hit = new RayHit();
            Utilities.Color pixelColor = new Utilities.Color();

            int nearest = 0;
            double distance = double.MaxValue;

            for (int x = 0; x < NumberOfObjects; x++)
            {
                double t0 = 0; // solutions for t if the ray intersects 
                double t1 = 0;

                Vector3 L = orig - position[x];
                double a = Vector3.Dot(dir, dir);
                double b = 2 * Vector3.Dot(dir, L);
                double c = Vector3.Dot(L, L) - size[x].x;

                Result nr = solveQuadratic(a, b, c, t0, t1);
                if (nr.r == true)
                {
                    if (nr.a < 0)
                    {
                        if (nr.b < 0)
                        {
                            hit.isHit = false;
                        }
                        else
                        {
                            if (hit.hitPoint.z < distance)
                            {
                                hit.isHit = true;
                                t = nr.b;
                                hit.SetHitPoint(new Ray(orig, dir), t);
                                hit.SetNormal(position[x]);
                                hit.SetUV();
                                hit.SetRatio(dir);
                                nearest = x;
                                distance = hit.hitPoint.z;
                            }
                        }
                    }
                    else
                    {
                        if (hit.hitPoint.z < distance)
                        {
                            hit.isHit = true;
                            t = nr.a;
                            hit.SetHitPoint(new Ray(orig, dir), t);
                            hit.SetNormal(position[x]);
                            hit.SetUV();
                            hit.SetRatio(dir);
                            nearest = x;
                            distance = hit.hitPoint.z;
                        }
                    }
                }

                if (hit.isHit == true)
                {
                    pixelColor = colors[nearest];
                    pixelColor *= hit.ratio;
                }
                else
                {
                    pixelColor = Utilities.Color.Set(0.0,0.0,0.0);
                }
            }

            return pixelColor;

        }

        public Result solveQuadratic(double a, double b, double c, double x0, double x1)
        {
            Result newResult = new Result();
            double discr = b * b - 4 * a * c;
            if (discr < 0)
            {
                newResult.r = false;
            }
            else if (discr == 0) x0 = x1 = -0.5 * b / a;
            else
            {
                double q = (b > 0) ?
                    -0.5 * (b + Math.Sqrt(discr)) :
                    -0.5 * (b - Math.Sqrt(discr));
                x0 = q / a;
                x1 = c / q;
            }

            if (x0 > x1)
            {
                double i = x1;
                double j = x0;
                x0 = i;
                x1 = j;
                newResult.r = true;
            }

            newResult.a = x0;
            newResult.b = x1;

            return newResult;
        }

    }
}
