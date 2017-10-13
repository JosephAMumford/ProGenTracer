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
