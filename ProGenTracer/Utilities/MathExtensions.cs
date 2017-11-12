using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class MathExtensions
    {
        /// <summary>
        /// Clamps input value beteween minimum value and maximum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double Clamp(double value, double minimum, double maximum)
        {
            return (value < minimum) ? minimum : (value > maximum) ? maximum : value;
        }

        /// <summary>
        /// Convert input degree to radian
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static double DegreesToRadians(double degree)
        {
            return degree * Math.PI / 180;
        }

        /// <summary>
        /// Convert input radian to degree
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static double RadiansToDegrees(double radian)
        {
            return radian * 180 / Math.PI;
        }
    }
}
