using OpenTK;
using OpenTK.Graphics;

namespace WorldOfCode
{
    /// <summary>
    /// Holds all the information for a single vertex
    /// </summary>
    public struct TerrainVertex
    {
        /// <summary>
        /// The position of the vertex
        /// </summary>
        public Vector3 Position { get; set; }
        /// <summary>
        /// The color of the vertex
        /// </summary>
        public Color4 Color { get; set; }
    }
}