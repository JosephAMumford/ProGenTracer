//Created by Joseph Mumford 10/29/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

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
        Random rand = new Random();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Renderer()
        {

        }
        /// <summary>
        /// Constructor referencing PictureBox control box and
        /// destination bitmap image for rendered scene
        /// </summary>
        /// <param name="box"></param>
        /// <param name="image"></param>
        public Renderer(ref PictureBox box, ref Bitmap image)
        {
            RenderBox = box;
            RenderImage = image;
        }
        /// <summary>
        /// Initialize renderer with current render settings
        /// </summary>
        /// <param name="settings"></param>
        public void IntializeRenderer(RenderSettings settings)
        {
            rs = settings;
            GenerateScene(CurrentScene);
        }
        /// <summary>
        /// Renders the current scene to referenced bitmap image in PictureBox
        /// </summary>
        public void RenderScene()
        {
            Camera SceneCamera = Camera.Create(new Vector3(3, 3, 7), new Vector3(0, 1, 0));     // TO DO: Get this from scene file
            Utilities.Color pixelColor = new Utilities.Color();                                 // Used to set pixel color in rendered image
            Ray newRay = new Ray();

            Stopwatch RenderTimer = new Stopwatch();
            RenderTimer.Start();

            // Set image scale and aspect ratio
            double scale = Math.Tan(MathExtensions.DegreesToRadians(rs.FieldOfView * 0.5));
            double imageAspectRatio = rs.ImageWidth / rs.ImageHeight;

            // Create camera rotation matrix and set ray origin
            newRay.Origin = SceneCamera.position;
            Matrix4x4 CameraToWorld = Matrix4x4.LookAt(SceneCamera.position, SceneCamera.target);

            // For each pixel in the image, cast a ray from camera into world space
            for (int y = 0; y < rs.ImageHeight; y++)
            {
                for (int x = 0; x < rs.ImageWidth; x++)
                {
                    // Generate ray direction
                    double px = (2 * (x+ 0.5) / (double)rs.ImageWidth - 1) * imageAspectRatio * scale;
                    double py = (1 - 2 * (y + 0.5) / (double)rs.ImageHeight) * scale;
                    int depth = rs.MaxDepth;
                    newRay.Direction = Matrix4x4.MultiplyVector(new Vector3(px, py, -1), CameraToWorld);
                    newRay.Direction = Vector3.Normalize(newRay.Direction);
                    newRay.Distance = double.PositiveInfinity;
                    pixelColor = CastRay(newRay, CurrentScene, ref depth);

                    RenderImage.SetPixel(x, y, pixelColor.ToDrawingColor());
                    if (x == 0) RenderBox.Refresh();
                }
            }

            RenderBox.Invalidate();
            RenderTimer.Stop();

            rs.RenderTime = RenderTimer.Elapsed.ToString();
        }

        public void GenerateScene(Scene scene)
        {
            // Lists used in mesh generation
            List<Vector3> newVertices = new List<Vector3>();
            List<Vector2> newUVs = new List<Vector2>();
            List<int> newTriangles = new List<int>();
            List<Utilities.Color> newColors = new List<Utilities.Color>();

            Texture newTexture = TextureLoader.LoadBitmap("Resources/Textures/flat.bmp");

            int index = 0;
            Vector3 size;
            Vector3 d;

            //Scene Object 1
            SceneObject so = new SceneObject();
            Mesh newMesh = new Mesh();
            so.Position = new Utilities.Vector3(0, 2, 0);

            Material mat = new Material();
            mat.MainColor = Utilities.Color.Set(1.0, 1.0, 1.0);
            mat.Specular = 25;
            mat.Type = 3;
            mat.MainTexture = newTexture;

            size = new Vector3(1, 1, 1);
            d = new Vector3(size.x * 0.5, size.y * 0.5, size.z * 0.5);
            so.BBox.Scale = size;
            so.BBox.ResizeBoundingBox();

            //Front
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            newColors.Add(new Utilities.Color(0.2, 0.2, 0.2));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));

            //Back
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));

            //Top
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));

            //Bottom
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));

            //Left       
            newVertices.Add(new Vector3(-d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, -d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));

            //Right
            newVertices.Add(new Vector3(d.x, -d.y, d.z));
            newVertices.Add(new Vector3(d.x, -d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));

            newMesh.SetVertices(newVertices);
            newMesh.SetTriangles(newTriangles);
            newMesh.SetUVs(newUVs);
            newMesh.SetColors(newColors);
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
            mat1.MainTexture = newTexture;
            mat1.Specular = 25;
            mat1.Type = 3;

            size = new Vector3(5, 0.1, 5);
            d = new Vector3(size.x * 0.5, size.y * 0.5, size.z * 0.5);
            s1.BBox.Scale = size;
            s1.BBox.ResizeBoundingBox();

            newVertices.Clear();
            newTriangles.Clear();
            newUVs.Clear();
            newColors.Clear();

            newVertices.Add(new Vector3(-d.x, d.y, d.z));
            newVertices.Add(new Vector3(d.x, d.y, d.z));
            newVertices.Add(new Vector3(-d.x, d.y, -d.z));
            newVertices.Add(new Vector3(d.x, d.y, -d.z));
            mesh1.SetVertices(newVertices);

            index = newVertices.Count - 4;
            newTriangles.Add(index);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 2);
            newTriangles.Add(index + 1);
            newTriangles.Add(index + 3);
            newTriangles.Add(index + 2);
            mesh1.SetTriangles(newTriangles);

            newUVs.Add(new Utilities.Vector2(0, 0));
            newUVs.Add(new Utilities.Vector2(1, 0));
            newUVs.Add(new Utilities.Vector2(0, 1));
            newUVs.Add(new Utilities.Vector2(1, 1));
            mesh1.SetUVs(newUVs);

            newColors.Add(new Utilities.Color(0.25, 0.25, 0.25));
            newColors.Add(new Utilities.Color(0.5, 0.5, 0.5));
            newColors.Add(new Utilities.Color(1, 1, 1));
            newColors.Add(new Utilities.Color(1, 1, 1));
            mesh1.SetColors(newColors);

            mesh1.ComputeNormals();

            s1.Mesh = mesh1;
            s1.Material = mat1;
            scene.SceneObjects.Add(s1);

            //Scene Object 3
            SceneObject s3 = new SceneObject();
            s3.Position = new Vector3(0, 1, 0);
            Mesh mesh2 = MeshBuilder.GenerateSphere();
            Material mat2 = new Material();
            mat2.MainColor = Utilities.Color.Set(0.0, 1.0, 0.0);
            mat2.Type = 3;
            s3.BBox.Scale = new Vector3(1, 1, 1);
            s3.BBox.ResizeBoundingBox();
            mesh2.ComputeNormals();

            s3.Mesh = mesh2;
            s3.Material = mat2;
            //scene.SceneObjects.Add(s3);

            //Light
            Light newlight = new Light();
            newlight.Position = new Utilities.Vector3(1, 2, 3);
            newlight.Direction = new Vector3(-0.5, -1, -1);
            newlight.Type = 0;
            newlight.Intensity = 4.0;
            newlight.LightColor = new Utilities.Color(1.0, 1.0, 0.0);
            scene.Lights.Add(newlight);

            Light newlight1 = new Light();
            newlight1.Position = new Utilities.Vector3(-1, 2, 3);
            newlight1.Direction = new Vector3(-0.5, -1, 1);
            newlight1.Type = 0;
            newlight1.Intensity = 4.0;
            newlight1.LightColor = new Utilities.Color(0.0, 0.0, 1.0);
            scene.Lights.Add(newlight1);
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
                    Utilities.Color ambient = (AmbientColor * mat.MainColor);

                    Utilities.Color textureColor = Utilities.Color.Set(1.0, 1.0, 1.0);
                    Utilities.Color vertexColor = Utilities.Color.Set(1.0, 1.0, 1.0);

                    double LightIntesity = 0;

                    Utilities.Color LightColor = Utilities.Color.Set(0.0, 0.0, 0.0);

                    for (int i = 0; i < scene.Lights.Count; i++)
                    {
                        Vector3 L = -scene.Lights[i].Direction;
                        double distance = Vector3.Magnitude(scene.Lights[i].Position - hit.hitPoint);
                        double dist = 1 / (distance * distance);
                        double cosTheta = MathExtensions.Clamp(Vector3.Dot(Normal, L), 0.0, 1.0);
                        Vector3 R = Vector3.Reflect(-L, Normal);
                        double cosAlpha = MathExtensions.Clamp(Vector3.Dot(Normal, R), 0.0, 1.0);

                        RayHit shadowHit = new RayHit();
                        Ray shadowRay = new Ray();
                        shadowRay.Origin = hit.hitPoint;
                        shadowRay.Direction = (scene.Lights[i].Position - hit.hitPoint);
                        shadowRay.Distance = double.PositiveInfinity;
                        shadowHit = Trace(scene, shadowRay);
                        if(shadowHit.isHit == false)
                        {
                            LightColor += scene.Lights[i].LightColor * scene.Lights[i].Intensity * cosTheta * dist;
                        }   
                    }
                    

                    if (mat.MainTexture.PixelMap != null)
                    {
                        //Texture Coordinates                
                        Vector2 st0 = scene.SceneObjects[hit.HitObjectID].Mesh.uv[scene.SceneObjects[hit.HitObjectID].Mesh.triangles[hit.index]];
                        Vector2 st1 = scene.SceneObjects[hit.HitObjectID].Mesh.uv[scene.SceneObjects[hit.HitObjectID].Mesh.triangles[hit.index + 1]];
                        Vector2 st2 = scene.SceneObjects[hit.HitObjectID].Mesh.uv[scene.SceneObjects[hit.HitObjectID].Mesh.triangles[hit.index + 2]];
                        Vector2 tex =  st0 * (1 - hit.uv.x - hit.uv.y) + st1 * hit.uv.x + st2 * hit.uv.y;

                        //Vertex Color
                        Utilities.Color vc0 = scene.SceneObjects[hit.HitObjectID].Mesh.colors[scene.SceneObjects[hit.HitObjectID].Mesh.triangles[hit.index]];
                        Utilities.Color vc1 = scene.SceneObjects[hit.HitObjectID].Mesh.colors[scene.SceneObjects[hit.HitObjectID].Mesh.triangles[hit.index + 1]];
                        Utilities.Color vc2 = scene.SceneObjects[hit.HitObjectID].Mesh.colors[scene.SceneObjects[hit.HitObjectID].Mesh.triangles[hit.index + 2]];
                        vertexColor = vc0 * (1 - hit.uv.x - hit.uv.y) + vc1 * hit.uv.x + vc2 * hit.uv.y;

                        int tx = (int)(tex.x * mat.MainTexture.Width);
                        int ty = (int)(tex.y * mat.MainTexture.Height);
                        textureColor = mat.MainTexture.GetPixel(tx, ty);
                    }
                    Utilities.Color diffuse = vertexColor * textureColor * LightColor;
                    //Utilities.Color specular = SpecularColor * scene.Lights[0].LightColor * scene.Lights[0].Intensity * Math.Pow(cosAlpha, 5) * dist;
                    newColor = ambient + diffuse;            //ambient + diffuse + SpecularColor;
                }
            }
            else
            {
                newColor = Utilities.Color.Background;
            }

            return newColor;
        }

        public RayHit Trace(Scene scene, Ray ray)
        {
            double near = double.PositiveInfinity;
            RayHit rayHit = new RayHit();
            rayHit.near = near;

            bool HitBBox = false;
            int ObjectID = 0;
            
            //Check to find nearest bounding box
            for (int i = 0; i < scene.SceneObjects.Count; i++)
            {
                //Check bounding box
                int numBoxTriangles = scene.SceneObjects[i].BBox.Mesh.triangles.Length / 3;
                Mesh b = scene.SceneObjects[i].BBox.Mesh;
                Vector3 bPosition = scene.SceneObjects[i].Position;
                int bIndex = 0;

                for (int k = 0; k < numBoxTriangles; k++)
                {
                    Vector3 b0 = bPosition + b.vertices[b.triangles[bIndex]];
                    Vector3 b1 = bPosition + b.vertices[b.triangles[bIndex + 1]];
                    Vector3 b2 = bPosition + b.vertices[b.triangles[bIndex + 2]];
                    Vector2 bUV = new Vector2();
                    if (rayTriangleIntersect(b0, b1, b2, ray, ref near, ref bUV))
                    {
                        if (near < rayHit.near)
                        {
                            rayHit.near = near;
                            HitBBox = true;
                            ObjectID = i;
                        }
                    }

                    bIndex += 3;
                }
            }

            near = double.PositiveInfinity;
            rayHit.near = near;

            if (HitBBox == true)
            {
                int numOfTriangles = scene.SceneObjects[ObjectID].Mesh.triangles.Length / 3;
                Mesh m = scene.SceneObjects[ObjectID].Mesh;
                Vector3 position = scene.SceneObjects[ObjectID].Position;
                int index = 0;

                for (int j = 0; j < numOfTriangles; j++)
                {
                    Vector3 v0 = position + m.vertices[m.triangles[index]];
                    Vector3 v1 = position + m.vertices[m.triangles[index + 1]];
                    Vector3 v2 = position + m.vertices[m.triangles[index + 2]];

                    if (rayTriangleIntersect(v0, v1, v2, ray, ref near, ref rayHit.uv))
                    {
                        if (near < rayHit.near)
                        {
                            rayHit.index = index;
                            rayHit.near = near;
                            ray.Distance = near;
                            rayHit.isHit = true;
                            rayHit.HitObjectID = ObjectID;
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
            //Ray and triangle are parallel is zero
            //if (det == 0 || det < 0)
            if(det < 0.0001)
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
