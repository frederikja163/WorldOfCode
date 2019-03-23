using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// Layout of a single element of the buffer
    /// </summary>
    public struct LayoutElement
    {
        /// <summary>
        /// Type of variable this element is
        /// </summary>
        public VertexAttribPointerType Type;
        /// <summary>
        /// The count of the variable in this element
        /// </summary>
        public int Count;
        /// <summary>
        /// Has the variable been normalized on the CPU side
        /// </summary>
        public bool Normalized;

        /// <summary>
        /// Get the size of a VertexAttribPointerType in bytes
        /// </summary>
        /// <param name="type">The type to look for the size of</param>
        /// <returns>The size of the type in bytes</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the argument is a type not commonly used in C#</exception>
        public static int GetSizeOfType(VertexAttribPointerType type)
        {
            switch (type)
            {
                case VertexAttribPointerType.Byte:
                    return sizeof(sbyte);
                case VertexAttribPointerType.UnsignedByte:
                    return sizeof(byte);
                case VertexAttribPointerType.Short:
                    return sizeof(short);
                case VertexAttribPointerType.UnsignedShort:
                    return sizeof(ushort);
                case VertexAttribPointerType.Int:
                    return sizeof(int);
                case VertexAttribPointerType.UnsignedInt:
                    return sizeof(uint);
                case VertexAttribPointerType.Float:
                    return sizeof(float);
                case VertexAttribPointerType.Double:
                    return sizeof(double);
                case VertexAttribPointerType.HalfFloat:
                case VertexAttribPointerType.Fixed:
                case VertexAttribPointerType.UnsignedInt2101010Rev:
                case VertexAttribPointerType.UnsignedInt10F11F11FRev:
                case VertexAttribPointerType.Int2101010Rev:
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
    
    /// <summary>
    /// Layout of a single buffer object
    /// </summary>
    public class BufferLayout
    {
        /// <summary>
        /// A list of the elements of the buffer layout
        /// </summary>
        public readonly List<LayoutElement> Elements = new List<LayoutElement>();

        /// <summary>
        /// The stride of the buffer layout
        /// </summary>
        private int _stride;
        /// <summary>
        /// Total size of the buffer layout
        /// </summary>
        public int Stride { get => _stride; }
        
        /// <summary>
        /// Add a new layout item to the buffer layout
        /// </summary>
        /// <param name="type">The type of variable</param>
        /// <param name="count">The amount of the given variable</param>
        /// <param name="normalized">Is the variable normalized on the CPU side</param>
        public void AddLayoutItem(VertexAttribPointerType type, int count, bool normalized = false)
        {
            Elements.Add(new LayoutElement(){Type = type, Count = count, Normalized = normalized});
            _stride += LayoutElement.GetSizeOfType(type) *count;
        }
    }
}