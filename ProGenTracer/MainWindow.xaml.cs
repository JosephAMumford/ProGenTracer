//Created by Joseph Mumford 10/11/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProGenTracer.Utilities;
using ProGenTracer.Rendering;
using System.Drawing;
using System.Diagnostics;


namespace ProGenTracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Bitmap bitmap;
        PictureBox pictureBox;
        RenderSettings rs = new RenderSettings();

        public MainWindow()
        {
            InitializeComponent();

            rs.ImageHeight = int.Parse(ImageResY.Text);
            rs.ImageWidth = int.Parse(ImageResX.Text);
            rs.MaxDepth = 3;
            rs.Bias = 0.00001;

            bitmap = new Bitmap(rs.ImageWidth, rs.ImageHeight);
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = bitmap;
            RenderForm.Child = pictureBox;
            pictureBox.Show();
        }

        public double deg2rad(double degree)
        {
            return degree * Math.PI / 180;
        }
        
        private void GenerateRender(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int num = 1;

            World w = new World(num);


            w.position[0] = new Utilities.Vector3(0, 0, -4);
            w.size[0] = new Vector3(0.5, 0.5, 0.5);
            w.colors[0] = new Utilities.Color(0.25, 0.80, 0.23);
            //w.lights = new Light[1];
            //w.lights[0].Position = new Vector3(1, 1, 4);
            //w.lights[0].LightColor = new Utilities.Color(1, 1, 0);

            //for (int i = 0; i < num; i++)
            //{
            //    w.position[i] = new Vector3(rand.Next(-5, 5), rand.Next(-5, 5), rand.Next(1, 10));
            //    w.size[i] = new Vector3(rand.NextDouble() * rand.Next(2), rand.NextDouble() * rand.Next(2), rand.NextDouble() * rand.Next(2));
            //    w.colors[i] = Utilities.Color.Set(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            //}

            Vector3 a = new Utilities.Vector3(1,-1,4);
            Vector3 b = new Utilities.Vector3(-1,0,4);
            Vector3 c = new Utilities.Vector3(0,1,4);

            Mesh newMesh = new Mesh();

            Stopwatch RenderTimer = new Stopwatch();

            RenderTimer.Start();

            double fov = double.Parse(FovInput.Text);
            double scale = Math.Tan(deg2rad(fov * 0.5));
            double imageAspectRatio = rs.ImageWidth / rs.ImageHeight;
            Utilities.Color pixelColor = new Utilities.Color();
            Ray newRay = new Ray();

            newRay.Origin = new Vector3(0, 0, 0);

            for (int y = 0; y < rs.ImageHeight; y++)
            {
                for (int x = 0; x < rs.ImageWidth; x++)
                {
                    //generate ray
                    double px = (2 * (x + 0.5) / (double)rs.ImageWidth - 1) * imageAspectRatio * scale;
                    double py = (1 - 2 * (y + 0.5) / (double)rs.ImageHeight) * scale;
                    newRay.Direction = Vector3.Normalize(new Vector3(px, py, 1));
                    //newRay.Distance = 100;
                    newRay.Distance = double.PositiveInfinity;
                    //pixelColor = castRay(newRay, newMesh, rs, 0);

                    RayHit hit = rayTriangleIntersect(a, b, c, newRay, newRay.Distance, 0, 0);

                    pixelColor = hit.hitColor;

                    //pixelColor = w.intersect(newRay.Origin, newRay.Direction, newRay.Distance);

                    bitmap.SetPixel(x, y, pixelColor.ToDrawingColor());
                    if (x == 0) pictureBox.Refresh();
                }
            }

                    pictureBox.Invalidate();
            RenderTimer.Stop();

            string RenderString = RenderTimer.Elapsed.ToString();
            RenderTime.Content = RenderString;
        }

        //public Utilities.Color castRay(Ray ray, Mesh mesh, RenderSettings rs, double depth, World world)
        //{
        //    Utilities.Color hitColor = Utilities.Color.Set(0.0, 0.0, 0.0);

        //    if(depth > rs.MaxDepth)
        //    {
        //        return new Utilities.Color(0, 0, 0);
        //    }

        //    double tnear = double.MaxValue;

        //    Vector2 uv = new Vector2();
        //    int index = 0;

        //    //Object hitObject;

        //    RayHit newRayHit = TraceRay();
        //    if (newRayHit.isHit)
        //    {
        //        Vector3 hitPoint = newRayHit.hitPoint;
        //        Vector3 Normal = new Vector3(); //Normal
        //        Vector2 st = new Vector2(); //st coordinates

        //        //hitobject = gtSurfaceProperties()
        //        Vector3 tmp = hitPoint;

        //        //Get Material Type
        //        //Reflection and Refraction
        //        //Reflection
        //        //Default
        //        double lightAmount = 0;
        //        //0Utilities.Color SpecularColor = new Utilities.Color();

        //        double SpecularColor = 0;

        //        Vector3 shadowPointOrigin = (Vector3.Dot(ray.Direction, Normal) < 0) ?
        //            hitPoint + Normal * rs.Bias : hitPoint - Normal * rs.Bias;

        //        for(int i = 0; i < world.lights.Length; i++)
        //        {
        //            Vector3 lightDirection = world.lights[i].Position - hitPoint;

        //            //square of the distance
        //            double lightDistance2 = Vector3.Dot(lightDirection, lightDirection);
        //            lightDirection = Vector3.Normalize(lightDirection);
        //            double LdotN = Math.Max(0.0, Vector3.Dot(lightDirection, Normal));

        //            //Object shadowObject;
        //            double nearShadow = double.MaxValue;

        //            RayHit inShadow = TraceRay();
        //            int shad = 0;
        //            if(inShadow.isHit == true)
        //            {
        //                shad = 1;
        //            }
        //            lightAmount += (1 - shad) * world.lights[i].Intensity * LdotN;

        //            Vector3 reflectionDirection = new Vector3();
        //            reflectionDirection = reflect(-lightDirection, Normal);

        //            SpecularColor += Math.Pow(Math.Max(0, -Vector3.Dot(reflectionDirection, ray.Direction)), 2.0) * world.lights[i].Intensity;

        //            hitColor = diffuseColor * lightAmount + kd + SpecularColor * ks;
        //        }

                 
        //    }

        //    return hitColor;
        //}

        public Vector3 reflect(Vector3 i, Vector3 n)
        {
            return i - n * 2 * Vector3.Dot(i, n);
        }

        public RayHit TraceRay(Ray ray, double near, World world)
        {
            RayHit hit = new RayHit();
            hit.isHit = false;

            for(int i = 0; i < world.position.Length; i++)
            {
                double tNeark = double.MaxValue;
                int index = 0;

                Vector2 uv = new Vector2();
                Vector3 p1 = new Vector3(0, 0, 0);  //V0
                Vector3 p2 = new Vector3(0, 0, 0);   //v1
                Vector3 p3 = new Vector3(0, 0, 0);      //v2
                hit = rayTriangleIntersect(p1, p2, p3, ray, near, uv.x, uv.y);

                //object = i;
                //near = near;
                //index = index;
                //uv = uv;
            }

            return hit;

        }

        public RayHit rayTriangleIntersect(Vector3 v0, Vector3 v1, Vector3 v2, Ray ray, double near, double u, double v)
        {
            RayHit hit = new RayHit();
            double du = 0;
            double dv = 0;
            Vector3 edge1 = v1 - v0;
            Vector3 edge2 = v2 - v0;
            Vector3 pvec = Vector3.Cross(ray.Direction, edge2);
            double det = Vector3.Dot(edge1, pvec);
            if(det == 0 || det < 0)
            {
                hit.hitColor = new Utilities.Color(0.0, 0.0, 1.0);
                hit.isHit = false;
                return hit;
            }

            Vector3 tvec = ray.Origin - v0;
            du = Vector3.Dot(tvec, pvec);
            if(du < 0 || du > det)
            {
                hit.isHit = false;
                hit.hitColor = new Utilities.Color(1.0,0.0,0.0);
                return hit;
            }

            Vector3 qvec = Vector3.Cross(tvec, edge1);
            dv = Vector3.Dot(ray.Direction, qvec);
            if(dv < 0 || du + dv > det)
            {
                hit.hitColor = new Utilities.Color(0.0, 1.0, 0.0);
                hit.isHit = false;
                return hit;
            }

            double invDet = 1 / det;

            near = Vector3.Dot(edge2, qvec) * invDet;

            du *= invDet;
            dv *= invDet;

            hit.isHit = true;
            hit.u = du;
            hit.v = dv;
            hit.hitColor = new Utilities.Color(0.75, 1.0, 0.5);
            return hit;
        }

        private void GenerateRender1(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int num = 50;

            World w = new World(num);

            for (int i = 0; i < num; i++)
            {
                w.position[i] = new Vector3(rand.Next(-5, 5), rand.Next(-5, 5), rand.Next(1, 10));
                w.size[i] = new Vector3(rand.NextDouble() * rand.Next(2), rand.NextDouble() * rand.Next(2), rand.NextDouble() * rand.Next(2));
                w.colors[i] = Utilities.Color.Set(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            }

            Stopwatch RenderTimer = new Stopwatch();

            RenderTimer.Start();

            double aspectRatio = (double)(rs.ImageWidth / rs.ImageHeight);

            Vector3 ver0 = new Vector3(0,0,1);
            Vector3 ver1 = new Vector3(0.25, 0.0, 1);
            Vector3 ver2 = new Vector3(0, 0.25, 1);
            Utilities.Color c1 = new Utilities.Color(0.6,10.4,0.1);
            Utilities.Color c2 = new Utilities.Color(0.1,0.5,0.3);
            Utilities.Color c3 = new Utilities.Color(0.1,0.3,0.7);
            double fov = double.Parse(FovInput.Text);
            double scale = Math.Tan(((fov*0.5* Math.PI)/180));

            RayHit hitray = new RayHit();
            Ray newRay = new Utilities.Ray();
            newRay.Distance = double.PositiveInfinity;
            newRay.Origin = new Utilities.Vector3(0, 0, 0);
            hitray.distance = double.PositiveInfinity;

            for (int y = 0; y < rs.ImageHeight; y++)
            {
                for (int x = 0; x < rs.ImageWidth; x++)
                {
 
                    double u = (x + 0.5) / rs.ImageWidth;
                    double v = (y + 0.5) / rs.ImageHeight;
                    //double px = (2 * (x + 0.5) / rs.ImageWidth - 1) * aspectRatio * scale;
                    //double py = (1 - 2 * (y + 0.5) / rs.ImageHeight) * scale;
                    //newRay.Distance = hitray.distance;

                    double px = (2 * u - 1) * aspectRatio * Math.Tan(rs.FieldOfView / 2);
                    double py = (1 - 2 * v) * Math.Tan(rs.FieldOfView / 2);
                    //Ray newRay = new Utilities.Ray();
                    newRay.Origin = new Utilities.Vector3(0, 0, 0);
                    newRay.Direction = Vector3.Normalize(new Vector3(px, py, 1));


                    // newRay.Direction = Vector3.Normalize(new Vector3(px, py, 1) - newRay.Origin);
                    //Utilities.Color pixel = w.intersect(newRay.Origin, newRay.Direction, double.PositiveInfinity);
                    //Utilities.Color pixel = w.intersectTriangle(newRay.Origin, newRay.Direction, double.PositiveInfinity);
                    //hitray = w.intersectTri(newRay, ver0, ver1, ver2);

                    //hitray = w.inter(newRay, ver0, ver1, ver2);

                    Utilities.Color pixelColor;

                    if (hitray.isHit == true)
                    {
                        pixelColor = Utilities.Color.Set(hitray.u, hitray.v, 1 - hitray.u - hitray.v);
                        //pixelColor = (c1 * u) + (c2 * v) + (c3 * (1 - u - v));
                    }
                    else
                    {
                        pixelColor = Utilities.Color.Set(0.25, 0.25, 0.25);
                    }

                    bitmap.SetPixel(x, y, pixelColor.ToDrawingColor());
                    if (x == 0) pictureBox.Refresh();
                }
            }
            pictureBox.Invalidate();
            RenderTimer.Stop();

            string RenderString = RenderTimer.Elapsed.ToString();
            RenderTime.Content = RenderString;
        }


        private void OpenRenderWindowcheck(object sender, RoutedEventArgs e)
        {

        }

        private void ImageResX_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int w = int.Parse(ImageResX.Text);
            //rs.ImageWidth = w;
            rs.ImageWidth = int.Parse(ImageResX.Text);
        }

        private void ImageResY_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int h = int.Parse(ImageResY.Text);
            //rs.ImageHeight = h;
            rs.ImageHeight = int.Parse(ImageResY.Text);
        }

        private void FovInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            double f = double.Parse(FovInput.Text);
            rs.FieldOfView = (f * Math.PI)/180;
        }
    }
}
