//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Rendering;

namespace ProGenTracer.Utilities
{
    public class Scene
    {
        /// <summary>
        /// List of all scene objects in scene
        /// </summary>
        public List<SceneObject> SceneObjects = new List<SceneObject>();
        /// <summary>
        /// List of all lights in scene
        /// </summary>
        public List<Light> Lights = new List<Light>();
        /// <summary>
        /// Scene camera
        /// </summary>
        public Camera MainCamera;
    }
}
