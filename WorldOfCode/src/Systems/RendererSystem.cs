namespace WorldOfCode
{
    /// <summary>
    /// Renders all entities
    /// </summary>
    public class RendererSystem : BaseSystem
    {
        //The shader is only stored here atm as a test
        private Shader _shader = new Shader();
        
        /// <inheritdoc />
        /// <summary>
        /// This system needs all entities with a RenderAble component
        /// </summary>
        protected override bool IsValidEntity(Entity entity)
        {
            return entity.GetComponent<RenderAble>() != null;
        }

        /// <summary>
        /// Initialize the system and subscribe to the events
        /// </summary>
        public override void Init()
        {
            EventManager.Draw += Draw;
            EventManager.Dispose += Dispose;
            _shader.Init("res/basic.shade");
        }

        private void Dispose()
        {
            _shader.Dispose();
        }

        /// <summary>
        /// Draw the entities
        /// </summary>
        private void Draw()
        {
            _shader.Bind();
            _shader.Uniform("u_view", ref Camera.Main.View);
            _shader.Uniform("u_projection", ref Camera.Main.Projection);
            for (int i = 0; i < Entities.Count; i++)
            {
                RenderAble renderAble = Entities[i].GetComponent<RenderAble>();
                Renderer.DrawTriangle(renderAble.Vao, 3);
            }
        }
    }
}