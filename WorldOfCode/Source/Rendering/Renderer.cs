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
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 1);
        }

        /// <summary>
        /// Manages the entire drawing sequence, from clearing to drawing to swapping the buffers
        /// </summary>
        /// <param name="context">Context to swap after drawn</param>
        public static void DrawEverything(IGraphicsContext context)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            EventManager.Draw.Invoke();
            
            context.SwapBuffers();
        }

        /// <summary>
        /// Draw a single element to the screen
        /// </summary>
        /// <param name="vao">Vertex array object to draw to the screen</param>
        /// <param name="indices">Amount of indices to draw to the screen</param>
        /// <param name="offset">Where to start drawing the indices from</param>
        public static void Draw(VertexArray vao, int indices, int offset = 0)
        {
            vao.Bind();
            GL.DrawElements(BeginMode.Triangles, indices, DrawElementsType.UnsignedInt, offset);
        }
    }
}