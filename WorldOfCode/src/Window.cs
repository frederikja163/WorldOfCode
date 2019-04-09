using System;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
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
        {
            _instance = this;
        }

        /// <summary>
        /// Load the game and initialize what needs to be initialized
        /// </summary>
        /// <param name="e">e contains the argument(s) of the function</param>
        protected override void OnLoad(EventArgs e)
        {
            //Initialize stuff
            Map.Init();
            EcsManager.Init();

            base.OnLoad(e);
        }

        #region StaticWindow
        /// <summary>
        /// The instance of the window to be used as static
        /// </summary>
        private static Window _instance;

        /// <summary>
        /// Is the cursor visible
        /// </summary>
        public static bool IsCursorVisible
        {
            get => _instance.CursorVisible;
            set => _instance.CursorVisible = value;
        }
        /// <summary>
        /// Is the window focused
        /// </summary>
        public static bool IsFocused => _instance.Focused;
        /// <summary>
        /// The window size with the X component being the width and the Y component being the height
        /// </summary>
        public static Vector2 WindowSize => new Vector2(_instance.Width, _instance.Height);
        /// <summary>
        /// The window position in vector form
        /// </summary>
        public static Vector2 WindowPosition => new Vector2(_instance.X, _instance.Y);
        /// <summary>
        /// Close the window and shut the application down
        /// </summary>
        public new static void Close() => _instance.Exit();
        #endregion StaticWindow
        
        #region Events
        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="e">e contains the argument(s) of the function</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Time.DeltaTime = (float)e.Time;
            EventManager.Update.Invoke();
            base.OnUpdateFrame(e);
        }
        /// <summary>
        /// Called when a frame needs to be drawn
        /// </summary>
        /// <param name="e">e contains the argument(s) of the function</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {

            Renderer.DrawEvent(Context);
            
            base.OnRenderFrame(e);
        }
        /// <summary>
        /// Called when the program disposes and unloads all resources
        /// </summary>
        /// <param name="e">e contains the argument(s) of the function</param>
        protected override void OnUnload(EventArgs e)
        {
            EventManager.Dispose.Invoke();
            base.OnUnload(e);
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
        
        /// <summary>
        /// Called when the focus changes focus mode, ie. from unfocused to focused or vice versa
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFocusedChanged(EventArgs e)
        {
            EventManager.FocusChanged();
            base.OnFocusedChanged(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            EventManager.MouseMove(e);
            base.OnMouseMove(e);
        }

        #endregion Events
    }
}