using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// Manages rendering, draw calls and as an interface to openTK
    /// </summary>
    public static class Renderer
    {
        /// <summary>
        /// Initialize the openTK renderer
        /// </summary>
        public static void Init()
        {
            GL.ClearColor(0, 0, 0, 1);
        }

        /// <summary>
        /// Manages the entire drawing sequence, from clearing to drawing to swapping the buffers
        /// </summary>
        /// <param name="context">Context to swap after drawn</param>
        public static void Draw(IGraphicsContext context)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            EventManager.Draw.Invoke();
            
            context.SwapBuffers();
        }
    }
}