using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Rendering;
using System;

namespace GameBuilder._Math
{
    internal class Time
    {
        public static void Start()
        {
            Debug.SendDebugMessage("Starting time...");
            Tick();
            Tick();
        }

        /// <summary>
        /// Time in MS since last frame
        /// </summary>
        public static float DeltaTime = 0;

        /// <summary>
        /// Current framerate of the game.
        /// </summary>
        public static int FPS = 0;

        static DateTime LastTick;
        /// <summary>
        /// Tick time
        /// </summary>
        public static void Tick()
        {
            DateTime now = DateTime.Now;

            DeltaTime = (float)now.Subtract(LastTick).TotalMilliseconds / 1000;
            FPS = (int)(1000 / (float)now.Subtract(LastTick).TotalMilliseconds);

            LastTick = DateTime.Now;

            if (DeltaTime > 0.1f) DeltaTime = 0.1f;
        }
    }
}
