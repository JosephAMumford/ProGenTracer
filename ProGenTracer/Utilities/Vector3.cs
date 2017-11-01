//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class Vector3
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
        /// z component of vector
        /// </summary>
        public double z;
        /// <summary>
        /// Length of vector
        /// </summary>
        public double magnitude;
        /// <summary>
        /// Vector with length of 1
        /// </summary>
        public Vector3 normalized;

        //Static Variables
        /// <summary>
        /// Shorthand for Vector3(0,0,-1)
        /// </summary>
        public static Vector3 back = new Vector3(0, 0, -1);
        /// <summary>
        /// Shorthand for Vector3(0,-1,0)
        /// </summary>
        public static Vector3 down = new Vector3(0, -1, 0);
        /// <summary>
        /// Shorthand for Vector3(0,0,1)
        /// </summary>
        public static Vector3 forward = new Vector3(0, 0, 1);
        /// <summary>
        /// Shorthand for Vector3(-1,0,0)
        /// </summary>
        public static Vector3 left = new Vector3(-1, 0, 0);
        /// <summary>
        /// Shorthand for Vector3(1,1,1)
        /// </summary>
        public static Vector3 one = new Vector3(1, 1, 1);
        /// <summary>
        /// Shorthand for Vector3(0,0,0)
        /// </summary>
        public static Vector3 zero = new Vector3(0, 0, 0);
        /// <summary>
        /// Shorthand for Vector3(1,0,0)
        /// </summary>
        public static Vector3 right = new Vector3(1, 0, 0);
        /// <summary>
        /// Shorthand for Vector3(0,1,0)
        /// </summary>
        public static Vector3 up = new Vector3(0, 1, 0);

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Vector3()
        {
        }
        /// <summary>
        /// Set Constructor with parameters x, y, and z
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            magnitude = Magnitude(this);
        }
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="v"></param>
        public Vector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            magnitude = v.magnitude;
        }

        //Public Functions
        /// <summary>
        /// Set Vector with x, y, and z parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Set(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Get a formatted string representation of Vector
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ")";
        }

        //Static Functions
        /// <summary>
        /// Get angle from Vector a to Vector B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Angle(Vector3 a, Vector3 b)
        {
            return (Dot(a, b)) / (a.magnitude * b.magnitude);
        }
        /// <summary>
        /// Get the Cross product of Vector a and Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3((a.y * b.z) - (a.z * b.y), (a.z * b.x) - (a.x * b.z), (a.x * b.y) - (a.y * b.x));
        }
        /// <summary>
        /// Get the distance from Vector a to Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Distance(Vector3 a, Vector3 b)
        {
            return Magnitude(a - b);
        }
        /// <summary>
        /// Reflect Vector3 a by Normal n
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Vector3 Reflect(Vector3 a, Vector3 n)
        {
            return a - n * 2 * Dot(a, n);
        }
        /// <summary>
        /// Get the dot product of Vector a and Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Dot(Vector3 a, Vector3 b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }
        /// <summary>
        /// Get the length of Vector a
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Magnitude(Vector3 a)
        {
            return Math.Sqrt(Dot(a, a));
        }
        /// <summary>
        /// Normalize the vector
        /// </summary>
        public static Vector3 Normalize(Vector3 a)
        {
            double mag = Magnitude(a);

            if (mag == 0)
            {
                mag = double.PositiveInfinity;
            }

            a.x = a.x / mag;
            a.y = a.y / mag;
            a.z = a.z / mag;

            return a;
        }


        //Operators
        /// <summary>
        /// Adds Vector a to Vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        /// <summary>
        /// Subtracts Vector b from Vector a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        /// <summary>
        /// Negate Vector a
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x,-a.y,-a.z);
        }
        /// <summary>
        /// Multiply Vector a by n
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 a, double n)
        {
            return new Vector3(a.x * n, a.y * n, a.z * n);
        }
        /// <summary>
        /// divide Vector a by n
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 a, double n)
        {
            return new Vector3(a.x / n, a.y / n, a.z / n);
        }
        /// <summary>
        /// Returns true if Vector a and Vector b are equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return (a.x == b.x) && (a.y == b.y) && (a.z == b.z);
        }
        /// <summary>
        /// Returns true if Vector a and Vector are not equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return (a.x != b.x) || (a.y != b.y) || (a.z != b.z);
        }
    }
}
