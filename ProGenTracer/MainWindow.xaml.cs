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

            rs.ImageHeight = int.Parse(ImageResY.Text);
            rs.ImageWidth = int.Parse(ImageResX.Text);

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

            double aspectRatio = rs.ImageWidth / rs.ImageHeight;

            for (int y = 0; y < rs.ImageHeight; y++)
            {
                for (int x = 0; x < rs.ImageWidth; x++)
                {

                    double u = (x + 0.5) / rs.ImageWidth;
                    double v = (y + 0.5) / rs.ImageHeight;
                    double px = (2 * u - 1) * aspectRatio * Math.Tan(rs.FieldOfView / 2);
                    double py = (1 - 2 * v) * Math.Tan(rs.FieldOfView / 2);
                    Ray newRay = new Utilities.Ray();
                    newRay.Origin = new Utilities.Vector3(0, 0, 0);
                    newRay.Direction = Vector3.Normalize(new Vector3(px, py, 1) - newRay.Origin);
                    Utilities.Color pixel = w.intersect(newRay.Origin, newRay.Direction, double.PositiveInfinity);
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
