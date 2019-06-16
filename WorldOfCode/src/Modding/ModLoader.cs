using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace WorldOfCode.Modding
{
    /// <summary>
    /// Loads mods and manages the order and priority of mods
    /// </summary>
    public static class ModLoader
    {
        /// <summary>
        /// Loads all mods and puts them into memory
        /// </summary>
        public static void Init()
        {
            
        }
        
        /// <summary>
        /// Gets the biome from the mod files
        /// </summary>
        /// <param name="humidity">The humidity of the biome</param>
        /// <param name="temperature">The temperature of the biome</param>
        /// <returns>The biome containing the information</returns>
        public static Biome GetBiome(float humidity, float temperature)
        {
            //Make the return value
            Biome biome = new Biome();
            
//            //Calculate the x and the y
//            int x = (int) (humidity * (_biomeColor.Width - 1));
//            int y = (int) (temperature * (_biomeColor.Height - 1));
//            
//            //Get the color
//            Color color = _biomeColor.GetPixel(x, y);
//            biome.Color = new Color4(color.R, color.G, color.B, color.A);
//            
//            //Get the typography
//            Color typography = _biomeTypography.GetPixel(x, y);

            return biome;
        }
    }
}