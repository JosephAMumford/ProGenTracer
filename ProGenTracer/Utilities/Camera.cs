using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Utilities
{
    class Camera
    {
        public Vector3 position;
        public Vector3 forward;
        public Vector3 up;
        public Vector3 right;

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
