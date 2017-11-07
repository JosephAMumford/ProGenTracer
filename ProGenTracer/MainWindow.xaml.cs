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
        //Image of rendered scene
        Bitmap bitmap;
        PictureBox pictureBox;
        RenderSettings CurrentRenderSettings = new RenderSettings();
        Renderer MainRenderer;

        public MainWindow()
        {
            InitializeComponent();

            CurrentRenderSettings.ImageHeight = int.Parse(ImageResY.Text);
            CurrentRenderSettings.ImageWidth = int.Parse(ImageResX.Text);
            CurrentRenderSettings.MaxDepth = 3;
            CurrentRenderSettings.Bias = 0.00001;
            CurrentRenderSettings.FieldOfView = double.Parse(FovInput.Text);

            bitmap = new Bitmap(CurrentRenderSettings.ImageWidth, CurrentRenderSettings.ImageHeight);
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = bitmap;
            RenderForm.Child = pictureBox;
            pictureBox.Show();

            MainRenderer = new Renderer(ref pictureBox, ref bitmap);
            MainRenderer.IntializeRenderer(CurrentRenderSettings);
        }

        public void UpdateRenderSettings()
        {
            CurrentRenderSettings.FieldOfView = double.Parse(FovInput.Text);
            MainRenderer.rs = CurrentRenderSettings;
        }

        private void GenerateRender(object sender, RoutedEventArgs e)
        {
            UpdateRenderSettings();
            ResizeBitmap();
            MainRenderer.RenderScene();
            RenderTime.Content = MainRenderer.rs.RenderTime;
        }

        private void OpenRenderWindowcheck(object sender, RoutedEventArgs e)
        {

        }

        private void ResizeBitmap()
        {
            bitmap = new Bitmap(CurrentRenderSettings.ImageWidth, CurrentRenderSettings.ImageHeight);
        }

        private void ImageResX_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentRenderSettings.ImageWidth = int.Parse(ImageResX.Text);
        }

        private void ImageResY_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentRenderSettings.ImageHeight = int.Parse(ImageResY.Text);
        }

        private void FovInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            double f = double.Parse(FovInput.Text);
            CurrentRenderSettings.FieldOfView = (f * Math.PI)/180;
        }
    }
}
