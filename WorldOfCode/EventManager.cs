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
        /// Called every frame when it is updated
        /// </summary>
        public delegate void UpdateEvent();
        /// <summary>
        /// Called every frame when it is updated
        /// </summary>
        public static UpdateEvent Update = () => { };
        /// <summary>
        /// Called every frame when it is rendered
        /// </summary>
        public delegate void DrawEvent();
        /// <summary>
        /// Called every frame when it is rendered
        /// </summary>
        public static DrawEvent Draw = () => { };

        /// <summary>
        /// Called when a key is pressed
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        public delegate void KeyPressEvent(KeyPressEventArgs e);
        /// <summary>
        /// Called when a keyboard key is pressed
        /// </summary>
        public static KeyPressEvent KeyPress = (e) => { };

        /// <summary>
        /// Called when a key is released
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        public delegate void KeyReleaseEvent(KeyboardKeyEventArgs e);
        /// <summary>
        /// Called when a keyboard key is released
        /// </summary>
        public static KeyReleaseEvent KeyRelease = (e) => { };
        
        /// <summary>
        /// Called when a key is held for more than one frame
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        public delegate void KeyHoldEvent(KeyboardKeyEventArgs e);
        /// <summary>
        /// Called when a keyboard key is held for more than one frame
        /// </summary>
        public static KeyHoldEvent KeyHold = (e) => { };
    }
}