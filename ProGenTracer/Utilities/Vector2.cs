//Created by Joseph Mumford 10/22/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class Vector2
    {
        //Public Variables
        /// <summary>
        /// x component of vector
        /// </summary>
        public double x;
        /// <summary>
        /// y component of vector
        /// </summary>
        public double y;
        /// <summary>
        /// Length of Vector
        /// </summary>
        public double magnitude;
        /// <summary>
        /// Vector with a length of 1
        /// </summary>
        public Vector2 normalized;

        //Static Variables
        /// <summary>
        /// Shorthand for Vector2(0,1)
        /// </summary>
        public static Vector2 up = new Vector2(0, 1);
        /// <summary>
        /// Shorthand for Vector2(0,-1)
        /// </summary>
        public static Vector2 down = new Vector2(0, -1);
        /// <summary>
        /// Shorthand for Vector2(1,1)
        /// </summary>
        public static Vector2 one = new Vector2(1, 1);
        /// <summary>
        /// Shorthand for Vector2(0,0)
        /// </summary>
        public static Vector2 zero = new Vector2(0, 0);
        /// <summary>
        /// Shorthand for Vector2(1,0)
        /// </summary>
        public static Vector2 right = new Vector2(1, 0);
        /// <summary>
        /// Shorthand for Vector2(-1,0)
        /// </summary>
        public static Vector2 left = new Vector2(-1, 0);

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Vector2()
        {
        }
        /// <summary>
        /// Set constructor with components x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="a"></param>
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
        /// <summary>
        /// Multiply Vector a by n
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Vector2 operator *(Vector2 a, double n)
        {
            return new Vector2(a.x * n , a.y * n);
        }
        /// <summary>
        /// Returns true if Vector a is equal to Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }
        /// <summary>
        /// Returns true if Vector a does not equal Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return (a.x != b.x) || (a.y != b.y);
        }
    }
}
