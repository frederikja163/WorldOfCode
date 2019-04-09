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
        /// A single noise layer
        /// </summary>
        private struct NoiseLayer
        {
            /// <summary>
            /// The frequency of the noise layer
            /// </summary>
            public float Frequency { get; }
            /// <summary>
            /// The amplitude of the noise layer (the multiplier)
            /// </summary>
            public float Amplitude { get; }

            /// <summary>
            /// Create a noise layer
            /// </summary>
            /// <param name="frequency">The frequency of the noise layer</param>
            /// <param name="amplitude">The amplitude of the noise layer (the multiplier)</param>
            public NoiseLayer(float frequency, float amplitude)
            {
                Frequency = frequency;
                Amplitude = amplitude;
            }
        }
        /// <summary>
        /// The different layers of noise that will be stacked on top of each other to create a more wholesome feel
        /// </summary>
        private static NoiseLayer[] NoiseLayers => new NoiseLayer[]
        {
            new NoiseLayer(0.0025f, 25),
            new NoiseLayer(0.01f, 5), 
        };
        
        /// <summary>
        /// The noise used to generate the height map
        /// </summary>
        private static FastNoise _height;

        /// <summary>
        /// The noise used for the x-axis of biome generation
        /// </summary>
        private static FastNoise _humidity;
        /// <summary>
        /// The noise used for the y-axis of biome generation
        /// </summary>
        private static FastNoise _temperature;
        

        /// <summary>
        /// Initialize the map generator
        /// </summary>
        public static void Init()
        {
            _height = new FastNoise(1);
            
            _humidity = new FastNoise(2);
            _temperature = new FastNoise(3);
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
            vertex.Position = new Vector3(x, GetNoise(x, y, _height), y);
            vertex.Color = Color4.Green; //TODO: Base this on a texture loaded (for modding purposes)
            
            return vertex;
        }

        /// <summary>
        /// Generate noise based on the position from multiple different frequenzies
        /// </summary>
        /// <param name="x">The x position of the noise</param>
        /// <param name="y">The y position of the noise</param>
        /// <param name="noise">The noise to use when generating the noise</param>
        /// <returns>The value gotten from the noise function</returns>
        private static float GetNoise(float x, float y, FastNoise noise)
        {
            float value = 0;
            for (int i = 0; i < NoiseLayers.Length; i++)
            {
                noise.SetFrequency(NoiseLayers[i].Frequency);
                value += noise.GetPerlin(x, y) * NoiseLayers[i].Amplitude;
            }

            return value;
        }
    }
}