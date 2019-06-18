using System.Drawing;
using System.IO;
using ModCompiler.Compiled;
using ModCompiler.Compilers;
using OpenTK;
using OpenTK.Graphics;

namespace WorldOfCode.Modding
{
    /// <summary>
    /// Loads mods and manages the order and priority of mods
    /// </summary>
    public static class ModLoader
    {
        public static Biome[] biomes;
        /// <summary>
        /// Loads all mods and puts them into memory
        /// </summary>
        public static void Init()
        {
            biomes = BiomeCompiler.CompiledFromString(File.ReadAllText("mods/vanilla.woc"));
        }
        
        /// <summary>
        /// Gets the biome from the mod files
        /// </summary>
        /// <param name="humidity">The humidity of the biome</param>
        /// <param name="temperature">The temperature of the biome</param>
        /// <returns>The biome containing the information</returns>
        public static Biome GetBiome(float humidity, float temperature)
        {
            for (int i = 0; i < biomes.Length; i++)
            {
                Biome b = biomes[i];
                if (b.boundary.Contains(new Vector2(humidity, temperature)))
                {
                    return b;
                }
            }
            Logger.FatalError($"No biome found at {humidity} humidity and {temperature} temperature");
            return new Biome();
        }
    }
}