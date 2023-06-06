using GameBuilder._Math;
using GameBuilder.Rendering;
using GameBuilder.User;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameBuilder.Game
{
    internal class Debug
    {

        public static bool DebugmodeIsOn = true;

        public static void SendDebugMessage(string message)
        {
            Console.WriteLine("[DEBUG] " + message);
        }

        public static void SendErrorMessage(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }
        public static void SendFatalErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[FATAL] " + message);
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public static void Tick()
        {
            //  UpdateCameraPosistion();

        }
        public static void UpdateCameraPosistion()
        {
            if (KeyboardInput.GetKey(Keys.Right))
            {
                RenderingEngine.CameraX += 1;
            }
            if (KeyboardInput.GetKey(Keys.Left))
            {
                RenderingEngine.CameraX -= 1;
            }
            if (KeyboardInput.GetKey(Keys.Up))
            {
                RenderingEngine.CameraY -= 1;
            }
            if (KeyboardInput.GetKey(Keys.Down))
            {
                RenderingEngine.CameraY += 1;
            }
        }
    }
}
