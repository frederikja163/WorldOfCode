using OpenTK;
using OpenTK.Input;

namespace WorldOfCode
{
    /// <summary>
    /// Manages the players movement
    /// </summary>
    public class PlayerMovement : BaseSystem
    {
        /// <summary>
        /// The last position for the mouse
        /// </summary>
        private Vector2 _lastMousePos;
        
        /// <inheritdoc />
        /// <summary>
        /// This system needs all entities with a Camera and PlayerInput component
        /// </summary>
        protected override bool IsValidEntity(Entity entity)
        {
            return entity.GetComponent<Camera>() != null && entity.GetComponent<PlayerInput>() != null;
        }

        /// <summary>
        /// Subscribe to the needed events
        /// </summary>
        public override void Init()
        {
            //This system needs the update event
            EventManager.Update += Update;
            EventManager.FocusChanged += FocusChanged;
            EventManager.MouseMove += MouseMove;
            
            CenterMouse();
            Window.IsCursorVisible = !Window.IsFocused;
            
            MouseState mouseInput = Mouse.GetState();
            _lastMousePos = new Vector2(mouseInput.X, mouseInput.Y);
        }

        /// <summary>
        /// Center the mouse
        /// TODO: This should be placed elsewhere (probs in the input system)
        /// </summary>
        private void CenterMouse()
        {
            Vector2 center = Window.WindowPosition + Window.WindowSize / 2;
            Mouse.SetPosition(center.X, center.Y);
        }
        
        /// <summary>
        /// Called when the mouse is moved
        /// </summary>
        /// <param name="e">e contains the arguments of the event</param>
        private void MouseMove(MouseMoveEventArgs e)
        {
            if (Window.IsFocused)
            {
                CenterMouse();
            }
        }

        /// <summary>
        /// Called when the focus is changed
        /// </summary>
        private void FocusChanged()
        {
            Window.IsCursorVisible = !Window.IsFocused;
        }

        /// <summary>
        /// Called on the update event
        /// </summary>
        private void Update()
        {
            //Return if the window is not focused
            if (!Window.IsFocused)
            {
                return;
            }
            
            KeyboardState keyboardInput = Keyboard.GetState();
            MouseState mouseInput = Mouse.GetState();

            if (keyboardInput.IsKeyDown(Key.Escape))
            {
                Window.Close();
            }
            
            for (int i = 0; i < Entities.Count; i++)
            {
                Camera camera = Entities[i].GetComponent<Camera>();
                PlayerInput playerInput = Entities[i].GetComponent<PlayerInput>();
                
                //TODO: implement custom input system
                //Movement
                if (keyboardInput.IsKeyDown(Key.W))
                { camera.Position += camera.Front * playerInput.MovementSpeed * Time.DeltaTime; } //Forward 
                if (keyboardInput.IsKeyDown(Key.S))
                { camera.Position -= camera.Front * playerInput.MovementSpeed  * Time.DeltaTime; } //Backwards
                if (keyboardInput.IsKeyDown(Key.A))
                { camera.Position += camera.Right * playerInput.MovementSpeed  * Time.DeltaTime; }//Left
                if (keyboardInput.IsKeyDown(Key.D))
                { camera.Position -= camera.Right * playerInput.MovementSpeed  * Time.DeltaTime; } //Right
                if (keyboardInput.IsKeyDown(Key.Space))
                { camera.Position += camera.Up * playerInput.MovementSpeed  * Time.DeltaTime; } //Up
                if (keyboardInput.IsKeyDown(Key.LShift))
                { camera.Position -= camera.Up * playerInput.MovementSpeed  * Time.DeltaTime; } //Down
                
                //Look around
                Vector2 mousePosition = new Vector2(mouseInput.X, mouseInput.Y);
                Vector2 delta = mousePosition - _lastMousePos;
                _lastMousePos = mousePosition;

                camera.Yaw += delta.X * playerInput.MouseSensitivity;
                camera.Pitch -= delta.Y * playerInput.MouseSensitivity;
            }
        }
    }
}