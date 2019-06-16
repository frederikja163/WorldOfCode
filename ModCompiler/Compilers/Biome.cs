using System.Collections.Generic;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;

namespace ModCompiler.Compilers
{
    /// <summary>
    /// Compiles and decompiles biomes
    /// </summary>
    public static class Biome
    {
        
        /// <summary>
        /// Gets compiled biomes based on a string
        /// </summary>
        /// <param name="str">String to get information about biomes from</param>
        /// <returns>An array containing all the biomes contained in the string</returns>
        public static Compiled.Biome[] CompiledFromString(string str)
        {
            List<Compiled.Biome> biomes = new List<Compiled.Biome>();
            string[] lines = str.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                //Check for type
                string s = lines[i];
                if (!s.StartsWith("[b:"))
                {
                    continue;
                }
                //Get all attributes
                s = s.Remove(s.Length - 1);
                s = s.Remove(0, 3);
                string[] attributes = s.Split(',');
                
                //Convert attributes
                byte attrib = 0;
                float NextAttrib()
                {
                    return float.Parse(attributes[attrib++]);
                }
                
                biomes.Add(new Compiled.Biome()
                {
                    Color = new Color4(NextAttrib(), NextAttrib(), NextAttrib(), NextAttrib()),
                    Min = new Vector2(NextAttrib(), NextAttrib()),
                    Max = new Vector2(NextAttrib(), NextAttrib()),
                    Typography = new Compiled.Biome.TypographyInfo()
                    {
                        Amplitude = NextAttrib(),
                        Frequency = NextAttrib(),
                        MinHeight = NextAttrib()
                    }
                });
            }

            return biomes.ToArray();
        }
        
        /// <summary>
        /// Gets decompiled biomes based on a string
        /// </summary>
        /// <param name="str">String to get information about biomes from</param>
        /// <returns>An array containing all the biomes contained in the string</returns>
        public static Decompiled.Biome[] DecompiledFromString(string str)
        {
            return JsonConvert.DeserializeObject<Decompiled.Biome[]>(str);
        }
        
        /// <summary>
        /// Compiles an array of decompiled biomes and returns their compiled variants
        /// </summary>
        /// <param name="biomes">An array containing decompiled biomes to be compiled</param>
        /// <returns>An array containing the compiled variants of the biomes, in the same order</returns>
        public static Compiled.Biome[] Compile(Decompiled.Biome[] biomes)
        {
            Compiled.Biome[] converted = new Compiled.Biome[biomes.Length];
            for (int i = 0; i < biomes.Length; i++)
            {
                Decompiled.Biome b = biomes[i];
                converted[i] = new Compiled.Biome()
                {
                    Color = biomes[i].Color,
                    Typography = new Compiled.Biome.TypographyInfo()
                    {
                        Amplitude = b.Typography.Amplitude,
                        Frequency = b.Typography.Frequency,
                        MinHeight = b.Typography.Amplitude
                    },
                    Min = new Vector2()
                    {
                        X = b.Humidity.Min,
                        Y = b.Temperature.Min
                    },
                    Max = new Vector2()
                    {
                        X = b.Humidity.Max,
                        Y = b.Temperature.Max
                    }
                };
            }

            return converted;
        }

        /// <summary>
        /// Decompiles an array of compiled biomes and returns their compiled variants
        /// </summary>
        /// <param name="biomes">An array containing compiled biomes to be decompiled</param>
        /// <returns>An array containing the decompiled variants of the biomes, in the same order</returns>
        public static Decompiled.Biome[] Decompile(Compiled.Biome[] biomes)
        {
            Decompiled.Biome[] converted = new Decompiled.Biome[biomes.Length];
            for (int i = 0; i < biomes.Length; i++)
            {
                Compiled.Biome b = biomes[i];
                converted[i] = new Decompiled.Biome()
                {
                    Color = biomes[i].Color,
                    Typography = new Decompiled.Biome.TypographyInfo()
                    {
                        Amplitude = b.Typography.Amplitude,
                        Frequency = b.Typography.Frequency,
                        MinHeight = b.Typography.Amplitude
                    },
                    Humidity = new Decompiled.Biome.BoundaryInfo()
                    {
                        Min = b.Min.X,
                        Max = b.Max.X
                    },
                    Temperature = new Decompiled.Biome.BoundaryInfo()
                    {
                        Min = b.Min.Y,
                        Max = b.Max.Y
                    }
                };
            }

            return converted;
        }

        /// <summary>
        /// Gets a string with information about an array of compiled biomes
        /// </summary>
        /// <param name="biomes">An array of compiled biomes to construct the string out of</param>
        /// <returns>A string containing information about all the biomes</returns>
        public static string CompiledToString(Compiled.Biome[] biomes)
        {
            string returnVal = "";
            for (int i = 0; i < biomes.Length; i++)
            {
                Compiled.Biome b = biomes[i];
                returnVal += $"[b:{b.Color.R},{b.Color.G},{b.Color.B},{b.Color.A},{b.Min.X},{b.Min.Y},{b.Max.X},{b.Max.Y}," + 
                             $"{b.Typography.Amplitude},{b.Typography.Frequency},{b.Typography.MinHeight}]\n";
            }
            return returnVal;
        }

        /// <summary>
        /// Gets a string with information about an array of decompiled biomes
        /// </summary>
        /// <param name="biomes">An array of decompiled biomes to construct the string out of</param>
        /// <returns>A string containing information about all the biomes</returns>
        public static string DecompiledToString(Decompiled.Biome[] biomes)
        {
            return JsonConvert.SerializeObject(biomes, Formatting.Indented);
        }
    }
}