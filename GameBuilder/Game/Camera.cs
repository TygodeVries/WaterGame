using GameBuilder.Levels;
using GameBuilder.Rendering;
using GameBuilder.User;
using System;
using System.Diagnostics;

namespace GameBuilder.Game
{
    internal class Camera
    {
        public static float x = 0;
        public static float y = 0;

        public enum state
        {
            off,
            locked,
            follow,
            debug
        }

        public static state currectCameraState = state.off;

        public static GameObject tracking;



        static double lastX = 0;

        public static void tick()
        {
            if (currectCameraState == state.follow && tracking != null)
            {
                // Send posistion update to renderer.

                int xBufferSize = (RenderingEngine.X * 16);
                int yBufferSize = (RenderingEngine.Y * 16);


                Camera.x = (int)(tracking.posistion.x / xBufferSize) * xBufferSize;
                Camera.y = (int)((tracking.posistion.y + (yBufferSize / 2)) / yBufferSize) * yBufferSize;

                Camera.x += xBufferSize / 2;

                RenderingEngine.CameraX = (x - (RenderingEngine.bitmapRender16.Width / 2)) + 0.5f;
                RenderingEngine.CameraY = y - (RenderingEngine.bitmapRender16.Height / 2);

                if((x - (RenderingEngine.bitmapRender16.Width / 2)) + 0.5f != lastX)
                {
                    ChancedX();
                }


                lastX = (x - (RenderingEngine.bitmapRender16.Width / 2)) + 0.5f; 
            }
        }

        static void ChancedX()
        {
            Console.WriteLine("Checkpoint!");
            Main.respawnPos = Main.playerController.gameObject.posistion.copy();
        }
    }
}
