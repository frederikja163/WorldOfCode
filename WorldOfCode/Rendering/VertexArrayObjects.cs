using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        /// Currently bound vertex array
        /// </summary>
        private static VertexArray _bound;
        
        /// <summary>
        /// Initialize the Vertex Array
        /// </summary>
        public void Init()
        {
            //If the object is not clean return
            if (!_isDisposed)
            {
                return;
            }
            _isDisposed = false;
            
            //Initialize the object
            _handle = GL.GenVertexArray();
        }

        /// <summary>
        /// Add a buffer to the vertex array
        /// </summary>
        /// <param name="bufferObject">Buffer object to add to the4 vertex array</param>
        /// <param name="bufferLayout">Buffer layout of the buffer object</param>
        public void AddBuffer(BufferObject bufferObject, BufferLayout bufferLayout = null)
        {
            //Add the buffer object
            Bind();
            bufferObject.Bind();
            //Add the buffer layout
            if (bufferLayout != null)
            {
                int offset = 0;
                for (int i = 0; i < bufferLayout.Elements.Count; i++)
                {
                    LayoutElement la = bufferLayout.Elements[i];
                    GL.VertexAttribPointer(i, la.Count, la.Type, la.Normalized, bufferLayout.Stride, offset);
                    GL.EnableVertexAttribArray(i);
                    offset += LayoutElement.GetSizeOfType(la.Type) * la.Count;
                }
            }
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
        private bool _isDisposed = true;
        /// <summary>
        /// Dispose the vertex array and clean up memory
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                GL.DeleteVertexArray(_handle);
                Unbind();
                
                _handle = -1;
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