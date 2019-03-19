using System.Collections.Generic;

namespace WorldOfCode.ECS
{
    /// <summary>
    /// The base System for the ECS system, derive from this class to make a system
    /// </summary>
    public abstract class BaseSystem
    {
        /// <summary>
        /// All the entities managed by this system
        /// </summary>
        protected readonly List<Entity> Entities = new List<Entity>();
        
        /// <summary>
        /// Add an entity to the system
        /// </summary>
        /// <param name="entity">The entity to add to the system</param>
        public void AddEntity(ref Entity entity)
        {
            if (IsValidEntity(entity))
            {
                Entities.Add(entity);
            }
        }

        /// <summary>
        /// Remove a entity from the system, it will no longer be updated
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        /// <summary>
        /// Check if a given entity is valid
        /// Should be implemented by derived systems to specify wether or not the entity should be computed
        /// </summary>
        /// <param name="entity">The entity to check for components</param>
        /// <returns>True: Keep track of the entity, False: Dont keep track of the entity</returns>
        protected virtual bool IsValidEntity(Entity entity)
        {
            return false;
        }
        
        /// <summary>
        /// Initialize the system
        /// Override this and make calls to the event system here to "subscribe" to the different events
        /// </summary>
        /// TODO: Create event system we can use here
        public virtual void Init() { }
    }
}