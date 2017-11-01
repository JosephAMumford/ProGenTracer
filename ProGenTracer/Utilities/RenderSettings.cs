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
        /// <summary>
        /// Width in pixels of rendered image
        /// </summary>
        public int ImageWidth;
        /// <summary>
        /// Height in pixels of rendered image
        /// </summary>
        public int ImageHeight;
        /// <summary>
        /// Camera field of view.  Put in camera
        /// when that is done
        /// </summary>
        public double FieldOfView;
        /// <summary>
        /// How many rays can be cast for reflections
        /// </summary>
        public int MaxDepth;

        public double Bias;
        /// <summary>
        /// Default background color
        /// </summary>
        public Color BackgroundColor = Color.Background;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public RenderSettings()
        {

        }
    }
}
