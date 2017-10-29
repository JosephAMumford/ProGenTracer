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
        //Public Variables
        public Vector3 Origin;
        public Vector3 Direction;
        public double Distance;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Ray()
        {

        }

        /// <summary>
        /// Create ray starting at origin with direction
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
