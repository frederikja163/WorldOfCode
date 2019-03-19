using System;
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
        /// Should only really create the window once TODO: add opentk and rendering support
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.Init(LogSeverity.Message);
            EcsManager.Init();
            
            Console.ReadKey();
        }
    }
}