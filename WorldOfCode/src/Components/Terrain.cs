using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// Defines a component that can be rendered to the screen
    /// </summary>
    public class Terrain : Component
    {
        /// <summary>
        /// The Vao that will store both the VBO and the IBO of the 
        /// </summary>
        public VertexArray Vao { get; }

        /// <summary>
        /// The vertices of the terrain
        /// </summary>
        public TerrainVertex[] Vertices { get; private set; }

        /// <summary>
        /// The size of the generated terrain
        /// </summary>
        private Vector2 size;

        /// <summary>
        /// The position of the terrain
        /// </summary>
        private Vector2 position;
        
        /// <summary>
        /// Creates a new terrain
        /// </summary>
        /// <param name="size">The size of the terrain</param>
        public Terrain(Vector2 size)
        {
            position = Vector2.Zero;
            this.size = size;
            Vertices = new TerrainVertex[(int)(size.X * size.Y)];
            //Generate vertices
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    this[x, y] = GenerateVertex(x, y);
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
            
            Vao = new VertexArray();
            VertexBuffer vbo = new VertexBuffer();
            IndexBuffer ibo = new IndexBuffer();
            BufferLayout layout = new BufferLayout();
            
            //Initialize the buffer objects
            Vao.Init();
            vbo.Init(Vertices);
            ibo.Init(indices.ToArray());
            
            //Set up the layout of the data
            layout.AddLayoutItem(VertexAttribPointerType.Float, 3);
            layout.AddLayoutItem(VertexAttribPointerType.Float, 4);
            
            //Add the buffers to the VAO
            Vao.Vbo = vbo;
            Vao.Ibo = ibo;
            Vao.Layout = layout;
        }

        private TerrainVertex this[int x, int y]
        {
            get => Vertices[x + y * (int) size.Y];
            set { Vertices[x + y * (int) size.Y] = value; }
        }

        private TerrainVertex GenerateVertex(int x, int y)
        {
            return Map.GetVertex(x + y % 2 * 0.5f - size.X / 2, y * (float) Math.Sin(1.0472) - size.Y / 2);
        }

        public void MoveTerrain(Direction direction)
        {
            Logger.Msg("start");
            for (int i = 0; i < 2; i++)
            {
                switch (direction)
                {
                    case Direction.Left:
                        position += new Vector2(0, 1);
                        for (int y = 1; y < size.Y; y++)
                        {
                            for (int x = 0; x < size.X; x++)
                            {
                                this[x, y-1] = this[x, y];
                            }
                        }
                        for (int x = 0; x < size.X; x++)
                        {
                            this[x, (int)size.Y - 1] = GenerateVertex((int) position.X + x, (int)( position.Y + size.Y));
                        }
                        break;
                    case Direction.Right:
//                        position -= new Vector2(0, 1);
//                        Vertices.RemoveRange(Vertices.Count - (int)size.X, (int)size.X);
//                        List<TerrainVertex> verts = new List<TerrainVertex>();
//                        for (int x = (int)size.X - 1; x >= 0; x--)
//                        {
//                            verts.Add(GenerateVertex((int)position.X - x + (int)size.X, (int)position.Y));
//                        }
//                        Vertices.InsertRange(0, verts);
                        break;
                    case Direction.Up:
                    case Direction.Down:
                        throw new ArgumentException("Invalid direction, direction must be either forward, backward, right or left.");
                }
            }
            Vao.Vbo.ChangeData(Vertices);
            Logger.Msg("end");
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