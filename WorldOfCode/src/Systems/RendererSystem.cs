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
            return entity.GetComponent<Terrain>() != null;
        }

        /// <summary>
        /// Initialize the system and subscribe to the events
        /// </summary>
        public override void Init()
        {
            EventManager.Draw += Draw;
            EventManager.Dispose += Dispose;
            _shader.Init("res/terrain.shade");
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
                Terrain terrain = Entities[i].GetComponent<Terrain>();
                Renderer.DrawTriangle(terrain.Vao);
            }
        }
    }
}