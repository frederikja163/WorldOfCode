using System;
using OpenTK;
using OpenTK.Graphics;
using WorldOfCode.ECS;

namespace WorldOfCode
{
    /// <summary>
    /// The window class is the main class of the game
    /// It 'owns' all the objects and calls all the events
    /// </summary>
    public class Window : GameWindow
    {
        /// <summary>
        /// Create a window from basic parameters
        /// </summary>
        /// <param name="width">Width of the window in pixels</param>
        /// <param name="height">Height of the window in pixels</param>
        /// <param name="title">The title displayed at the top of the window</param>
        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        { }

        /// <summary>
        /// Load the game and initialize what needs to be initialized
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //Initialize stuff
            EcsManager.Init();
            
            base.OnLoad(e);
        }
    }
}