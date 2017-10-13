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
