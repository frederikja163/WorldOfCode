using System;
using System.Collections.Generic;

namespace WorldOfCode.ECS
{
    /// <summary>
    /// An entity of the ECS system
    /// </summary>
    public class Entity : IDisposable
    {
        private List<Component> _components;

        /// <summary>
        /// Get a component of give type if it exists on this entity
        /// </summary>
        /// <typeparam name="TComponentType">The type of the component derived from the base Component class</typeparam>
        /// <returns>Null if component not found, otherwise the found entity</returns>
        public TComponentType GetComponent<TComponentType>() where TComponentType : Component
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add components to this entity
        /// </summary>
        /// <param name="components">Components to add to this entity</param>
        public void AddComponent(params Component[] components)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose all the components of the entity
        /// </summary>
        public void Dispose()
        {
        }
    }
}