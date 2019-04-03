using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WorldOfCode
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
        private static readonly List<BaseSystem> Systems = new List<BaseSystem>();
        
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
               if (types[i].IsSubclassOf(typeof(BaseSystem))) //Is the type derived from the system base class
               {
                   Systems.Add(Activator.CreateInstance(types[i]) as BaseSystem);
               }
           }

           //As some of the initializers create entities that all systems must see
           //The systems are looped over once more here
           //TODO: Make some sort of calling hierachy here instead
           for (int i = 0; i < Systems.Count; i++)
           {
               Systems[i].Init();
           }

           EventManager.Dispose += () => RemoveEntities(Entities.ToArray());
           
           Logger.Msg("ECS manager initialized");
        }

        /// <summary>
        /// Add entities to the ECS system
        /// This allows the systems to see the entities
        /// </summary>
        /// <param name="entities">The entities to add to the system</param>
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

        /// <summary>
        /// Removes entities from the ECS system
        /// Also notifies the systems and the entities that the entities have been terminated
        /// </summary>
        /// <param name="entities">Entities to remove from the system</param>
        public static void RemoveEntities(params Entity[] entities)
        {
            //Notify the systems that entities will be terminated
            for (int i = 0; i < Systems.Count; i++)
            {
                for (int j = 0; j < entities.Length; j++)
                {
                    Systems[i].RemoveEntity(entities[j]);
                }
            }

            //Remove all the entities
            for (int i = 0; i < entities.Length; i++)
            {
                Entities.Remove(entities[i]);
                entities[i].Dispose();
            }
        }
    }
}