using GameBuilder._Math;
using GameBuilder.Rendering;
using GameBuilder.User;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using SharpDX.Text;

namespace GameBuilder.Game
{
    internal class Debug
    {
        static List<string> log = new List<string>();

        public static bool DebugmodeIsOn = true;

        public static void SendDebugMessage(string message)
        {
            string msg = DateTime.Now.ToShortTimeString() + " : [DEBUG] " + message;
        //    Console.WriteLine(msg);
            log.Add(msg);
        }

        public static void SendErrorMessage(string message)
        {
            string msg = DateTime.Now.ToShortTimeString() + " : [ERROR] " + message;
         //   Console.WriteLine(msg);
            log.Add(msg);
        }
        public static void SendFatalErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            
            // Create logs directory if missing
            Directory.CreateDirectory(Program.DataPath + "\\logs");

            log.Add(DateTime.Now.ToShortTimeString() + " : [! FATAL !] " + message);

            writelogtoFile("latest");
            writelogtoFile(DateTime.Now.ToShortDateString().Replace('/', '-') + "_" + DateTime.Now.ToShortTimeString().Replace(':', '-').Replace(' ', '-') + $"({DateTime.Now.Second})");

            Process p = Process.Start("notepad.exe", (Program.DataPath + "\\logs\\latest.txt"));
            
            Environment.Exit(0);
        }

        static void writelogtoFile(string name)
        {
            File.WriteAllText(Program.DataPath + $"\\logs\\{name}.txt", ".");

            Thread.Sleep(500);

            FileStream stream = new FileStream(Program.DataPath + $"\\logs\\{name}.txt", FileMode.Open, FileAccess.ReadWrite);

            string msg = "";

            // Crash message
            msg = " -- Project Midnight has crashed. -- \n";
            stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);

            // White line
            msg = "\n";
            stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);

            // Log
            foreach (string m in log)
            {
                msg = m + "\n";
                stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);
            }

            // White line
            msg = "\n";
            stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);

            // Crash message
            msg = " -- Project Midnight has crashed. -- \n";
            stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);

            // White line
            msg = "\n";
            stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);

            // Save target
            msg = "This log has been saved to: " + Program.DataPath + "\\logs\\latest.txt\n";
            stream.Write(Encoding.UTF8.GetBytes(msg), 0, Encoding.UTF8.GetBytes(msg).Length);

            stream.Close();
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
