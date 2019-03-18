using System;

namespace WorldOfCode.ECS
{
    /// <summary>
    /// The base component of the ECS system, derive from this class to make a component
    /// </summary>
    public abstract class Component : IDisposable
    {
        /// <summary>
        /// Gets called once the owner entity gets destroyed
        /// </summary>
        public virtual void Dispose() { }
    }
}