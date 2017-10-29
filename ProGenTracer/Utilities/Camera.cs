//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Utilities
{
    public class Camera
    {
        //Public variables
        /// <summary>
        /// Position of camera
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// Forward vector of camera
        /// </summary>
        public Vector3 forward;
        /// <summary>
        /// Up vector of camera
        /// </summary>
        public Vector3 up;
        /// <summary>
        /// Right vector of camera
        /// </summary>
        public Vector3 right;

        //Public Functions
        /// <summary>
        /// Create camera at position pos pointing at position lookat
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="lookAt"></param>
        /// <returns></returns>
        public static Camera Create(Vector3 pos, Vector3 lookAt)
        {

            Vector3 newForward = Vector3.Normalize(lookAt - pos);
            Vector3 newDown = Vector3.down;
            Vector3 newRight = Vector3.Normalize(Vector3.Cross(newForward, newDown)) * 1.5;
            Vector3 newUp = Vector3.Normalize(Vector3.Cross(newForward, newRight)) * 1.5;
            
            return new Camera() { position = pos, forward = newForward, up = newUp, right = newRight };
        }
    }
}
