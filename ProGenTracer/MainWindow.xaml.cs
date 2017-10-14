//Created by Joseph Mumford 10/11/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private void GenerateRender(object sender, RoutedEventArgs e)
        {
            RenderWindow renderer = new RenderWindow();
            RenderSettings rs = new RenderSettings();
            rs.ImageHeight = 480;
            rs.ImageWidth = 640;
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
