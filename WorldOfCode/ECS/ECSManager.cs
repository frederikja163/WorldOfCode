using System;
using System.Collections.Generic;

namespace WorldOfCode.ECS
{
    /// <summary>
    /// Manages the entire Entity Component System
    /// </summary>
    public static class EcsManager
    {
        /// <summary>
        /// A list of all the entities of the ECS system
        /// </summary>
        private static List<Entity> _entities;
        /// <summary>
        /// A list of all the systems of the ECS system
        /// </summary>
        private static List<System> _systems;
        
        /// <summary>
        /// Initialize the Systems and set them up
        /// Makes the ECS system ready to create entities
        /// </summary>
        public static void Init()
        {
            throw new NotImplementedException();
        }
    }
}