using GameBuilder.Levels;
using GameBuilder.Rendering;
using GameBuilder.User;
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

        public static void tick()
        {
            if(currectCameraState == state.debug)
            {
                if (InputManager.GetKey("Right")) RenderingEngine.CameraX += 1;
                if (InputManager.GetKey("Left")) RenderingEngine.CameraX -= 1;

                if (InputManager.GetKey("FastFall")) RenderingEngine.CameraY += 1;
                if (InputManager.GetKey("Jump")) RenderingEngine.CameraY -= 1;

                Debug.SendDebugMessage("cam pos: " + RenderingEngine.CameraX + ", " + RenderingEngine.CameraY);
            }

            if (currectCameraState == state.follow)
            {
                // Send posistion update to renderer.

                Camera.x = tracking.posistion.x;
                Camera.y = tracking.posistion.y;

                RenderingEngine.CameraX = x - (RenderingEngine.bitmapRender16.Width / 2) + 20;
                RenderingEngine.CameraY = y - (RenderingEngine.bitmapRender16.Height / 2);
            }
        }
    }
}
