using GameBuilder._Math;
using GameBuilder.Rendering;
using GameBuilder.User;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

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
            Console.WriteLine();
            Directory.CreateDirectory(Program.DataPath + "\\logs");
            File.WriteAllLines(Program.DataPath + "\\logs\\latest.txt", new string[] { " -- Project Midnight has crashed. -- ","", "[FATAL] " + message, "", " --  Project Midnight has crashed. -- ", "", "This log has been saved to: " + Program.DataPath + "\\logs\\latest.txt" });
            
            Process.Start("notepad.exe", (Program.DataPath + "\\logs\\latest.txt"));
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
