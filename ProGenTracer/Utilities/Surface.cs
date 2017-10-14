//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    class Surface
    {
        public Func<Vector3, Color> Diffuse;
        public Func<Vector3, Color> Specular;
        public Func<Vector3, double> Reflect;
        public double Roughness;
    }
}
