//Created by Joseph Mumford 10/29/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Rendering
{
    public class Texture
    {
        //Public Variables
        /// <summary>
        /// Width of texture
        /// </summary>
        public int Width;
        /// <summary>
        /// Height of texture
        /// </summary>
        public int Height;
        /// <summary>
        /// Array of colors representing texture pixels
        /// </summary>
        public Color[] PixelMap;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Texture()
        {
        }
        /// <summary>
        /// Create texture with width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Texture(int width, int height)
        {
            Width = width;
            Height = height;
            InitializePixelMap();
        }
        /// <summary>
        /// Create texture with width and height and set with pixel map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pixels"></param>
        public Texture(int width, int height, Color[] pixels)
        {
            Width = width;
            Height = height;
            SetPixels(pixels);
        }

        //Public Functions
        /// <summary>
        /// Initialize pixel map
        /// </summary>
        public void InitializePixelMap()
        {
            int size = Width * Height;
            PixelMap = new Color[size];

            for(int i = 0; i < size; i++)
            {
                PixelMap[i] = new Color();
            }
        }
        /// <summary>
        /// Set pixel at x and y with color
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            PixelMap[y * Width + x] = color;
        }
        /// <summary>
        /// Set all pixels of texture with pixel array
        /// </summary>
        /// <param name="pixels"></param>
        public void SetPixels(Color[] pixels)
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    PixelMap[y * Width + x] = new Color();
                    PixelMap[y * Width + x] = pixels[y * Width + x];
                }
            }
        }
        /// <summary>
        /// Get pixel at x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            return PixelMap[y * Width + x];
        }
        /// <summary>
        /// Get all pixels in texture
        /// </summary>
        /// <returns></returns>
        public Color[] GetPixels()
        {
            return PixelMap;
        }
    }
}
