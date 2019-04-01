namespace WorldOfCode
{
    /// <summary>
    /// Component for the input of the player
    /// </summary>
    public class PlayerInput : Component
    {
        /// <summary>
        /// The movement speed of the player
        /// </summary>
        public float MovementSpeed { get; set; }
        /// <summary>
        /// The mouse sensitivity of the player
        /// </summary>
        public float MouseSensitivity { get; set; }


        /// <summary>
        /// Create a playerInput
        /// </summary>
        /// <param name="movementSpeed">The movement speed of the player</param>
        /// <param name="mouseSensitivity">The mouse sensitivity of the player</param>
        public PlayerInput(float movementSpeed = 1.5f, float mouseSensitivity = 0.01f)
        {
            MovementSpeed = movementSpeed;
            MouseSensitivity = mouseSensitivity;
        }
    }
}