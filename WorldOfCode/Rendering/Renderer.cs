using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    //For now this class is just a test, it will be fully implemented later, but for now i need a test to see if my openGL abstraction actually works and runs
    public class Renderer
    {
        private BufferObject VBO = new BufferObject();
        private BufferObject IBO = new BufferObject();
        private VertexArray VAO = new VertexArray();
        private Shader Shader = new Shader();

        public void Init()
        {
            float[] vertices = {
                -.5f, 0, 0,
                .5f, 0, 0,
                0, 1, 0
            };
            uint[] indices = {
                0, 1, 2
            };
            
            VBO.Init(BufferTarget.ArrayBuffer, vertices);
            IBO.Init(BufferTarget.ElementArrayBuffer, indices);
            
            VAO.Init();
            
            BufferLayout layout = new BufferLayout();
            layout.AddLayoutItem(VertexAttribPointerType.Float, 3);
            
            VAO.AddBuffer(VBO, layout);
            VAO.AddBuffer(IBO);
            
            Shader.Init("basic.shade");
            
            GL.ClearColor(0, 0, 0, 1);
        }

        public void Draw()
        {
            Shader.Bind();
            VAO.Bind();
            GL.DrawElements(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, 0);
        }
    }
}