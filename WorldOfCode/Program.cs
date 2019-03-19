using System;
using OpenTK;
using WorldOfCode.ECS;

namespace WorldOfCode
{
    /// <summary>
    /// The entry point of the game
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The entry point of the application
        /// Should only do 2 things
        ///     1. Initialize the logging system
        ///     2. Initialize the openTK window
        /// Should only really create the window TODO: add opentk and rendering support
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.Init(LogSeverity.Message);
            EcsManager.Init();

            using (Window window = new Window(500, 500,"World Of Code"))
            {
                window.Run(60.0f);
            }
            
            Console.ReadKey();
        }
    }
}