using GameBuilder.Levels;
using GameBuilder.Physics;
using GameBuilder.Scripts;
using GameBuilder.Particle;
using System;
using GameBuilder._Math;

namespace GameBuilder.Game
{
    internal class Main
    {
        public static bool DebugIsOn = true;

        public static PlayerController playerController;

        public static bool paused;
        public static void Start()
        {

        }

        public static void Update()
        {
            if (!paused)
            {
                GameObject.TickGameObjects();

                if(Time.FPS > 30) PhysicsEngine.Tick();
                
                GameObject.TickLateGameObjects();
                if (DebugIsOn) Debug.Tick();
                Camera.tick();
            }
        }
    }
}
