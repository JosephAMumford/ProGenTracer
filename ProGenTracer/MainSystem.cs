using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;
//using System.Windows;
using System.Windows.Forms;

namespace ProGenTracer
{

    public class Renderer
    {

        public World w = new World();

        public Utilities.Color TraceRay(Ray ray1)
        {
            Utilities.Color pixelColor;

            bool b = w.intersect(ray1);

            if(b == true)
            {
                pixelColor = Utilities.Color.Set(1.0, 0.0, 0.0);
            }
            else
            {
                pixelColor = Utilities.Color.Set(0.5, 0.5, 0.5);
            }

            return pixelColor;
        }


    }

    public class World
    {
        public Vector3 position;
        public Vector3 size;

        public bool intersect(Ray ray1)
        {
            bool b = false;

            double x = ray1.start.x;
            double y = ray1.start.y;
            double z = ray1.direction.z;

            if(z >= position.z - size.z)  //&& z <= position.z + size.z)
            {
                if(x >= position.x - size.x && x <= position.x + size.x)
                {
                    if(y >= position.y - size.y && y <= position.y + size.y)
                    {
                        b = true;
                    }
                }
            }

            return b;
        }
    }

    public delegate void Action<T, U, V>(T t, U u, V v);

    public class RayTracer
    {
        private int screenWidth;
        private int screenHeight;
        private const int MaxDepth = 8;

        public Action<int, int, System.Drawing.Color> setPixel;

        public RayTracer(int screenWidth, int screenHeight, Action<int, int, System.Drawing.Color> setPixel)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.setPixel = setPixel;
        }

        private IEnumerable<ISect> Intersections(Ray ray, Scene scene)
        {
            return scene.Objects
                        .Select(obj => obj.Intersect(ray))
                        .Where(inter => inter != null)
                        .OrderBy(inter => inter.Distance);
        }

        private double TestRay(Ray ray, Scene scene)
        {
            var isects = Intersections(ray, scene);
            ISect isect = isects.FirstOrDefault();
            if (isect == null)
                return 0;
            return isect.Distance;
        }

        private Utilities.Color TraceRay(Ray ray, Scene scene, int depth)
        {
            var isects = Intersections(ray, scene);
            ISect isect = isects.FirstOrDefault();
            if (isect == null)
                return Utilities.Color.Background;
            return Shade(isect, scene, depth);
        }

        private Utilities.Color GetNaturalColor(SceneObject thing, Vector3 pos, Vector3 norm, Vector3 rd, Scene scene)
        {
            Utilities.Color ret = Utilities.Color.Set(0, 0, 0);
            foreach (Light light in scene.Lights)
            {
                Vector3 ldis = Vector3.Normalize(light.Position - pos);
                Vector3 livec = ldis;
                double neatIsect = TestRay(new Ray() { start = pos, direction = livec }, scene);
                bool isInShadow = !((neatIsect > ldis.Magnitude()) || (neatIsect == 0));
                if (!isInShadow)
                {
                    double illum = Vector3.Dot(livec, norm);
                    Utilities.Color lcolor = illum > 0 ? (light.Color * illum) : Utilities.Color.Set(0, 0, 0);
                    double specular = Vector3.Dot(livec, Vector3.Normalize(rd));
                    Utilities.Color scolor = specular > 0 ? (light.Color * Math.Pow(specular, thing.Surface.Roughness)) : Utilities.Color.Set(0, 0, 0);
                    ret = ret + (lcolor * thing.Surface.Diffuse(pos) +
                        scolor * thing.Surface.Specular(pos));
                }
            }
            return ret;
        }

        private Utilities.Color GetReflectionColor(SceneObject thing, Vector3 pos, Vector3 norm, Vector3 rd, Scene scene, int depth)
        {
            return TraceRay(new Ray() { start = pos, direction = rd }, scene, depth + 1) * thing.Surface.Reflect(pos);
        }

        private Utilities.Color Shade(ISect isect, Scene scene, int depth)
        {
            var d = isect.Ray.direction;
            var pos = (isect.Ray.direction * isect.Distance) + isect.Ray.start;
            var normal = isect.Object.Normal(pos);
            var reflectDir = d - (normal * (2 * Vector3.Dot(normal, d)));
            Utilities.Color ret = Utilities.Color.DefaultColor;
            ret = ret + GetNaturalColor(isect.Object, pos, normal, reflectDir, scene);
            if (depth >= MaxDepth)
            {
                return ret + Utilities.Color.Set(.5, .5, .5);
            }
            return ret + GetReflectionColor(isect.Object, (pos + (reflectDir * .001)), normal, reflectDir, scene, depth);
        }

        private double RecenterX(double x)
        {
            return (x - (screenWidth / 2.0)) / (2.0 * screenWidth);
        }
        private double RecenterY(double y)
        {
            return -(y - (screenHeight / 2.0)) / (2.0 * screenHeight);
        }

        private Vector3 GetPoint(double x, double y, Camera camera)
        {
            return Vector3.Normalize(camera.forward + ((camera.right * RecenterX(x)) + (camera.up * RecenterY(y))) );
        }

        internal void Render(Scene scene)
        {
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    Utilities.Color color = TraceRay(new Ray() { start = scene.Camera.position, direction = GetPoint(x, y, scene.Camera) }, scene, 0);
                    setPixel(x, y, color.ToDrawingColor());
                }
            }
        }

        internal readonly Scene DefaultScene =
            new Scene()
            {
                Objects = new SceneObject[] {
                                new Plane() {
                                    normal = new Vector3(0,1,0),
                                    Offset = 0,
                                    Surface = Surfaces.CheckerBoard
                                },
                                new Sphere() {
                                    Center = new Vector3(0,1,0),
                                    Radius = 0.25,
                                    Surface = Surfaces.Shiny
                                },
                                new Sphere() {
                                    Center = new Vector3(-1,.5,1.5),
                                    Radius = .5,
                                    Surface = Surfaces.Shiny
                                }},
                Lights = new Light[] {
                                new Light() {
                                    Position = new Vector3(-2,2.5,0),
                                    Color = Utilities.Color.Set(.49,.07,.07)
                                },
                                new Light() {
                                    Position = new Vector3(1.5,2.5,1.5),
                                    Color = Utilities.Color.Set(.07,.07,.49)
                                },
                                new Light() {
                                    Position = new Vector3(1.5,2.5,-1.5),
                                    Color = Utilities.Color.Set(.07,.49,.071)
                                },
                                new Light() {
                                    Position = new Vector3(0,3.5,0),
                                    Color = Utilities.Color.Set(.21,.21,.35)
                                }
                },
                Camera = Camera.Create(new Vector3(3, 2, 4), new Vector3(-1, .5, 0))
            };
    }

    static class Surfaces
    {
        // Only works with X-Z plane.
        public static readonly Surface CheckerBoard = new Surface()
        {
            Diffuse = pos => ((Math.Floor(pos.z) + Math.Floor(pos.x)) % 2 != 0)
                                ? Utilities.Color.Set(1, 0.75, 0.75)
                                : Utilities.Color.Set(0.25, 0.5, 0.42),
            Specular = pos => Utilities.Color.Set(1, 0, 1),
            Reflect = pos => ((Math.Floor(pos.z) + Math.Floor(pos.x)) % 2 != 0)
                                ? .1
                                : .7,
            Roughness = 150
        };

        public static readonly Surface Shiny = new Surface()
        {
            Diffuse = pos => Utilities.Color.Set(0, 1, 1),
            Specular = pos => Utilities.Color.Set(.5, .5, .5),
            Reflect = pos => .6,
            Roughness = 50
        };
    }

    public class MainSystem
    {

    }

    public class RenderSettings
    {
        public int ImageWidth = 640;
        public int ImageHeight = 480;
    }

    //public partial class RayTracerForm : Form
    //{
    //    Bitmap bitmap;
    //    PictureBox pictureBox;
    //    const int width = 300;
    //    const int height = 300;

        

    //    public RayTracerForm()
    //    {
    //        bitmap = new Bitmap(width, height);

    //        pictureBox = new PictureBox();
    //        pictureBox.Dock = DockStyle.Fill;
    //        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
    //        pictureBox.Image = bitmap;

    //        ClientSize = new System.Drawing.Size(width, height + 24);
    //        Controls.Add(pictureBox);
    //        Text = "Ray Tracer";
    //        Load += RayTracerForm_Load;

    //        Show();
    //    }

    //    public void RayTracerForm_Load(object sender, EventArgs e)
    //    {
    //        this.Show();
    //        RayTracer rayTracer = new RayTracer(width, height, (int x, int y, System.Drawing.Color color) =>
    //        {
    //            bitmap.SetPixel(x, y, color);
    //            if (x == 0) pictureBox.Refresh();
    //        });
    //        rayTracer.Render(rayTracer.DefaultScene);
    //        pictureBox.Invalidate();

    //    }

    //    [STAThread]
    //    public static void RunRenderer()
    //    {
    //        Application.EnableVisualStyles();

    //        Application.Run(new RayTracerForm());

    //    }
    //}
}
