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
        /// <summary>
        /// Position of light in world space
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// Direction of light
        /// </summary>
        public Vector3 Direction;
        /// <summary>
        /// Light color
        /// </summary>
        public Color LightColor;
        /// <summary>
        /// Type of light
        /// 0 = Directional light
        /// 1 = Point light
        /// 2 = Spot light
        /// 3 = Area light
        /// </summary>
        public int Type;
        /// <summary>
        /// Radius of point light
        /// </summary>
        public double Radius;
        /// <summary>
        /// Intensity of light
        /// </summary>
        public double Intensity;

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
