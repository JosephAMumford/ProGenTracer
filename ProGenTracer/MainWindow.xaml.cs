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

            rs.ImageHeight = 500;
            rs.ImageWidth = 500;

            bitmap = new Bitmap(rs.ImageWidth, rs.ImageHeight);
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = bitmap;
            RenderForm.Child = pictureBox;
            pictureBox.Show();
        }

        private void GenerateRender(object sender, RoutedEventArgs e)
        {
            Renderer ren = new Renderer();
            ren.w.position[0] = new Vector3(1, 0, 2);
            ren.w.size[0] = new Vector3(.5, .5, .5);
            ren.w.colors[0] = Utilities.Color.Set(1.0, 0.0, 0.0);

            ren.w.position[1] = new Vector3(-2, 0, 2);
            ren.w.size[1] = new Vector3(.5, .5, .5);
            ren.w.colors[1] = Utilities.Color.Set(0.0, 1.0, 0.0);

            ren.w.position[2] = new Vector3(1, 0, 3);
            ren.w.size[2] = new Vector3(.5, .5, .5);
            ren.w.colors[2] = Utilities.Color.Set(0.0, 0.0, 1.0);

            Stopwatch RenderTimer = new Stopwatch();

            RenderTimer.Start();
            double aspectRatio = rs.ImageWidth / rs.ImageHeight;
            double fov = (Math.PI / 2);

            for (int y = 0; y < rs.ImageHeight; y++)
            {
                for (int x = 0; x < rs.ImageWidth; x++)
                {

                    double u = (x + 0.5) / rs.ImageWidth;
                    double v = (y + 0.5) / rs.ImageHeight;
                    double px = (2 * u - 1) * aspectRatio * Math.Tan(fov / 2);
                    double py = (1 - 2 * v) * Math.Tan(fov / 2);
                    Ray newRay = new Utilities.Ray();
                    newRay.Origin = new Utilities.Vector3(0, 0, 0);
                    newRay.Direction = Vector3.Normalize(new Vector3(px, py, 1) - newRay.Origin);
                    Utilities.Color pixel = ren.TraceRay(newRay);
                    bitmap.SetPixel(x, y, pixel.ToDrawingColor());
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
    }
}
