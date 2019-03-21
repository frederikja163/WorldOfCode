using System;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

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

        #region Events
        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            EventManager.Update();
            base.OnUpdateFrame(e);
        }
        
        /// <summary>
        /// Called when a key is pressed
        /// </summary>
        /// <param name="e">e contains the argument of the event</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            EventManager.KeyPress(e);
            base.OnKeyPress(e);
        }
        /// <summary>
        /// Called when a key is released
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            EventManager.KeyHold(e);
            base.OnKeyDown(e);
        }
        /// <summary>
        /// Called when a key is held for more than one frame
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            EventManager.KeyRelease(e);
            base.OnKeyUp(e);
        }
        #endregion Events
    }
}