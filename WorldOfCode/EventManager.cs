using System;
using OpenTK;
using OpenTK.Input;

namespace WorldOfCode
{
    /// <summary>
    /// Manages events for the program
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// Called every frame
        /// </summary>
        public delegate void UpdateEvent();
        /// <summary>
        /// Called every frame
        /// </summary>
        public static UpdateEvent Update;

        /// <summary>
        /// Called when a key is pressed
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        public delegate void KeyPressEvent(KeyPressEventArgs e);
        /// <summary>
        /// Called when a keyboard key is pressed
        /// </summary>
        public static KeyPressEvent KeyPress;

        /// <summary>
        /// Called when a key is released
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        public delegate void KeyReleaseEvent(KeyboardKeyEventArgs e);
        /// <summary>
        /// Called when a keyboard key is released
        /// </summary>
        public static KeyReleaseEvent KeyRelease;
        
        /// <summary>
        /// Called when a key is held for more than one frame
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        public delegate void KeyHoldEvent(KeyboardKeyEventArgs e);
        /// <summary>
        /// Called when a keyboard key is held for more than one frame
        /// </summary>
        public static KeyHoldEvent KeyHold;
    }
}