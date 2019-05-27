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
        private Vector2 _size;

        /// <summary>
        /// The position of the terrain
        /// </summary>
        public Vector2 Position { get; private set; }
        
        /// <summary>
        /// Creates a new terrain
        /// </summary>
        /// <param name="size">The size of the terrain</param>
        public Terrain(Vector2 size)
        {
            Position = Vector2.Zero;
            this._size = size;
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
            get => Vertices[x + y * (int) _size.Y];
            set { Vertices[x + y * (int) _size.Y] = value; }
        }

        private TerrainVertex GenerateVertex(float x, float y)
        {
            return Map.GetVertex(x + y % 2 * 0.5f - _size.X / 2 + Position.X, y * (float) Math.Sin(1.0472) - _size.Y / 2 + Position.Y);
        }

        /// <summary>
        /// Move the terrain in a direction
        /// </summary>
        /// <param name="direction">Direction, can be either forward, back, left or right</param>
        /// <exception cref="ArgumentException">Thrown if direction is up or down</exception>
        public void MoveTerrain(Direction direction)
        {
            for (int i = 0; i < 2; i++)
            {
                switch (direction)
                {
                    case Direction.Forward:
                        Position += new Vector2(0, 1);
                        Array.Copy(Vertices, (int)_size.X, Vertices, 0, Vertices.Length - (int)_size.X);
                        for (int x = 0; x < _size.X; x++)
                        {
                            this[x, (int)_size.Y - 1] = GenerateVertex(x,  _size.Y);
                        }
                        break;
                    case Direction.Back:
                        Position += new Vector2(0, -1);
                        Array.Copy(Vertices, 0, Vertices, (int)_size.X, Vertices.Length - (int)_size.X);
                        for (int x = 0; x < _size.X; x++)
                        {
                            this[x, 0] = GenerateVertex(x, 0);
                        }
                        break;
                    case Direction.Left:
                        Position += new Vector2(1, 0);
                        for (int y = 0; y < _size.X; y++)
                        {
                            Array.Copy(Vertices, y * (int)_size.X + 1, Vertices, y * (int)_size.X, (int)_size.X - 1);
                            this[(int)_size.X - 1, y] = GenerateVertex( _size.X, y);
                        }
                        break;
                    case Direction.Right:
                        Position += new Vector2(-1, 0);
                        for (int y = 0; y < _size.X; y++)
                        {
                            Array.Copy(Vertices, y * (int)_size.X, Vertices, y * (int)_size.X + 1, (int)_size.X - 1);
                            this[0, y] = GenerateVertex(0, y);
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid direction, direction must be either forward, backward, right or left.");
                }
            }

            Vao.Vbo.ChangeData(Vertices);
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