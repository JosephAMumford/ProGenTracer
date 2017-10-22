//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;
//using System.Windows;
using System.Windows.Forms;

namespace ProGenTracer
{

    public class Renderer
    {

        public World w = new World();

        public Utilities.Color TraceRay(Ray ray1)
        {
            return w.intersect(ray1.Origin, ray1.Direction, double.PositiveInfinity);
        }
    }

    public class Result
    {
        public bool r;
        public double a;
        public double b;
    }


    public class MainSystem
    {

    }

}
