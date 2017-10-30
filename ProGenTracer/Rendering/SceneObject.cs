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
        public string name = "";
        public bool active = true;
        public Vector3 position = new Vector3();
        public Vector3 rotation = new Vector3();
        public Mesh mesh;
        public Material material;

    }
}
