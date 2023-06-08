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
                if (Time.FPS > 10)
                {
                    GameObject.TickGameObjects();
                    PhysicsEngine.Tick();
                    GameObject.TickLateGameObjects();
                }
                else
                {
                    Debug.SendErrorMessage("FPS is to low, pausing game.");
                }
                if (DebugIsOn) Debug.Tick();
                Camera.tick();
            }
        }
    }
}
