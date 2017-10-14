//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    class Scene
    {
        public SceneObject[] Objects;
        public Light[] Lights;
        public Camera Camera;

        public IEnumerable<ISect> Intersect(Ray r)
        {
            return from x in Objects
                   select x.Intersect(r);
        }
    }
}
