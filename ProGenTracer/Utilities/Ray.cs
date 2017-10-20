//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class Ray
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
