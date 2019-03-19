using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
           //Get all types of the assembly
           List<Type> types =  Assembly.GetEntryAssembly().GetTypes().ToList();
           for (int i = 0; i < types.Count; i++)
           {
               if (types[i].IsSubclassOf(typeof(System))) //Is the type derived from the system base class
               {
                   //Create a system instance
                   _systems.Add(Activator.CreateInstance(types[0]) as System);
                   //Initialize the system
                   //TODO: Make some sort of calling hierachy here instead
                   _systems.Last().Init();
               }
           }
        }
    }
}