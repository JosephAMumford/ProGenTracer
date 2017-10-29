//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    public class Light
    {
        //Public Variables
        public Vector3 Position;
        public Vector3 Direction;
        public Color LightColor;
        public int Type;
        public double Radius;
        public Vector3 Intensity;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Light()
        {

        }

        /// <summary>
        /// Directional light contructor at position with direction and color
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="color"></param>
        public Light(Vector3 position, Vector3 direction, Color lightColor)
        {
            Type = 0;
            Position = position;
            Direction = direction;
            LightColor = lightColor;
        }

        /// <summary>
        /// Point light constructor at position with radius and color
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="lightColor"></param>
        public Light(Vector3 position, double radius, Color lightColor)
        {
            Type = 1;
            Position = position;
            Radius = radius;
            LightColor = lightColor;
        }
    }
}
