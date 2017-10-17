//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    class Color
    {
        //Public variables
        /// <summary>
        /// Red component of color
        /// </summary>
        public double R;
        /// <summary>
        /// Green component of color
        /// </summary>
        public double G;
        /// <summary>
        /// Blue component of color
        /// </summary>
        public double B;
        /// <summary>
        /// Set color to standard background color
        /// </summary>
        public static Color Background = Set(0, 0, 0);
        /// <summary>
        /// Set color to standard default color
        /// </summary>
        public static Color DefaultColor = Set(0, 0, 0);

        //Constructors
        /// <summary>
        /// Create color with empty components
        /// </summary>
        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
        }
        /// <summary>
        /// Create color and set with components r,g, and b
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        //Public Functions
        /// <summary>
        /// Set color with components r, g, and b
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color Set(double r, double g, double b)
        {
            return new Color(r, g, b);
        }
        /// <summary>
        /// Check to ensure color component d is less than or equal to 1
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public double Check(double d)
        {
            return d > 1 ? 1 : d;
        }
        /// <summary>
        /// Convert color to System.Drawing.Color
        /// </summary>
        /// <returns></returns>
        internal System.Drawing.Color ToDrawingColor()
        {
            return System.Drawing.Color.FromArgb((int)(Check(R) * 255), (int)(Check(G) * 255), (int)(Check(B) * 255));
        }

        //Operators
        /// <summary>
        /// Multiply Color c by number n
        /// </summary>
        /// <param name="c"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Color operator *(Color c, double n)
        {
            return new Color(c.R * n, c.G * n, c.B * n);
        }
        /// <summary>
        /// Multiply Color c by Color d
        /// </summary>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Color operator *(Color c, Color d)
        {
            return new Color(c.R * d.R, c.G * d.G, c.B * d.B);
        }
        /// <summary>
        /// Add Color c to Color d
        /// </summary>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Color operator +(Color c, Color d)
        {
            return new Color(c.R + d.R, c.G + d.G, c.B + d.B);
        }
        /// <summary>
        /// Subtract Color d from Color c
        /// </summary>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Color operator -(Color c, Color d)
        {
            return new Color(c.R - d.R, c.G - d.G, c.B - d.B);
        }
    }
}
