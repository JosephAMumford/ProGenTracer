//Created by Joseph Mumford 10/29/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;
namespace ProGenTracer.Rendering
{
    public class SceneObject
    {
        /// <summary>
        /// Name of scene object
        /// </summary>
        public string Name = "";
        /// <summary>
        /// Is scene object active in scene.  Render
        /// if true
        /// </summary>
        public bool Active = true;
        /// <summary>
        /// Center position of scene object
        /// </summary>
        public Vector3 Position = new Vector3();
        /// <summary>
        /// Rotation of scene object
        /// </summary>
        public Vector3 Rotation = new Vector3();
        /// <summary>
        /// Mesh of scene object
        /// </summary>
        public Mesh Mesh = new Mesh();
        /// <summary>
        /// Material of scene object
        /// </summary>
        public Material Material = new Material();
        /// <summary>
        /// Boudning box of scene obeject
        /// </summary>
        public BoundingBox BBox = new BoundingBox();
    }
}
