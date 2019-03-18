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
        /// Might also initialize the logger here so it can log how opentk is initialized TODO: add logging system
        /// </summary>
        public static void Main(string[] args)
        {
            ECS.EcsManager.Init();
        }
    }
}