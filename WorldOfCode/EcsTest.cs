using System;
using WorldOfCode.ECS;

namespace WorldOfCode
{
    public class EcsTest
    {
        public void Test()
        {
            //Write a quick test of the ecs system, should be contained in only this one file to make cleanup easier
            //When i want to remove this file i should remember to update the main function (Entrypoint)
            //to avoid errors the next time i test
            //To help with the test i have also temporarily added a update function to the system called through
            //the ECS manager. This should run through the event system later on.
            
            Entity entity1 = new Entity();
            entity1.AddComponents(new Component1(), new Component2());
            Entity entity2 = new Entity();
            entity2.AddComponents(new Component1());
            EcsManager.AddEntities(entity1, entity2);
            
            //The ECS manager has been initialized from the entrypoint of the application
            EcsManager.Update();
            EcsManager.Update();
            EcsManager.Update();
        }
    }

    public class System1 : ECS.BaseSystem
    {
        protected override bool IsValidEntity(Entity entity)
        {
            return entity.GetComponent<Component1>() != null;
        }

        public override void Update()
        {
            Console.WriteLine($"Update system 1");
            for (int i = 0; i < Entities.Count; i++)
            {
                Console.WriteLine($"X++: {Entities[i].GetComponent<Component1>().X++}");
            }
        }
    }

    public class System2 : ECS.BaseSystem
    {
        protected override bool IsValidEntity(Entity entity)
        {
            return entity.GetComponent<Component2>() != null && entity.GetComponent<Component1>() != null;
        }

        public override void Update()
        {
            Console.WriteLine($"Update system 2");
            for (int i = 0; i < Entities.Count; i++)
            {
                Console.WriteLine($"X++: {Entities[i].GetComponent<Component1>().X++} | Name: {Entities[i].GetComponent<Component2>().name}");
            }
        }
    }

    public class Component1 : ECS.Component
    {
        public float X, Y;
    }

    public class Component2 : ECS.Component
    {
        public string name;
    }
}