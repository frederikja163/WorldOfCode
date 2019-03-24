namespace WorldOfCode
{
    /// <summary>
    /// Defines a component that can be rendered to the screen
    /// </summary>
    public class RenderAble : Component
    {
        /// <summary>
        /// The Vao that will store both the VBO and the IBO of the renderAble
        /// </summary>
        public VertexArray Vao { get; set; }

        /// <summary>
        /// Creates a new renderable
        /// </summary>
        /// <param name="vao">The vertex array object to store and draw</param>
        public RenderAble(VertexArray vao)
        {
            Vao = vao;
        }

        /// <summary>
        /// Dispose the component and get rid of unnecessary resources
        /// </summary>
        public override void Dispose()
        {
            Vao.Dispose();
        }
    }
}