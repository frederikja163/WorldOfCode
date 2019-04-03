using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// A index buffer holds the indices for the vertices/the order to draw the vertices in
    /// </summary>
    public class IndexBuffer : BufferObject
    {
        /// <summary>
        /// The amount of indices
        /// </summary>
        public int Count { get; private set; }
        
        /// <summary>
        /// Initialize the index buffer
        /// </summary>
        /// <param name="data">The order of the vertices to draw</param>
        /// <param name="hint">A hint to the GPU about how the data is going to be used</param>
        public void Init(uint[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw)
        {
            Count = data.Length;
            base.Init(BufferTarget.ElementArrayBuffer, data, hint);
        }
    }
}