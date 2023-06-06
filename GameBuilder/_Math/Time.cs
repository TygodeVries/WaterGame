using GameBuilder.Levels;
using GameBuilder.Rendering;
using System;

namespace GameBuilder._Math
{
    internal class Time
    {
        public static float DeltaTime;



        static DateTime LastTick;
        public static int FPS;

        public static void Tick()
        {
            DateTime now = DateTime.Now;

            DeltaTime = (float)now.Subtract(LastTick).TotalMilliseconds / 1000;
            FPS = (int)(1000 / (float)now.Subtract(LastTick).TotalMilliseconds);

            LastTick = DateTime.Now;



        }
    }
}
