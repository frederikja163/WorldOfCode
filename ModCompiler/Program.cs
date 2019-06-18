using System;
using System.Globalization;
using System.IO;

namespace ModCompiler
{
    /// <summary>
    /// Contains the entry point of the program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The entry point of the program
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Welcome to the official World of Code mod compiler");
            Console.WriteLine("To use this compiler place it in a folder with the mod you want compiled");
            Console.WriteLine("Then either type \"Compile\" or \"DeCompile\" to compile/make a mod or decompile/reconstruct a mod");
            Console.WriteLine();
            Console.WriteLine("Waiting for user input: ");
            string s = Console.ReadLine();
            if (s.ToLower() == "compile")
            {
                Console.WriteLine("Compiling all mods into one file");
                ModCompiler.Compile(Directory.GetCurrentDirectory());
            }
            else if (s.ToLower() == "decompile")
            {
                Console.WriteLine("Decompiling all mods into their respective folders");
                ModCompiler.DeCompile(Directory.GetCurrentDirectory());
            }
        }
    }
}