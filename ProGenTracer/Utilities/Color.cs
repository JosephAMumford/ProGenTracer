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
        public double R;
        public double G;
        public double B;

        public Color(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Color Set(double r, double g, double b)
        {
            return new Color(r, g, b);
        }

        public static Color operator *(Color c, double n)
        {
            return new Color(c.R, c.G, c.B);
        }

        public static Color operator *(Color c, Color d)
        {
            return new Color(c.R * d.R, c.G * d.G, c.B * d.B);
        }
        
        public static Color operator +(Color c, Color d)
        {
            return new Color(c.R + d.R, c.G + d.G, c.B + d.B);
        }
        
        public static Color operator -(Color c, Color d)
        {
            return new Color(c.R - d.R, c.G - d.G, c.B - d.B);
        }

        public static Color Background = Set(0, 0, 0);
        public static Color DefaultColor = Set(0, 0, 0);

        public double Check(double d)
        {
            return d > 1 ? 1 : d;
        }

        internal System.Drawing.Color ToDrawingColor()
        {
            return System.Drawing.Color.FromArgb((int)(Check(R) * 255), (int)(Check(G) * 255), (int)(Check(B) * 255));
        }
    }
}
