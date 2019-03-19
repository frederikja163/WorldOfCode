using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfCode.ECS
{
    /// <summary>
    /// An entity of the ECS system
    /// </summary>
    public class Entity : IDisposable
    {
        private List<Component> _components = new List<Component>();

        /// <summary>
        /// Get a component of give type if it exists on this entity
        /// </summary>
        /// <typeparam name="TComponentType">The type of the component derived from the base Component class</typeparam>
        /// <returns>Null if component not found, otherwise the found entity</returns>
        public TComponentType GetComponent<TComponentType>() where TComponentType : Component
        {
            IEnumerable<TComponentType> components = _components.OfType<TComponentType>();
            return components.Count() > 0 ? components.First() : null;
        }

        /// <summary>
        /// Add components to this entity
        /// </summary>
        /// <param name="components">Components to add to this entity</param>
        public void AddComponents(params Component[] components)
        {
            _components.AddRange(components);
        }

        /// <summary>
        /// Dispose all the components of the entity
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Dispose();
            }
        }
    }
}