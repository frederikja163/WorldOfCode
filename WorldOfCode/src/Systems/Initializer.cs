using System;
using System.Collections.Generic;
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
            #region Terrain

            //Generate vertices
            Vector2 size = new Vector2(100, 100);
            List<TerrainVertex> vertices = new List<TerrainVertex>();
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    vertices.Add(Map.GetVertex(x + y % 2 * 0.5f, y * (float) Math.Sin(1.0472)));
                }
            }
            
            //Generate the indices
            List<uint> indices = new List<uint>();
            //Adds a single index to the list based on the position of the vertex
            void AddIndex(int x, int y)
            {
                indices.Add((uint)(x + y * size.Y));
            }
            //Generate all the indices
            for (int y = 0; y < size.Y - 1; y++)
            {
                for (int x = 0; x < size.X - 1; x++)
                {
                    AddIndex(x, y);
                    AddIndex(x + 1, y + y % 2);
                    AddIndex(x, y + 1);

                    if (x != 0)
                    {
                        AddIndex(x, y);
                        AddIndex(x, y + 1);
                        AddIndex(x - 1, y + (y+1)%2);
                    }
                }
            }
            
            VertexArray vertexArray = new VertexArray();
            VertexBuffer vbo = new VertexBuffer();
            IndexBuffer ibo = new IndexBuffer();
            BufferLayout layout = new BufferLayout();
            
            //Initialize the buffer objects
            vertexArray.Init();
            vbo.Init(vertices.ToArray());
            ibo.Init(indices.ToArray());
            
            //Set up the layout of the data
            layout.AddLayoutItem(VertexAttribPointerType.Float, 3);
            layout.AddLayoutItem(VertexAttribPointerType.Float, 4);
            
            //Add the buffers to the VAO
            vertexArray.SetVbo(vbo);
            vertexArray.SetIbo(ibo);
            vertexArray.SetLayout(layout);
            
            //Create the entity
            Entity entity = new Entity();
            entity.AddComponents(new Terrain(vertexArray));
            EcsManager.AddEntities(entity);

            #endregion Terrain
            
            //Create the main camera
            Camera camera = new Camera();
            camera.Position = new Vector3(size.X/2f, 3, 0);
            camera.Yaw = 90;
            entity = new Entity();
            entity.AddComponents(new PlayerInput(25f, 0.2f), camera);
            EcsManager.AddEntities(entity);
        }
    }
}