using OpenTK;
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
            GL.ClearColor(0.2f, .3f, .8f, 1);
        }

        /// <summary>
        /// Manages the entire drawing scene, from clearing to drawing to swapping the buffers
        /// </summary>
        /// <param name="context">Context to swap after drawn</param>
        public static void DrawEvent(IGraphicsContext context)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            EventManager.Draw.Invoke();
            
            context.SwapBuffers();
        }

        /// <summary>
        /// Draw an array of triangles
        /// </summary>
        /// <param name="vao">Data for drawing the triangles</param>
        /// <param name="indices">Amount of indices to draw</param>
        /// <param name="offset">Offset of where to start drawing from</param>
        public static void DrawTriangle(VertexArray vao, int indices = -1, int offset = 0)
        {
            Draw(PrimitiveType.Triangles, vao, indices, offset);
        }
        
        /// <summary>
        /// Draw an array of points
        /// </summary>
        /// <param name="vao">Data for drawing the lines</param>
        /// <param name="indices">Amount of indices to draw</param>
        /// <param name="offset">Offset of where to start drawing from</param>
        public static void DrawLines(VertexArray vao, int indices = -1, int offset = 0)
        {
            Draw(PrimitiveType.Lines, vao, indices, offset);
        }
        
        /// <summary>
        /// Draw an array of points
        /// </summary>
        /// <param name="vao">Data for drawing the points</param>
        /// <param name="indices">Amount of indices to draw</param>
        /// <param name="offset">Offset of where to start drawing from</param>
        public static void DrawPoint(VertexArray vao, int indices = -1, int offset = 0)
        {
            Draw(PrimitiveType.Points, vao, indices, offset);
        }

        /// <summary>
        /// Directly control the draw call of openGL
        /// </summary>
        /// <param name="primitive">The primitive type to draw</param>
        /// <param name="vao">Vertex array object to draw to the screen</param>
        /// <param name="indices">Amount of indices to draw to the screen, set to -1 to draw all</param>
        /// <param name="offset">Where to start drawing the indices from</param>
        private static void Draw(PrimitiveType primitive, VertexArray vao, int indices, int offset)
        {
            if (indices == -1)
            {
                indices = vao.IndexCount;
            }
            
            vao.Bind();
            //TODO: Support non index based draw calls
            GL.DrawElements(primitive, indices, DrawElementsType.UnsignedInt, offset);
        }
    }
}