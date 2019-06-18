using System;
using ModCompiler.Compiled;
using OpenTK;
using OpenTK.Graphics;
using WorldOfCode.Modding;

namespace WorldOfCode
{
    /// <summary>
    /// Handles the noise generation for both the height and color of each point
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// The noise used for the x-axis of biome generation
        /// </summary>
        private static FastNoise _humidity;
        /// <summary>
        /// The noise used for the y-axis of biome generation
        /// </summary>
        private static FastNoise _temperature;

        /// <summary>
        /// The noise used for the height of biome generation
        /// </summary>
        private static FastNoise _height;

        /// <summary>
        /// Initialize the map generator
        /// </summary>
        public static void Init()
        {
            _height = new FastNoise(1);
            _humidity = new FastNoise(2);
            _temperature = new FastNoise(3);
            
            _humidity.SetFrequency(0.005f);
            _temperature.SetFrequency(0.005f);
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
            
            //Get the biome
            float humidity = MathHelper.Clamp(_humidity.GetPerlin(x, y) + 0.5f, 0f, 1f);
            float temperature = MathHelper.Clamp(_temperature.GetPerlin(x, y) + 0.5f, 0f, 1f);
            Biome biome = ModLoader.GetBiome(humidity, temperature);
            
            //Set the data for the vertex
            _height.SetFrequency(biome.Typography.Frequency);
            vertex.Position = new Vector3(x, (_height.GetPerlin(x, y) + 0.5f) * biome.Typography.Amplitude + biome.Typography.MinHeight, y);
            vertex.Color = biome.Color;
            
            return vertex;
        }
    }
}