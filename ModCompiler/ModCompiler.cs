using System.IO;
using ModCompiler.Compilers;

namespace ModCompiler
{
    /// <summary>
    /// Compiles and decompiles mods
    /// </summary>
    public static class ModCompiler
    {
        /// <summary>
        /// Will compile all files in a folder and in the sub folders into a single mod
        /// </summary>
        /// <param name="path">The path of the folder</param>
        public static void Compile(string path)
        {
            //The string that will be filled with the entire mod to be written at last
            string mod = "";
            
            //compile the biomes
            Decompiled.Biome[] decompiled = BiomeCompiler.DecompiledFromString(File.ReadAllText(Path.Combine(path, "biomes.json")));
            Compiled.Biome[] compiled = BiomeCompiler.Compile(decompiled);
            mod += BiomeCompiler.CompiledToString(compiled);
            
            //Write the new mod
            File.WriteAllText(Path.Combine(path, "unnamed.woc"), mod);
        }

        /// <summary>
        /// Will decompile all mods in a folder and in the sub folders and put them into their respective folders
        /// </summary>
        /// <param name="path">The path of the folder</param>
        public static void DeCompile(string path)
        {
            //compile the biomes
            Compiled.Biome[] compiled = BiomeCompiler.CompiledFromString(LoadAllMods(path));
            Decompiled.Biome[] decompiled = BiomeCompiler.Decompile(compiled);
            File.WriteAllText(Path.Combine(path, "biomes.json"), BiomeCompiler.DecompiledToString(decompiled));
        }

        /// <summary>
        /// Loads all mods in a single directory
        /// </summary>
        /// <param name="path">The path of the directory to load mods from</param>
        /// <returns>A string with all the mods combined</returns>
        private static string LoadAllMods(string path)
        {
            string modCollection = "";
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].EndsWith(".woc"))
                {
                    continue;
                }
                modCollection += File.ReadAllText(files[i]);
            }
            return modCollection;
        }
    }
}