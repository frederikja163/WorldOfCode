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
        private static readonly List<Entity> Entities = new List<Entity>();
        /// <summary>
        /// A list of all the systems of the ECS system
        /// </summary>
        private static readonly List<System> Systems = new List<System>();
        
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
                   Systems.Add(Activator.CreateInstance(types[i]) as System);
                   //Initialize the system
                   //TODO: Make some sort of calling hierachy here instead
                   Systems.Last().Init();
               }
           }
        }

        /// <summary>
        /// Add entities to the ECS system
        /// This allows the systems to see the entities
        /// </summary>
        /// <param name="entities">The entities to add the the system</param>
        public static void AddEntities(params Entity[] entities)
        {
            Entities.AddRange(entities);
            for (int i = 0; i < Systems.Count; i++)
            {
                for (int j = 0; j < entities.Length; j++)
                {
                    Systems[i].AddEntity(ref entities[j]);
                }
            }
        }
        
        //TEMPORARY
        public static void Update()
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                Systems[i].Update();
            }
        }
    }
}