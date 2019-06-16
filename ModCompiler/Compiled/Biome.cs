using OpenTK;
using OpenTK.Graphics;

namespace ModCompiler.Compiled
{
    /// <summary>
    /// A biome inside WOC
    /// </summary>
    public struct Biome
    {
        /// <summary>
        /// Defines typography for a biome
        /// </summary>
        public struct TypographyInfo
        {
            /// <summary>
            /// The amplitude of hills in the biome
            /// </summary>
            public float Amplitude { get; set; }
            /// <summary>
            /// The frequency of hills in the biome
            /// </summary>
            public float Frequency { get; set; }
            /// <summary>
            /// The minimum height of the biome
            /// </summary>
            public float MinHeight { get; set; }
        }
        
        /// <summary>
        /// Defines the typography for the biome
        /// </summary>
        public TypographyInfo Typography { get; set; }

        /// <summary>
        /// The color of the biome
        /// </summary>
        public Color4 Color { get; set; }
        
        /// <summary>
        /// The minimum position for the biome
        /// </summary>
        public Vector2 Min { get; set; }
        
        /// <summary>
        /// The maximum position for the biome
        /// </summary>
        public Vector2 Max { get; set; }
    }
}