using OpenTK;
using OpenTK.Input;

namespace WorldOfCode
{
    /// <summary>
    /// Renders all entities
    /// </summary>
    public class TerrainSystem : BaseSystem
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
            EventManager.Update += Update;
            _shader.Init("res/terrain.shade");
        }

        /// <summary>
        /// Update the position of the terrain
        /// </summary>
        private void Update()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Terrain terrain = Entities[i].GetComponent<Terrain>();
                Vector2 movement = Camera.Main.Position.Xz - terrain.Position;
                if (movement.X >= 1)
                {
                    terrain.MoveTerrain(Direction.Left, (int)movement.X);
                }
                else if (movement.X <= -1)
                {
                    terrain.MoveTerrain(Direction.Right, (int)-movement.X);
                }
                if (movement.Y >= 1)
                {
                    terrain.MoveTerrain(Direction.Forward, (int)movement.Y);
                }
                else if (movement.Y <= -1)
                {
                    terrain.MoveTerrain(Direction.Back, (int)-movement.Y);
                }
            }
        }

        /// <summary>
        /// Dispose the system
        /// </summary>
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