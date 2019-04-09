using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// A vertex buffer holds the information of the vertices
    /// </summary>
    public class VertexBuffer : BufferObject
    {
        /// <summary>
        /// Initialize the vertex buffer
        /// </summary>
        /// <param name="data">The data for the vertex buffer</param>
        /// <param name="hint">A hint to the GPU about how the data is going to be used</param>
        public void Init<TDataType>(TDataType[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw)
            where TDataType : unmanaged
        {
            base.Init(BufferTarget.ArrayBuffer, data, hint);
        }
    }
}