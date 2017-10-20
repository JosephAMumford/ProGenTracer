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
            ren.w.position = new Vector3(0, 0, 5);
            ren.w.size = new Vector3(10, 10, 2);

            for(int y = 0; y < rs.ImageHeight; y++) 
            {
                for(int x = 0; x < rs.ImageWidth; x++)
                {
                    Utilities.Color pixel = ren.TraceRay(new Ray(new Vector3(x - (rs.ImageWidth*0.5), y - (rs.ImageHeight * 0.5), 0), new Vector3(0, 0, 6)));
                    bitmap.SetPixel(x, y,pixel.ToDrawingColor());
                    if (x == 0) pictureBox.Refresh();
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
    }
}
