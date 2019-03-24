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
            BufferObject vbo = new BufferObject();
            BufferObject ibo = new BufferObject();
            BufferLayout layout = new BufferLayout();
            
            //Initialize the buffer objects
            vertexArray.Init();
            vbo.Init(BufferTarget.ArrayBuffer, vertices);
            ibo.Init(BufferTarget.ElementArrayBuffer, indices);
            
            //Set up the layout of the data
            layout.AddLayoutItem(VertexAttribPointerType.Float, 3);
            
            //Add the buffers to the VAO
            vertexArray.AddBuffer(ref vbo, layout);
            vertexArray.AddBuffer(ref ibo);
            
            //Create the entity
            Entity entity = new Entity();
            entity.AddComponents(new RenderAble(vertexArray));
            EcsManager.AddEntities(entity);
        }
    }
}