using System;
using System.Drawing;
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
            float humidity, temperature;
            void GetHumidityAndTemperature(float xOffset, float yOffset)
            {
                humidity = MathHelper.Clamp(_humidity.GetPerlin(x + xOffset, y + yOffset) + 0.5f, 0f, 1f);
                temperature = MathHelper.Clamp(_temperature.GetPerlin(x + xOffset, y + yOffset) + 0.5f, 0f, 1f);
            }
            GetHumidityAndTemperature(0, 0);
            Biome biome = ModLoader.GetBiome(humidity, temperature);
            
            //Set the data for the vertex
            float GetYPosition(Biome bio, float xOffset, float yOffset)
            {
                _height.SetFrequency(bio.Topology.Frequency);
                return (_height.GetPerlin(x + xOffset, y + yOffset) + 0.5f) * bio.Topology.Amplitude + bio.Topology.MinHeight;
            }
            vertex.Position = new Vector3(x, GetYPosition(biome, 0, 0), y);
            vertex.Color = biome.Color;
            
            //Check for biome blending
            void BlendBiomes(float xOffSet, float yOffset)
            {
                //Step 1: Check if it is still the same biome
                GetHumidityAndTemperature(xOffSet, yOffset);
                if (biome.boundary.Contains(new Vector2(humidity, temperature)))
                {
                    return;
                }
                
                //Step 2: Get the new biome
                Biome b = ModLoader.GetBiome(humidity, temperature);
                
                //Step 3: Blend the biomes
                vertex.Position -= Vector3.UnitY * (vertex.Position.Y - GetYPosition(b, xOffSet, yOffset)) / 2f;
//                vertex.Color = Color4.FromHsv(Color4.ToHsv(biome.Color) + (Color4.ToHsv(biome.Color) - Color4.ToHsv(b.Color) / 2f));
            }
            
            BlendBiomes(10, 0);
            BlendBiomes(-10, 0);
            BlendBiomes(0, 10);
            BlendBiomes(0, -10);
            
            return vertex;
        }
    }
}