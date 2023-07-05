using GameBuilder.Levels;
using GameBuilder.Physics;
using GameBuilder.Scripts;
using GameBuilder.Particle;
using System;
using GameBuilder._Math;
using GameBuilder.Audio;
using System.Security.RightsManagement;

namespace GameBuilder.Game
{
    internal class Main
    {
        public static Vector respawnPos = null;

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
