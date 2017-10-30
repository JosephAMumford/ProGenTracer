//Created by Joseph Mumford 10/21/2017
//This file is part of ProGen Tracer which is released under MIT License.  See license.txt for full details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Utilities
{
    public class RenderSettings
    {
        //Public Variables
        public int ImageWidth;
        public int ImageHeight;
        public double FieldOfView;
        public int MaxDepth;
        public double Bias;
        public Color BackgroundColor = Color.Background;


        /// <summary>
        /// Empty Constructor
        /// </summary>
        public RenderSettings()
        {

        }
    }
}
