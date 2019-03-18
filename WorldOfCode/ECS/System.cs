using System;
using System.Collections.Generic;

namespace WorldOfCode.ECS
{
    /// <summary>
    /// The base System for the ECS system, derive from this class to make a system
    /// </summary>
    public abstract class System
    {
        /// <summary>
        /// All the entities managed by this system
        /// </summary>
        protected List<Entity> Entities;

        /// <summary>
        /// Add an entity to the system
        /// </summary>
        /// <param name="entity">The entity to add to the system</param>
        public void AddEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a entity from the system, it will no longer be updated
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Initialize the system
        /// Override this and make calls to the event system here to "subscribe" to the different events
        /// </summary>
        /// TODO: Create event system we can use here
        public virtual void Init() { }
    }
}