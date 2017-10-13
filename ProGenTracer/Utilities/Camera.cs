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

        public static Camera Create(Vector3 position, Vector3 lookAt)
        {

            Vector3 newForward = (lookAt - position);
            newForward.Normalize();
            Vector3 newDown = Vector3.down;
            Vector3 newRight = new Vector3().Cross(newForward, newDown);
            newRight.Normalize();
            newRight *= 1.5;
            Vector3 newUp = new Vector3().Cross(newForward, newRight);
            newUp.Normalize();
            newUp *= 1.5;
            
            return new Camera() { position = position, forward = newForward, up = newUp, right = newRight };
        }
    }
}
