using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// A buffer object, arrays of information for the GPU to draw from
    /// </summary>
    public class BufferObject : IDisposable
    {
        /// <summary>
        /// The type of buffer
        /// </summary>
        private BufferTarget _bufferType;
        /// <summary>
        /// The handle for the BO on the GPU
        /// </summary>
        private int _handle = -1;

        /// <summary>
        /// bound buffer objects
        /// </summary>
        private static Dictionary<BufferTarget, BufferObject> _bound = new Dictionary<BufferTarget, BufferObject>();
        
        /// <summary>
        /// Initializes the buffer and sets the data for it
        /// </summary>
        /// <param name="bufferType">The type of buffer object</param>
        /// <param name="data">The data to initialize the buffer object with</param>
        /// <param name="hint">Hint to where the data should be stored on the GPU</param>
        /// <typeparam name="TDataType">The type of data this buffer object will manage</typeparam>
        protected unsafe void Init<TDataType>
            (BufferTarget bufferType,
            TDataType[] data,
            BufferUsageHint hint = BufferUsageHint.StaticDraw)
            where TDataType : unmanaged
        {
            if (_handle != -1)
            {
                Dispose();
                _isDisposed = false;
            }
            //Initialize the buffer object
            _handle = GL.GenBuffer();
            _bufferType = bufferType;
            
            Bind();
            GL.BufferData(_bufferType, sizeof(TDataType) * data.Length, data, hint);
        }

        /// <summary>
        /// Change the data of the buffer
        /// </summary>
        /// <param name="data">The new data</param>
        /// <param name="index">The index of where to start the data</param>
        /// <typeparam name="TDataType">The type of data</typeparam>
        public unsafe void ChangeData<TDataType>(TDataType[] data, int index = 0) where TDataType : unmanaged
        {
            Bind();
            GL.BufferSubData(_bufferType, (IntPtr)(index * sizeof(TDataType)), data.Length * sizeof(TDataType) , data);
        }

        /// <summary>
        /// Binds this buffer object
        /// </summary>
        public void Bind()
        {
            if (!_bound.ContainsKey(_bufferType)){ _bound.Add(_bufferType, this); }

            GL.BindBuffer(_bufferType, _handle);
        }

        /// <summary>
        /// Unbinds this buffer object
        /// </summary>
        public void Unbind()
        {
            if (_bound[_bufferType] != this){ return; }
            GL.BindBuffer(_bufferType, 0);
        }

        #region Dispose
        /// <summary>
        /// Has the object been disposed
        /// </summary>
        private bool _isDisposed = false;
        /// <summary>
        /// Dispose the object and clean up memory
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Unbind();
                GL.DeleteBuffer(_handle);
                
                _handle = -1;
                _isDisposed = true;
            }
        }

        /// <summary>
        /// Always make sure we dispose correctly, this is a last measure
        /// </summary>
        ~BufferObject()
        {
            Dispose();
        }
        #endregion Dispose
    }
}