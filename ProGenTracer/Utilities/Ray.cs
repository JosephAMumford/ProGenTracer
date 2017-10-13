using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    class Ray
    {
        public Vector3 start;
        public Vector3 direction;

        public Ray()
        {

        }
        public Ray(Vector3 str, Vector3 dir)
        {
            start = str;
            direction = dir;
        }
    }
}
