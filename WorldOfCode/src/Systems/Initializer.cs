using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// Initialized all entities
    /// </summary>
    public class Initializer : BaseSystem
    {
        /// <summary>
        /// Initialize all entities we need to initialize
        /// </summary>
        public override void Init()
        {
            //Initialize a test triangle
            //First generate all the necessary variables
            float[] vertices = {
                -.5f, 0, 0,
                0.5f, 0, 0,
                0, 1f, 0
            };
            uint[] indices = {
                0, 1, 2
            };
            VertexArray vertexArray = new VertexArray();
            VertexBuffer vbo = new VertexBuffer();
            IndexBuffer ibo = new IndexBuffer();
            BufferLayout layout = new BufferLayout();
            
            //Initialize the buffer objects
            vertexArray.Init();
            vbo.Init(vertices);
            ibo.Init(indices);
            
            //Set up the layout of the data
            layout.AddLayoutItem(VertexAttribPointerType.Float, 3);
            
            //Add the buffers to the VAO
            vertexArray.SetVbo(vbo);
            vertexArray.SetIbo(ibo);
            vertexArray.SetLayout(layout);
            
            //Create the entity
            Entity entity = new Entity();
            entity.AddComponents(new RenderAble(vertexArray));
            EcsManager.AddEntities(entity);
            
            
            //Create a basic player
            Camera camera = new Camera();
            camera.Position = Vector3.UnitZ * 3;
            camera.Yaw = -90;
            entity = new Entity();
            entity.AddComponents(new PlayerInput(1.5f, 0.2f), camera);
            EcsManager.AddEntities(entity);
        }
    }
}