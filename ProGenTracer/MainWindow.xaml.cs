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
            so.position = new Utilities.Vector3(-1, 0, 8);
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();
            Material mat = new Material();
            mat.color = Utilities.Color.Set(0.0, 0.0, 1.0);
            mat.Specular = 25;
            mat.Type = 3;
            //Clockwise rotation
            newVertices.Add(new Vector3(0.0, -1, 3));
            newVertices.Add(new Vector3(-1.0, -1, 3));
            newVertices.Add(new Vector3(-1.0, 1, 4));
            newVertices.Add(new Vector3(0.0, 1, 4));
            newMesh.SetVertices(newVertices);

            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);
            newMesh.SetTriangles(newTriangles);

            newMesh.ComputeNormals();

            so.mesh = newMesh;
            so.material = mat;
            scene.SceneObjects.Add(so);

            SceneObject s1 = new SceneObject();
            s1.position = new Utilities.Vector3(0, 0, 0);
            Mesh mesh1 = new Mesh();
            Material mat1 = new Material();
            mat1.color = Utilities.Color.Set(1.0, 1.0, 1.0);
            mat1.Specular = 25;
            mat1.Type = 3;
            newVertices.Clear();
            newTriangles.Clear();

            newVertices.Add(new Vector3(10.0, -1.0, 0));
            newVertices.Add(new Vector3(-10.0, -1.0, 0));
            newVertices.Add(new Vector3(-10.0, -1.0, 20));
            newVertices.Add(new Vector3(10.0, -1.0, 20));
            mesh1.SetVertices(newVertices);

            newTriangles.Add(0);
            newTriangles.Add(1);
            newTriangles.Add(2);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);
            mesh1.SetTriangles(newTriangles);

            mesh1.ComputeNormals();

            s1.mesh = mesh1;
            s1.material = mat1;
            scene.SceneObjects.Add(s1);

            SceneObject s3 = new SceneObject();
            s3.position = new Vector3(0.5, 0, 3);
            Mesh mesh2 = GenerateSphere();
            Material mat2 = new Material();
            mat2.color = Utilities.Color.Set(0.0, 1.0, 0.0);
            mat2.Type = 3;

            mesh2.ComputeNormals();

            s3.mesh = mesh2;
            s3.material = mat2;
            //scene.SceneObjects.Add(s3);

            Light newlight = new Light();
            newlight.Position = new Utilities.Vector3(0, 2, 3);
            newlight.Direction = new Vector3(1, -0.8, 1);
            newlight.Type = 0;
            newlight.Intensity = 8.0;
            newlight.LightColor = new Utilities.Color(1.0, 1.0, 1.0);
            scene.Lights.Add(newlight);
        }


        public Mesh GenerateSphere()
        {
            Mesh newMesh = new Mesh();
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();

            double SphereRadius = 0.5;

            double dTheta = (2 * Math.PI) / 16;
            double dPhi = (Math.PI) / 8;

            int index = 0;

            for(int i = 0; i < 16; i++)         //theta
            {
                for(int j = 0; j < 8; j++)      //phi
                {
                    double x;
                    double y;
                    double z;

                    x = SphereRadius * Math.Sin(i * dTheta) * Math.Cos(j * dPhi);
                    y = SphereRadius * Math.Cos(i * dTheta);
                    z = SphereRadius * Math.Sin(i * dTheta) * Math.Sin(j * dPhi);
                    newVertices.Add(new Vector3(x,y,z));

                    x = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Cos(j * dPhi);
                    y = SphereRadius * Math.Cos((i + 1) * dTheta);
                    z = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Sin(j * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    x = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Cos((j + 1) * dPhi);
                    y = SphereRadius * Math.Cos((i + 1) * dTheta);
                    z = SphereRadius * Math.Sin((i + 1) * dTheta) * Math.Sin((j + 1) * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    x = SphereRadius * Math.Sin(i * dTheta) * Math.Cos((j + 1) * dPhi);
                    y = SphereRadius * Math.Cos(i * dTheta);
                    z = SphereRadius * Math.Sin(i * dTheta) * Math.Sin((j + 1) * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

                    index = newVertices.Count - 4;

                    newTriangles.Add(index);
                    newTriangles.Add(index + 1);
                    newTriangles.Add(index + 2);
                    newTriangles.Add(index);
                    newTriangles.Add(index + 2);
                    newTriangles.Add(index + 3);

                }
            }

            newMesh.SetVertices(newVertices);
            newMesh.SetTriangles(newTriangles);

            return newMesh;
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
                    int depth = rs.MaxDepth;
                    //for each pixel - Color = castRay( trace(find nearest object), use material to determine color)) 
                    pixelColor = CastRay(newRay, newScene, ref depth);

                    bitmap.SetPixel(x, y, pixelColor.ToDrawingColor());
                    if (x == 0) pictureBox.Refresh();
                }
            }

            pictureBox.Invalidate();
            RenderTimer.Stop();

            string RenderString = RenderTimer.Elapsed.ToString();
            RenderTime.Content = RenderString;
        }

        public Utilities.Color CastRay(Ray ray, Scene scene, ref int depth)
        {
            Utilities.Color newColor = new Utilities.Color();

            Utilities.Color AmbientColor = new Utilities.Color(0.1, 0.1, 0.1);
            Utilities.Color SpecularColor = new Utilities.Color(1.0, 1.0, 1.0);

            if(depth > rs.MaxDepth)
            {
                return Utilities.Color.Background;
            }

            RayHit hit = Trace(scene, ray);
            if (hit.isHit)
            {
                Vector3 hitPoint = hit.hitPoint;
                Vector3 Normal = scene.SceneObjects[hit.HitObjectID].mesh.GetNormal(hit.index);
                Vector2 st = new Vector2();         //St Coordinates

                //GetSurfaceProperties
                Vector3 temp = hitPoint;

                //Get Material Type
                Material mat = scene.SceneObjects[hit.HitObjectID].material;

                //Default
                if (mat.Type == 0)       
                {
                    newColor = mat.color;
                }

                //Flat Shading
                if (mat.Type == 1)
                {
                    newColor = mat.color * Math.Max(0, Vector3.Dot(Normal, -ray.Direction));
                }

                //Smooth Shading - Need to vertex normals
                if(mat.Type == 2)
                {
                    Mesh mesh = scene.SceneObjects[hit.HitObjectID].mesh;
                    Vector3 n0 = mesh.normals[mesh.triangles[hit.index]];
                    Vector3 n1 = mesh.normals[mesh.triangles[hit.index + 1]];
                    Vector3 n2 = mesh.normals[mesh.triangles[hit.index + 2]];

                    Normal = n0 * (1 - hit.uv.x - hit.uv.y);
                    Normal += (n1 * hit.uv.x);
                    Normal += (n2 * hit.uv.y);

                    newColor = mat.color * Math.Max(0, Vector3.Dot(Normal, -ray.Direction));
                }

                //Diffuse
                if(mat.Type == 3)
                {
                    Vector3 L = -scene.Lights[0].Direction;
                    double distance = Vector3.Magnitude(scene.Lights[0].Position - hit.hitPoint);
                    double dist = 1 / (distance * distance);
                    double cosTheta = Clamp(Vector3.Dot(Normal, L), 0.0, 1.0);
                    Vector3 R = Vector3.Reflect(-L, Normal);
                    double cosAlpha = Clamp(Vector3.Dot(Normal, R), 0.0, 1.0);
                    Utilities.Color ambient = (AmbientColor * mat.color);
                    Utilities.Color diffuse = mat.color * scene.Lights[0].LightColor * scene.Lights[0].Intensity * cosTheta * dist;
                    Utilities.Color specular = SpecularColor * scene.Lights[0].LightColor * scene.Lights[0].Intensity * Math.Pow(cosAlpha, 7) * dist;
                    newColor = ambient + diffuse + specular;               
                }
            }
            else
            {
                newColor = Utilities.Color.Background;
            }

            return newColor;
        }

        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
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
                Vector3 position = scene.SceneObjects[i].position;    
                int index = 0;
                
                  
                for (int j = 0; j < numOfTriangles; j++)
                {
                    Vector3 v0 = position + m.vertices[m.triangles[index]];
                    Vector3 v1 = position + m.vertices[m.triangles[index + 1]];
                    Vector3 v2 = position + m.vertices[m.triangles[index + 2]];

                    if (rayTriangleIntersect(v0, v1, v2, ray, ref near, ref rayHit.uv))
                    {
                        if(near <= rayHit.near)
                        {
                            rayHit.index = index;
                            rayHit.near = near;
                            ray.Distance = near;
                            rayHit.isHit = true;
                            rayHit.HitObjectID = i;
                            rayHit.hitPoint = ray.Origin + ray.Direction * near;
                        }
                    }

                    index += 3;
                }
            }

            return rayHit;
        }

        public bool rayTriangleIntersect(Vector3 v0, Vector3 v1, Vector3 v2, Ray ray, ref double near, ref Vector2 dUv)
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

            dUv.x = du * invDet;
            dUv.y = dv * invDet;

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
