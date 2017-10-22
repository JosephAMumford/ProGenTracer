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
        public Vector3[] position = new Vector3[3];
        public Vector3[] size = new Vector3[3];
        public Utilities.Color[] colors = new Utilities.Color[3];

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public World()
        {

        }

        public Utilities.Color intersect(Vector3 orig, Vector3 dir, double t)
        {
            RayHit hit = new RayHit();
            Utilities.Color pixelColor = new Utilities.Color();

            int nearest = 0;
            double distance = double.MaxValue;

            for (int x = 0; x < 3; x++)
            {
                double t0 = 0; // solutions for t if the ray intersects 
                double t1 = 0;
                // analytic solution
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
                    pixelColor = Utilities.Color.Set(0.5, 0.5, 0.5);
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
