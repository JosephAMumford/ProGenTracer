using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using ProGenTracer.Utilities;

namespace ProGenTracer.Rendering
{
    public class Renderer
    {

        public Scene CurrentScene = new Scene();
        public PictureBox RenderBox;
        public RenderSettings rs;
        public Bitmap RenderImage;

        public Renderer()
        {

        }

        public Renderer(ref PictureBox box, ref Bitmap image)
        {
            RenderBox = box;
            RenderImage = image;
        }

        public void IntializeRenderer(RenderSettings settings)
        {
            rs = settings;
            GenerateScene(CurrentScene);
        }

        public void RenderScene()
        {
            //Scene newScene = new Scene();
            //GenerateScene(newScene);

            Stopwatch RenderTimer = new Stopwatch();

            RenderTimer.Start();

            //double fov = double.Parse( FovInput.Text);
            double scale = Math.Tan(deg2rad(rs.FieldOfView * 0.5));
            double imageAspectRatio = rs.ImageWidth / rs.ImageHeight;
            Utilities.Color pixelColor = new Utilities.Color();

            Ray newRay = new Ray();
            newRay.Origin = new Vector3(0,6,-4);

            //Vector3 cameraDirection = new Utilities.Vector3(-1,0,0);

            Vector3 target = new Vector3(0, 0, 0);

            //Matrix4x4 cameraToWorld = Matrix4x4.identity;

            Matrix4x4 cameraToWorld = LookAt(newRay.Origin, target);

            //Vector3 orig = new Vector3();
            //orig = Matrix4x4.MultiplyPoint(new Vector3(0, 0, -2), cameraToWorld);

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


                    Vector3 dir = new Vector3();
                    dir = Matrix4x4.MultiplyVector(newRay.Direction, cameraToWorld);
                    dir = Vector3.Normalize(dir);
                    Ray superRay = new Ray();
                    superRay.Origin = newRay.Origin;
                    superRay.Direction = dir;
                    superRay.Distance = double.PositiveInfinity;
                    pixelColor = CastRay(superRay, CurrentScene, ref depth);

                    //pixelColor = CastRay(newRay, CurrentScene, ref depth);

                    RenderImage.SetPixel(x, y, pixelColor.ToDrawingColor());
                    if (x == 0) RenderBox.Refresh();
                }
            }

            RenderBox.Invalidate();
            RenderTimer.Stop();

            rs.RenderTime = RenderTimer.Elapsed.ToString();
            //RenderTime.Content = RenderString;
        }

        Matrix4x4 LookAt(Vector3 from, Vector3 to)
        {
            Vector3 forward = to - from;
            Vector3 right = Vector3.Cross(Vector3.Normalize(new Vector3(0,1,0)), forward);
            Vector3 up = Vector3.Cross(forward, right);

            Matrix4x4 camToWorld = new Matrix4x4();

            camToWorld.m[0] = right.x;
            camToWorld.m[1] = right.y;
            camToWorld.m[2] = right.z;
            camToWorld.m[4] = up.x;
            camToWorld.m[5] = up.y;
            camToWorld.m[6] = up.z;
            camToWorld.m[8] = forward.x;
            camToWorld.m[9] = forward.y;
            camToWorld.m[10] = forward.z;
            camToWorld.m[12] = to.x;
            camToWorld.m[13] = to.y;
            camToWorld.m[14] = to.z;
            camToWorld.m[15] = 1;

            return camToWorld;
        }

        public double deg2rad(double degree)
        {
            return degree * Math.PI / 180;
        }

        public void GenerateScene(Scene scene)
        {
            //Scene Object 1
            SceneObject so = new SceneObject();
            Mesh newMesh = new Mesh();
            so.Position = new Utilities.Vector3(-1, 0, 0);
            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();
            Material mat = new Material();
            mat.MainColor = Utilities.Color.Set(0.0, 1.0, 1.0);
            mat.Specular = 25;
            mat.Type = 3;
            Vector3 size = new Vector3(1, 1, 1);
            Vector3 d = new Vector3(size.x * 0.5, size.y * 0.5, size.z * 0.5);
            so.BBox.Scale = size;
            so.BBox.ResizeBoundingBox();

            int index = 0;

            //Front
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 3);

            //Back
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 3);

            //Top
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 3);

            //Bottom
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 3);

            //Left
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 3);

            //Right
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 3);

            newMesh.SetVertices(newVertices);
            newMesh.SetTriangles(newTriangles);
            newMesh.ComputeNormals();

            so.Mesh = newMesh;
            so.Material = mat;
            scene.SceneObjects.Add(so);

            //Scene Object 2
            SceneObject s1 = new SceneObject();
            s1.Position = new Utilities.Vector3(0, 0, 0);
            Mesh mesh1 = new Mesh();
            Material mat1 = new Material();
            mat1.MainColor = Utilities.Color.Set(1.0, 1.0, 1.0);
            mat1.Specular = 25;
            mat1.Type = 3;
            s1.BBox.Scale = new Vector3(20, 1, 40);
            s1.BBox.ResizeBoundingBox();

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

            s1.Mesh = mesh1;
            s1.Material = mat1;
            scene.SceneObjects.Add(s1);

            //Scene Object 3
            SceneObject s3 = new SceneObject();
            s3.Position = new Vector3(00, 0, 3);
            Mesh mesh2 = GenerateSphere();
            Material mat2 = new Material();
            mat2.MainColor = Utilities.Color.Set(0.0, 1.0, 0.0);
            mat2.Type = 3;
            s3.BBox.Scale = new Vector3(1, 1, 1);
            s3.BBox.ResizeBoundingBox();
            mesh2.ComputeNormals();

            s3.Mesh = mesh2;
            s3.Material = mat2;
            scene.SceneObjects.Add(s3);

            //Light
            Light newlight = new Light();
            newlight.Position = new Utilities.Vector3(0, 2, 5);
            newlight.Direction = new Vector3(-.5, -1, 0);
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

            for (int i = 0; i < 16; i++)         //theta
            {
                for (int j = 0; j < 8; j++)      //phi
                {
                    double x;
                    double y;
                    double z;

                    x = SphereRadius * Math.Sin(i * dTheta) * Math.Cos(j * dPhi);
                    y = SphereRadius * Math.Cos(i * dTheta);
                    z = SphereRadius * Math.Sin(i * dTheta) * Math.Sin(j * dPhi);
                    newVertices.Add(new Vector3(x, y, z));

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

        public Utilities.Color CastRay(Ray ray, Scene scene, ref int depth)
        {
            Utilities.Color newColor = new Utilities.Color();

            Utilities.Color AmbientColor = new Utilities.Color(0.1, 0.1, 0.1);
            Utilities.Color SpecularColor = new Utilities.Color(1.0, 1.0, 1.0);

            if (depth > rs.MaxDepth)
            {
                return Utilities.Color.Background;
            }

            RayHit hit = Trace(scene, ray);
            if (hit.isHit)
            {
                Vector3 hitPoint = hit.hitPoint;
                Vector3 Normal = scene.SceneObjects[hit.HitObjectID].Mesh.GetNormal(hit.index);
                Vector2 st = new Vector2();         //St Coordinates

                //GetSurfaceProperties
                Vector3 temp = hitPoint;

                //Get Material Type
                Material mat = scene.SceneObjects[hit.HitObjectID].Material;

                //Default
                if (mat.Type == 0)
                {
                    newColor = mat.MainColor;
                }

                //Flat Shading
                if (mat.Type == 1)
                {
                    newColor = mat.MainColor * Math.Max(0, Vector3.Dot(Normal, -ray.Direction));
                }

                //Smooth Shading - Need to vertex normals
                if (mat.Type == 2)
                {
                    Mesh mesh = scene.SceneObjects[hit.HitObjectID].Mesh;
                    Vector3 n0 = mesh.normals[mesh.triangles[hit.index]];
                    Vector3 n1 = mesh.normals[mesh.triangles[hit.index + 1]];
                    Vector3 n2 = mesh.normals[mesh.triangles[hit.index + 2]];

                    Normal = n0 * (1 - hit.uv.x - hit.uv.y);
                    Normal += (n1 * hit.uv.x);
                    Normal += (n2 * hit.uv.y);

                    newColor = mat.MainColor * Math.Max(0, Vector3.Dot(Normal, -ray.Direction));
                }

                //Diffuse
                if (mat.Type == 3)
                {
                    Vector3 L = -scene.Lights[0].Direction;
                    double distance = Vector3.Magnitude(scene.Lights[0].Position - hit.hitPoint);
                    double dist = 1 / (distance * distance);
                    double cosTheta = Clamp(Vector3.Dot(Normal, L), 0.0, 1.0);
                    Vector3 R = Vector3.Reflect(-L, Normal);
                    double cosAlpha = Clamp(Vector3.Dot(Normal, R), 0.0, 1.0);
                    Utilities.Color ambient = (AmbientColor * mat.MainColor);
                    Utilities.Color diffuse = mat.MainColor * scene.Lights[0].LightColor * scene.Lights[0].Intensity * cosTheta * dist;
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

                //Check bounding box
                int numBoxTriangles = scene.SceneObjects[i].BBox.Mesh.triangles.Length / 3;
                Mesh b = scene.SceneObjects[i].BBox.Mesh;
                Vector3 bPosition = scene.SceneObjects[i].Position;
                int bIndex = 0;
                bool HitBBox = false;

                for (int k = 0; k < numBoxTriangles; k++)
                {
                    Vector3 b0 = bPosition + b.vertices[b.triangles[bIndex]];
                    Vector3 b1 = bPosition + b.vertices[b.triangles[bIndex + 1]];
                    Vector3 b2 = bPosition + b.vertices[b.triangles[bIndex + 2]];
                    double bBnear = 0;
                    Vector2 bUV = new Vector2();
                    if (rayTriangleIntersect(b0, b1, b2, ray, ref bBnear, ref bUV))
                    {
                        HitBBox = true;
                        break;
                    }

                    bIndex += 3;
                }

                if (HitBBox == true)
                {
                    int numOfTriangles = scene.SceneObjects[i].Mesh.triangles.Length / 3;
                    Mesh m = scene.SceneObjects[i].Mesh;
                    Vector3 position = scene.SceneObjects[i].Position;
                    int index = 0;

                    for (int j = 0; j < numOfTriangles; j++)
                    {
                        Vector3 v0 = position + m.vertices[m.triangles[index]];
                        Vector3 v1 = position + m.vertices[m.triangles[index + 1]];
                        Vector3 v2 = position + m.vertices[m.triangles[index + 2]];

                        if (rayTriangleIntersect(v0, v1, v2, ray, ref near, ref rayHit.uv))
                        {
                            if (near <= rayHit.near)
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
            if (det == 0 || det < 0)
            {
                return false;
            }

            Vector3 tvec = ray.Origin - v0;
            du = Vector3.Dot(tvec, pvec);
            if (du < 0 || du > det)
            {
                return false;
            }

            Vector3 qvec = Vector3.Cross(tvec, edge1);
            dv = Vector3.Dot(ray.Direction, qvec);
            if (dv < 0 || du + dv > det)
            {
                return false;
            }

            double invDet = 1 / det;

            near = Vector3.Dot(edge2, qvec) * invDet;

            dUv.x = du * invDet;
            dUv.y = dv * invDet;

            return true;
        }
    }
}
