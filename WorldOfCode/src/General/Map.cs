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
                humidity = MathHelper.Clamp(_humidity.GetValueFractal(x + xOffset, y + yOffset) + 0.5f, 0f, 1f);
                temperature = MathHelper.Clamp(_temperature.GetValueFractal(x + xOffset, y + yOffset) + 0.5f, 0f, 1f);
            }
            GetHumidityAndTemperature(0, 0);
            Biome biome = ModLoader.GetBiome(humidity, temperature);
            
            //Set the data for the vertex
            float GetYPosition(Biome bio, float xOffset, float yOffset)
            {
                _height.SetFrequency(bio.Topology.Frequency);
                return (_height.GetSimplexFractal(x + xOffset, y + yOffset) + 0.5f) * bio.Topology.Amplitude + bio.Topology.MinHeight;
            }
            vertex.Position = new Vector3(x, GetYPosition(biome, 0, 0), y);
            vertex.Color = biome.Color;
            
            //Check for biome blending
            void BlendBiomes(float xOffset, float yOffset)
            {
                Vector2 checkOffset = new Vector2(xOffset, yOffset);
                float thisY = 0;
                
                //Step 1: Check when the border of the biomes are
                while(xOffset != 0 || yOffset != 0)
                {
                    //Get the humidity and temperature
                    GetHumidityAndTemperature(xOffset, yOffset);
                    //Check if it is within the original biome
                    if (biome.boundary.Contains(new Vector2(humidity, temperature)))
                    {
                        if (xOffset == checkOffset.X && yOffset == checkOffset.Y)
                        {
                            return;
                        }
                        break;
                    }
                    //Move one step closer to no offset
                    xOffset -= xOffset > 0 ? 1 : xOffset < 0 ? -1 : 0;
                    yOffset -= yOffset > 0 ? 1 : yOffset < 0 ? -1 : 0;
                }
                //(We need to put the x- and yOffset one back)
                thisY = GetYPosition(biome, xOffset, yOffset);
                xOffset += checkOffset.X > 0 ? 1 : checkOffset.X < 0 ? -1 : 0;
                yOffset += checkOffset.Y > 0 ? 1 : checkOffset.Y < 0 ? -1 : 0;

                
                //Step 2: Get the new biome
                GetHumidityAndTemperature(xOffset, yOffset);
                Biome b = ModLoader.GetBiome(humidity, temperature);
                
                //Step 3: Calculate the blend proportion
                float blendProportion = 1;
                if (xOffset != checkOffset.X)
                {
                    blendProportion = xOffset / checkOffset.X;
                }

                if (yOffset != checkOffset.Y)
                {
                    blendProportion *= yOffset / checkOffset.Y;
                }
                
                //Step 4: Blend the biomes
                float otherY = GetYPosition(b, xOffset, yOffset);
                Vector3 position = vertex.Position;
                position.Y =  ((otherY - thisY) / 2 + thisY) * (1 - blendProportion) + position.Y * blendProportion;
                vertex.Position = position;
            }
            
            BlendBiomes(10, 0);
            BlendBiomes(-10, 0);
            BlendBiomes(0, 10);
            BlendBiomes(0, -10);
            
            return vertex;
        }
    }
}