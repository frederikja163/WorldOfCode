using OpenTK;
using OpenTK.Graphics;

namespace ModCompiler.Decompiled
{
    /// <summary>
    /// A biome outside WOC
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
        /// Defines boundary for a biome
        /// </summary>
        public struct BoundaryInfo
        {
            /// <summary>
            /// The minimum accepted part of a biome
            /// </summary>
            public float Min { get; set; }
            /// <summary>
            /// The maximum accepted part of a biome
            /// </summary>
            public float Max { get; set; }
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
        /// The humidity boundary for the biome
        /// </summary>
        public BoundaryInfo Humidity { get; set; }
        
        /// <summary>
        /// The temperature boundary for the biome
        /// </summary>
        public BoundaryInfo Temperature { get; set; }
    }
}