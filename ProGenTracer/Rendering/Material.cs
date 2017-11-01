using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProGenTracer.Utilities;

namespace ProGenTracer.Rendering
{
    public class Material
    {
        /// <summary>
        /// Name of material
        /// </summary>
        public string Name;
        /// <summary>
        /// Material Type
        /// </summary>
        public int Type;
        /// <summary>
        /// Main color component
        /// </summary>
        public Color MainColor = new Color();
        /// <summary>
        /// Specular color component
        /// </summary>
        public Color SpecularColor = new Color();
        /// <summary>
        /// Specular exponent
        /// </summary>
        public double Specular;
        /// <summary>
        /// Main diffuse texture map
        /// </summary>
        public Texture MainTexture = new Texture();
        /// <summary>
        /// Bump texture map
        /// </summary>
        public Texture BumpTexture = new Texture();
        /// <summary>
        /// Normal texture map
        /// </summary>
        public Texture NormalTexture = new Texture();
    }
}
