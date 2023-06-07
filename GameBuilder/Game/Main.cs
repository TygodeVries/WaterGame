using GameBuilder.Levels;
using GameBuilder.Physics;
using GameBuilder.Scripts;
using GameBuilder.Particle;
using System;

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


        static Random rng = new Random();

        public static void Update()
        {
            if (!paused)
            {
                GameObject.TickGameObjects();
                PhysicsEngine.Tick();
                GameObject.TickLateGameObjects();
                if (DebugIsOn) Debug.Tick();
                Camera.tick();
            }
        }
    }
}
