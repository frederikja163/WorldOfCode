using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// Vertex array, is basically an array of BufferObjects
    /// </summary>
    public class VertexArray : IDisposable
    {
        /// <summary>
        /// The handle for openTK
        /// </summary>
        private int _handle = -1;

        /// <summary>
        /// The data for the vertices
        /// </summary>
        private VertexBuffer _vbo;
        /// <summary>
        /// Set the data for the vertices
        /// </summary>
        /// <param name="value">The new vertex data</param>
        public void SetVbo(VertexBuffer value)
        {
            //Make sure any previous VBOs are disposed
            _vbo?.Dispose();
            
            //Set the VBO
            _vbo = value;
            Bind();
            _vbo.Bind();
        }
        

        /// <summary>
        /// The layout of the vertices
        /// </summary>
        private BufferLayout _layout;

        /// <summary>
        /// Set the layout of the vertices
        /// </summary>
        /// <param name="value">The new buffer layout</param>
        public void SetLayout(BufferLayout value)
        {
            //Set the layout and the offset initially
            _layout = value;
            Bind();
            int offset = 0;
            //Loop over each element
            for (int i = 0; i < _layout.Elements.Count; i++)
            {
                LayoutElement la = _layout.Elements[i];

                //Tell openTK how the layout of the vertices is
                GL.VertexAttribPointer(i, la.Count, la.Type, la.Normalized, _layout.Stride, offset);
                GL.EnableVertexAttribArray(i);

                //Increment the offset
                offset += LayoutElement.GetSizeOfType(la.Type) * la.Count;
            }
        }

        /// <summary>
        /// The order of the vertices
        /// </summary>
        private IndexBuffer _ibo;
        /// <summary>
        /// Set the order of the vertices
        /// </summary>
        /// <param name="value">The new index buffer</param>
        public void SetIbo (IndexBuffer value)
        {
            //Make sure any previous IBOs are disposed
            _ibo?.Dispose();
            
            //Set the IBO
            _ibo = value;
            Bind();
            _ibo.Bind();
        }
        /// <summary>
        /// The amount of indices
        /// </summary>
        public int IndexCount => _ibo.Count;
        
        
        /// <summary>
        /// Currently bound vertex array
        /// </summary>
        private static VertexArray _bound;
        
        /// <summary>
        /// Initialize the Vertex Array
        /// </summary>
        public void Init()
        {
            //Initialize the object
            _handle = GL.GenVertexArray();
        }

        /// <summary>
        /// Binds this vertex array
        /// </summary>
        public void Bind()
        {
            _bound = this;
            GL.BindVertexArray(_handle);
        }

        /// <summary>
        /// Unbinds this vertex array
        /// </summary>
        public void Unbind()
        {
            if (_bound != this) { return; }
            GL.BindVertexArray(0);
        }

        #region Dispose
        /// <summary>
        /// Has the vertex array been disposed
        /// </summary>
        private bool _isDisposed = false;
        /// <summary>
        /// Dispose the vertex array and clean up memory
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                //Dispose of the vertex- and index buffer
                _vbo.Dispose();
                _vbo = null;
                
                _ibo.Dispose();
                _ibo = null;
                
                //Dispose the vertex array
                _handle = -1;
                GL.DeleteVertexArray(_handle);
                Unbind();
                
                _isDisposed = true;
            }
        }

        /// <summary>
        /// Always make sure we dispose correctly, this is a last measure
        /// </summary>
        ~VertexArray()
        {
            Dispose();
        }
        #endregion Dispose
    }
}