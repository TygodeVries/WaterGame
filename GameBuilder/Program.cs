using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using GameBuilder.User;
using System;
using System.Threading;

namespace GameBuilder
{
    internal class Program
    {
        public static string AssetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.midnight\\assets";
        public static string DataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.midnight";
        static void Main(string[] args)
        {
            // Time that loading starts.
            DateTime startTime = DateTime.Now;

            //  Create Windows
            Window.CreateWindow();
            Debug.SendDebugMessage("Waiting for window to start.");

            while (!Window.IsReady)
            {

            }

            Debug.SendDebugMessage("Detected window is launched!");
            
            // Load assets from web.
            AssetLoading.LoadAssetsIfNotExist(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            // Start engine.
            RenderingEngine.Start();
            ConsoleCommands.Start();
            ControllerInput.Start();
            Time.Start();

            //  Load main menu level / start.           
            LevelLoading.LoadFromFile("menu");

            // Sebd start message
            Debug.SendDebugMessage($"Game has started! in {DateTime.Now.Subtract(startTime).TotalMilliseconds}ms");

            // Start core game loop
            StartGameLoop();
        }

        static void StartGameLoop()
        {
            /*
             *  Core game loop
             */

            while (true)
            {
                Time.Tick();

                if (LevelLoading.Loading) continue;

                /*
                 *  Pause game if FPS is below 5. 
                 *  note; This is a hot fix. but it works.
                 */

                if (Time.FPS < 5) continue;

                TickInput();
                TickGame();
                TickBackend();
            }

        }

        static void TickInput()
        {
            InputManager.Tick();
        }

        static void TickGame()
        {
            Game.Main.Update();
        }

        static void TickBackend()
        {
            RenderingEngine.RenderFrame();
            Window.TickWindow();
            GameObject.DeleteQueue();
        }
    }
}
