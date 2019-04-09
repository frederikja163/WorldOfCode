using OpenTK;
using OpenTK.Graphics;

namespace WorldOfCode
{
    /// <summary>
    /// Handles the noise generation for both the height and color of each point
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// The noise used to generate the map
        /// </summary>
        private static FastNoise _noise;

        /// <summary>
        /// Initialize the map generator
        /// </summary>
        public static void Init()
        {
            _noise = new FastNoise();
            _noise.SetFrequency(0.05f);
        }

        /// <summary>
        /// Generate the terrain vertex information based on the position
        /// </summary>
        /// <param name="x">The x position of the vertex</param>
        /// <param name="y">The y position of the vertex</param>
        /// <returns>The information for the terrain vertex</returns>
        public static TerrainVertex GetVertex(float x, float y)
        {
            //Create the vertex
            TerrainVertex vertex = new TerrainVertex();
            
            //Set the data for the vertex
            vertex.Position = new Vector3(x, _noise.GetPerlin(x, y) * 5, y);
            vertex.Color = Color4.Green; //TODO: Base this on a texture loaded (for modding purposes)
            
            return vertex;
        }
    }
}