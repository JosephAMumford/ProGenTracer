//Created by Joseph Mumford 10/22/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    class Vector2
    {
        //Public Variables
        public double x;
        public double y;
        public double magnitude;
        public Vector2 normalized;

        //Static Variables
        public static Vector2 up = new Vector2(0, 1);
        public static Vector2 down = new Vector2(0, -1);
        public static Vector2 one = new Vector2(1, 1);
        public static Vector2 zero = new Vector2(0, 0);
        public static Vector2 right = new Vector2(1, 0);
        public static Vector2 left = new Vector2(-1, 0);

        public Vector2()
        {
        }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Vector2 a)
        {
            this.x = a.x;
            this.y = a.y;
            this.magnitude = a.magnitude;
        }

        /// <summary>
        /// Add Vector a to Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Subtract Vector b from vector a 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Negate Vector a
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        public static Vector2 operator *(Vector2 a, double n)
        {
            return new Vector2(a.x * n , a.y * n);
        }
    }
}
