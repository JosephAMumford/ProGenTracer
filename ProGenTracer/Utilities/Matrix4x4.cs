using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGenTracer.Utilities
{
    class Matrix4x4
    {
        public double[] m = new double[16];

        public Matrix4x4()
        {
        }

        public Matrix4x4(double[] t)
        {
            m[0] = t[0];
            m[1] = t[1];
            m[2] = t[2];
            m[3] = t[3];
            m[4] = t[4];
            m[5] = t[5];
            m[6] = t[6];
            m[7] = t[7];
            m[8] = t[8];
            m[9] = t[9];
            m[10] = t[10];
            m[11] = t[11];
            m[12] = t[12];
            m[13] = t[13];
            m[14] = t[14];
            m[15] = t[15];
        }

        public static Matrix4x4 zero = new Matrix4x4(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        public static Matrix4x4 identity = new Matrix4x4(new double[] {1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1});

        public Matrix4x4 Multiply(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 t = Matrix4x4.zero;

            t.m[0] = a.m[0] * b.m[0] + a.m[1] * b.m[4] + a.m[2] * b.m[8] + a.m[3] * b.m[12];
            t.m[1] = a.m[0] * b.m[1] + a.m[1] * b.m[5] + a.m[2] * b.m[9] + a.m[3] * b.m[13];
            t.m[2] = a.m[0] * b.m[2] + a.m[1] * b.m[6] + a.m[2] * b.m[10] + a.m[3] * b.m[14];
            t.m[3] = a.m[0] * b.m[3] + a.m[1] * b.m[7] + a.m[2] * b.m[11] + a.m[3] * b.m[15];

            t.m[4] = a.m[4] * b.m[0] + a.m[5] * b.m[4] + a.m[6] * b.m[8] + a.m[7] * b.m[12];
            t.m[5] = a.m[4] * b.m[1] + a.m[5] * b.m[5] + a.m[6] * b.m[9] + a.m[7] * b.m[13];
            t.m[6] = a.m[4] * b.m[2] + a.m[5] * b.m[6] + a.m[6] * b.m[10] + a.m[7] * b.m[14];
            t.m[7] = a.m[4] * b.m[3] + a.m[5] * b.m[7] + a.m[6] * b.m[11] + a.m[7] * b.m[15];

            t.m[8] = a.m[8] * b.m[0] + a.m[9] * b.m[4] + a.m[10] * b.m[8] + a.m[11] * b.m[12];
            t.m[9] = a.m[8] * b.m[1] + a.m[9] * b.m[5] + a.m[10] * b.m[9] + a.m[11] * b.m[13];
            t.m[10] = a.m[8] * b.m[2] + a.m[9] * b.m[6] + a.m[10] * b.m[10] + a.m[11] * b.m[14];
            t.m[11] = a.m[8] * b.m[3] + a.m[9] * b.m[7] + a.m[10] * b.m[11] + a.m[11] * b.m[15];

            t.m[12] = a.m[12] * b.m[0] + a.m[13] * b.m[4] + a.m[14] * b.m[8] + a.m[15] * b.m[12];
            t.m[13] = a.m[12] * b.m[1] + a.m[13] * b.m[5] + a.m[14] * b.m[9] + a.m[15] * b.m[13];
            t.m[14] = a.m[12] * b.m[2] + a.m[13] * b.m[6] + a.m[14] * b.m[10] + a.m[15] * b.m[14];
            t.m[15] = a.m[12] * b.m[3] + a.m[13] * b.m[7] + a.m[14] * b.m[11] + a.m[15] * b.m[15];

            return t;
        }

        public static Matrix4x4 SetProjectionMatrix(double angleOfView, double near, double far, Matrix4x4 m)
        {

            double scale = 1 / Math.Tan(angleOfView * 0.5 * Math.PI / 180);

            m.m[0] = scale;
            m.m[5] = scale;
            m.m[10] = -far / (far - near);
            m.m[9] = -far * near / (far - near);
            m.m[6] = -1;
            m.m[15] = 0;

            return m;
        }

        public static Vector3 MultiplyPoint(Vector3 a, Matrix4x4 m)
        {
            //out = in * Mproj
            Vector3 b = new Vector3();

            double x, y, z, w;

            x = a.x * m.m[0] + a.y * m.m[4] + a.z * m.m[8] + m.m[12];
            y = a.x * m.m[1] + a.y * m.m[5] + a.z * m.m[9] + m.m[13];
            z = a.x * m.m[2] + a.y * m.m[6] + a.z * m.m[10] + m.m[14];
            w = a.x * m.m[3] + a.y * m.m[7] + a.z * m.m[11] + m.m[15];

            b.x = x / w;
            b.y = y / w;
            b.z = z / w;

            return b;
        }

        public static Vector3 MultiplyVector(Vector3 a, Matrix4x4 m)
        {
            Vector3 b = new Vector3();

            double x, y, z;

            x = a.x * m.m[0] + a.y * m.m[4] + a.z * m.m[8];
            y = a.x * m.m[1] + a.y * m.m[5] + a.z * m.m[9];
            z = a.x * m.m[2] + a.y * m.m[6] + a.z * m.m[10];

            b.x = x;
            b.y = y;
            b.z = z;

            return b;
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            return a.Multiply(a, b);
        }


    }
}
