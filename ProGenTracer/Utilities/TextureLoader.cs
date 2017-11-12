using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Rendering;

namespace ProGenTracer.Utilities
{
    /// <summary>
    /// Methods for loading image files to use as textures 
    /// for mesh objects.  Currently supports:
    /// .bmp (24-bit)
    /// </summary>
    public class TextureLoader
    {
        /// <summary>
        /// Load bitmap image with fileName and return as texture
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Texture LoadBitmap(string fileName)
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open);     // Open file stream
            BinaryReader br = new BinaryReader(fileStream);                 // Create binary file reader
            Texture loadedTexture = new Texture();

            // Bitmap information
            char[] bitmapHeader = new char[2];      // File information in BMP
            int bitmapDataPos = 0;                  // Position in file where data begins
            int bitmapWidth = 0;                    // Bitmap image width
            int bitmapHeight = 0;                   // Bitmap image height
            int bitmapSize = 0;                     // Bitmap Width * Bitmap Height * color byte size
            byte[] bitmapData;                      // Pixel data in bitmap image

            bool validBitmap = false;               // Used to determine if file is a valid bitmap image

            // Begin reading file
            bitmapHeader[0] = br.ReadChar();
            bitmapHeader[1] = br.ReadChar();

            // Validate bitmap file
            if (bitmapHeader[0] == 'B' || bitmapHeader[1] == 'M')
            {
                validBitmap = true;

                br.ReadInt32(); //2-5
                br.ReadInt32(); //6-9
                bitmapDataPos = br.ReadInt32(); //10-13
                br.ReadInt32(); //14-17
                bitmapWidth = br.ReadInt32();   //18-21
                bitmapHeight = br.ReadInt32();  //22-25
                br.ReadInt16(); //26-27
                br.ReadInt16(); //28-29
                br.ReadInt32(); //30-33
                bitmapSize = br.ReadInt32();    //34-37
                br.ReadInt32(); //38-41
                br.ReadInt32(); //42-45
                br.ReadInt32(); //46-49
                br.ReadInt32(); //50-53

                // Get pixel data
                bitmapData = new byte[bitmapSize];

                for (int j = 0; j < bitmapSize; j++)
                {
                    bitmapData[j] = br.ReadByte();
                }

                //Create Texture
                loadedTexture = new Texture((int)bitmapWidth, (int)bitmapHeight);
                int dataIndex = 0;
                for (int y = 0; y < bitmapHeight; y++)
                {
                    for (int x = 0; x < bitmapWidth; x++)
                    {
                        double r = (double)(bitmapData[dataIndex + 2] / 255.0);
                        double g = (double)(bitmapData[dataIndex + 1] / 255.0);
                        double b = (double)(bitmapData[dataIndex] / 255.0);
                        Utilities.Color newColor = new Utilities.Color(r, g, b);
                        loadedTexture.SetPixel(x, y, newColor);
                        dataIndex += 3;
                    }

                }
            }

            br.Close();
            fileStream.Close();

            return loadedTexture;
        }
    }
}
