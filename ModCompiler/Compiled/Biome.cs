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
        /// Defines topology for a biome
        /// </summary>
        public struct TopologyInfo
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
        /// Defines the topology for the biome
        /// </summary>
        public TopologyInfo Topology { get; set; }

        /// <summary>
        /// The color of the biome
        /// </summary>
        public Color4 Color { get; set; }
        
        /// <summary>
        /// The boundary of the biome
        /// </summary>
        public Box2 boundary { get; set; }
    }
}