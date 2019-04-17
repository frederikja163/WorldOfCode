using OpenTK.Graphics;

namespace WorldOfCode.Modding
{
    /// <summary>
    /// Defines a biome and all the data needed to define a biome
    /// </summary>
    public struct Biome
    {
        /// <summary>
        /// Defines typography for a biome
        /// </summary>
        public struct Typography
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
        public Typography Hill { get; set; }

        /// <summary>
        /// The color of the biome
        /// </summary>
        public Color4 Color { get; set; }
    }
}