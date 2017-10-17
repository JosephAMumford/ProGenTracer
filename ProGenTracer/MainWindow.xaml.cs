//Created by Joseph Mumford 10/11/2017

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
using System.Drawing;
using System.Diagnostics;


namespace ProGenTracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static WriteableBitmap writeableBitmap;
        static Window w;
        //static Image i;



        //    public RayTracerForm()
        //    {
        //        bitmap = new Bitmap(width, height);
        //
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

        Bitmap bitmap;
        PictureBox pictureBox;
        
        private void GenerateRender(object sender, RoutedEventArgs e)
        {
            Form newForm = new Form();
            RenderSettings rs = new RenderSettings();
            rs.ImageHeight = 500;
            rs.ImageWidth = 500;

            bitmap = new Bitmap(rs.ImageWidth, rs.ImageHeight);
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = bitmap;

            newForm.ClientSize = new System.Drawing.Size(rs.ImageWidth, rs.ImageHeight);
            newForm.Controls.Add(pictureBox);
            newForm.Text = "Rendering Image";

            newForm.Show();

            Random r = new Random();
            int cr = 0;
            int cg = 0;
            int cb = 0;

            for (int y = 0; y < rs.ImageHeight; y++)
            {
                for(int x = 0; x < rs.ImageWidth; x++)
                {
                    cr = r.Next(255);
                    cg = r.Next(255);
                    cb = r.Next(255);

                    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(cr,cg,cb));
                    if (x == 0)
                    {
                        pictureBox.Refresh();
                    }
                }
            }
            pictureBox.Invalidate();

            //RayTracer rayTracer = new RayTracer(rs.ImageWidth, rs.ImageHeight, (int x, int y, System.Drawing.Color color) =>
            //{
            //    bitmap.SetPixel(x, y, color);
            //    if (x == 0) pictureBox.Refresh();
            //});
            //rayTracer.Render(rayTracer.DefaultScene);
            //pictureBox.Invalidate();

        }

        private void GenerateRender2(object sender, RoutedEventArgs e)
        {
            //List<System.Drawing.Color> colorMap = new List<System.Drawing.Color>();

            //RenderSettings rs = new RenderSettings();
            //rs.ImageHeight = 500;
            //rs.ImageWidth = 500;

            //System.ComponentModel.TypeConverter converter = new System.ComponentModel.TypeConverter();

            //i = new Image();
            //RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.NearestNeighbor);
            //RenderOptions.SetEdgeMode(i, EdgeMode.Aliased);

            //w = new Window();
            //w.Height = rs.ImageHeight;
            //w.Width = rs.ImageWidth;
            //w.Content = i;
            //w.Show();

            //writeableBitmap = new WriteableBitmap(
            //    (int)w.ActualWidth,
            //    (int)w.ActualHeight,
            //    96,
            //    96,
            //    PixelFormats.Bgr32,
            //    null);

            //i.Source = writeableBitmap;

            //i.Stretch = Stretch.None;
            //i.HorizontalAlignment = HorizontalAlignment.Left;
            //i.VerticalAlignment = VerticalAlignment.Top;

            //Random r = new Random();
            //int cr = 0;
            //int cg = 0;
            //int cb = 0;

            //for (int x = 0; x <  rs.ImageWidth; x++)
            //{
            //    for(int y = 0; y < rs.ImageHeight; y++)
            //    {
            //        cr = r.Next(255);
            //        cg = r.Next(255);
            //        cb = r.Next(255);

            //        //DrawPixel(x, y, System.Drawing.Color.FromArgb(cr,cg,cb));
            //        colorMap.Add(System.Drawing.Color.FromArgb(cr, cg, cb));
            //    }

            //}


            //RayTracer rayTracer = new RayTracer(rs.ImageWidth, rs.ImageHeight, (int x, int y, System.Drawing.Color color1) =>
            //{
            //    DrawPixel(x, y, color1);

            //});

            //rayTracer.Render(rayTracer.DefaultScene);

        }

        static void DrawPixel(int x, int y, System.Drawing.Color color)
        {
            int column = x;
            int row = y;

            // Reserve the back buffer for updates.
            writeableBitmap.Lock();

            unsafe
            {
                // Get a pointer to the back buffer.
                int pBackBuffer = (int)writeableBitmap.BackBuffer;

                // Find the address of the pixel to draw.
                pBackBuffer += row * writeableBitmap.BackBufferStride;
                pBackBuffer += column * 4;

                // Compute the pixel's color.
               // int color_data = 255 << 16; // R
               // color_data |= 128 << 8;   // G
               // color_data |= 255 << 0;   // B

                // Assign the color data to the pixel.
                //*((int*)pBackBuffer) = color_data;
                *((int*)pBackBuffer) = color.ToArgb();
            }

            // Specify the area of the bitmap that changed.
            writeableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));

            // Release the back buffer and make it available for display.
            writeableBitmap.Unlock();
        }

        private void GenerateRender1(object sender, RoutedEventArgs e)
        {
            RenderWindow renderer = new RenderWindow();
            RenderSettings rs = new RenderSettings();
            rs.ImageHeight = 260;
            rs.ImageWidth = 280;
            renderer.SetWindow(rs);
            renderer.RenderImage.Width = rs.ImageWidth;
            renderer.RenderImage.Height = rs.ImageHeight;
            //Bitmap newRender = new Bitmap(rs.ImageWidth, rs.ImageHeight);

            //renderer.RenderImage;

            PixelFormat pf = PixelFormats.Bgr32;
            int bpp = (pf.BitsPerPixel + 7) / 8;
            int rawStride = 4 * ((rs.ImageWidth * bpp + 3) / 4);
            byte[] rawImage = new byte
                [rawStride * rs.ImageHeight];

            List<byte> ri = new List<byte>();

            RayTracer rayTracer = new RayTracer(rs.ImageWidth, rs.ImageHeight, (int x, int y, System.Drawing.Color color) =>
            {

                ri.Add(color.B);
                ri.Add(color.G);
                ri.Add(color.R);
                ri.Add(color.A);
                
                //newRender.SetPixel(x, y, color);
                //
                //bitmap.SetPixel(x, y, color);
                //if (x == 0) pictureBox.Refresh();
            });
            rayTracer.Render(rayTracer.DefaultScene);

            BitmapSource bitmap = BitmapSource.Create(rs.ImageWidth, rs.ImageHeight, 300, 300, pf, null, ri.ToArray(), rawStride);

            renderer.RenderImage.Source = bitmap;

            //RayTracerForm.RunRenderer();   
        }
    }
}
