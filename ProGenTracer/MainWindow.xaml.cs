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
        

        public void GenerateScene(Scene scene)
        {
            SceneObject so = new SceneObject();
            Mesh newMesh = new Mesh();
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();
            Material mat = new Material();
            mat.color = Utilities.Color.Set(1.0, 0.0, 0.0);
            //Clockwise rotation
            newVertices.Add(new Vector3(0.5, -0.5, 3));
            newVertices.Add(new Vector3(-0.5, -0.5, 3));
            newVertices.Add(new Vector3(-0.5, 0.5, 5));
            newVertices.Add(new Vector3(0.5, 0.5, 5));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            newMesh.SetVertices(newVertices);
            newMesh.SetTriangles(newTriangles);

            so.mesh = newMesh;
            so.material = mat;
            scene.SceneObjects.Add(so);

            SceneObject s1 = new SceneObject();
            Mesh mesh1 = new Mesh();
            Material mat1 = new Material();
            mat1.color = Utilities.Color.Set(0.0, 0.0, 1.0);
            newVertices.Clear();
            newTriangles.Clear();

            newVertices.Add(new Vector3(1.0, -0.5, 4));
            newVertices.Add(new Vector3(0, -0.5, 4));
            newVertices.Add(new Vector3(0, 0.5, 4));
            newVertices.Add(new Vector3(1.0, 0.5, 4));
            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);

            mesh1.SetVertices(newVertices);
            mesh1.SetTriangles(newTriangles);

            s1.mesh = mesh1;
            s1.material = mat1;
            scene.SceneObjects.Add(s1);
        }

        private void GenerateRender(object sender, RoutedEventArgs e)
        {
            Scene newScene = new Scene();
            GenerateScene(newScene);

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
                    newRay.Distance = double.PositiveInfinity;

                    RayHit hit = Trace(newScene, newRay);
                    if (hit.isHit)
                    {
                        pixelColor = newScene.SceneObjects[hit.HitObjectID].material.color;
                    }
                    else
                    {
                        pixelColor = Utilities.Color.Background;
                    }

                    //for each pixel - Color = castRay( trace(find nearest object), use material to determine color)) 

                    bitmap.SetPixel(x, y, pixelColor.ToDrawingColor());
                    if (x == 0) pictureBox.Refresh();
                }
            }

            pictureBox.Invalidate();
            RenderTimer.Stop();

            string RenderString = RenderTimer.Elapsed.ToString();
            RenderTime.Content = RenderString;
        }

        public Utilities.Color CastRay(Ray ray, Scene scene)
        {
            Utilities.Color newColor = new Utilities.Color();

            return newColor;
        }

        public RayHit Trace(Scene scene, Ray ray)
        {
            double near = double.PositiveInfinity;
            RayHit rayHit = new RayHit();
            rayHit.near = near;

            for (int i = 0; i < scene.SceneObjects.Count; i++)
            {
                int numOfTriangles = scene.SceneObjects[i].mesh.triangles.Length / 3;
                Mesh m = scene.SceneObjects[i].mesh;
                
                int index = 0;
                  
                for (int j = 0; j < numOfTriangles; j++)
                {
                    Vector3 v0 = m.vertices[m.triangles[index]];
                    Vector3 v1 = m.vertices[m.triangles[index + 1]];
                    Vector3 v2 = m.vertices[m.triangles[index + 2]];

                    if (rayTriangleIntersect(v0, v1, v2, ray, ref near, 0, 0))
                    {
                        if(near <= rayHit.near)
                        {
                            rayHit.near = near;
                            ray.Distance = near;
                            rayHit.isHit = true;
                            rayHit.HitObjectID = i;
                        }
                    }

                    index += 3;
                }
            }

            return rayHit;
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
                //hit = rayTriangleIntersect(p1, p2, p3, ray, near, uv.x, uv.y);

                //object = i;
                //near = near;
                //index = index;
                //uv = uv;
            }

            return hit;

        }

        public bool rayTriangleIntersect(Vector3 v0, Vector3 v1, Vector3 v2, Ray ray, ref double near, double u, double v)
        {
            double du = 0;
            double dv = 0;
            Vector3 edge1 = v1 - v0;
            Vector3 edge2 = v2 - v0;
            Vector3 pvec = Vector3.Cross(ray.Direction, edge2);
            double det = Vector3.Dot(edge1, pvec);
            if(det == 0 || det < 0)
            {
                return false;
            }

            Vector3 tvec = ray.Origin - v0;
            du = Vector3.Dot(tvec, pvec);
            if(du < 0 || du > det)
            {
                return false;
            }

            Vector3 qvec = Vector3.Cross(tvec, edge1);
            dv = Vector3.Dot(ray.Direction, qvec);
            if(dv < 0 || du + dv > det)
            {
                return false;
            }

            double invDet = 1 / det;

            near = Vector3.Dot(edge2, qvec) * invDet;

            du *= invDet;
            dv *= invDet;

            return true;
        }

        private void OpenRenderWindowcheck(object sender, RoutedEventArgs e)
        {

        }

        private void ImageResX_TextChanged(object sender, TextChangedEventArgs e)
        {
            rs.ImageWidth = int.Parse(ImageResX.Text);
        }

        private void ImageResY_TextChanged(object sender, TextChangedEventArgs e)
        {
            rs.ImageHeight = int.Parse(ImageResY.Text);
        }

        private void FovInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            double f = double.Parse(FovInput.Text);
            rs.FieldOfView = (f * Math.PI)/180;
        }
    }
}
