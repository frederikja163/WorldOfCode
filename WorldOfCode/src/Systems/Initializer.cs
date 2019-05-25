using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// Initialized all entities
    /// </summary>
    public class Initializer : BaseSystem
    {
        /// <summary>
        /// Initialize all entities we need to initialize
        /// </summary>
        public override void Init()
        {
            Vector2 size = new Vector2(500, 500);
            Entity terrain = new Entity();
            terrain.AddComponents(new Terrain(size));
            
            //Create the main camera1
            Camera camera = new Camera();
            camera.Position = new Vector3(0, 3, 0);
            camera.Yaw = 0;
            Entity player = new Entity();
            player.AddComponents(new PlayerInput(25f, 0.2f), camera);
            EcsManager.AddEntities(player, terrain);
        }
    }
}