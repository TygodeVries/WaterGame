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
        public static void Update()
        {
            GameObject.TickGameObjects();
            PhysicsEngine.Tick();
            GameObject.TickLateGameObjects();
            Camera.tick();
        }
    }
}
