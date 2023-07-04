using GameBuilder._Math;
using GameBuilder.Audio;
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

        /// <summary>
        /// Path of the game's assets folder (Appdata\\.midnight\\assets)
        /// </summary>
        public static string AssetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.midnight\\assets";
        /// <summary>
        /// The datapath of the game's folder (Appdata\\.midnight)
        /// </summary>
        public static string DataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.midnight";
        static void Main(string[] args)
        {
            // Time that loading starts.
            DateTime startTime = DateTime.Now;
            Debug.SendDebugMessage("Welcome to midnight! Today its " + startTime.ToShortDateString() + " at " + startTime.ToShortTimeString());


            //  Create Windows
            Window.CreateWindow();
            Debug.SendDebugMessage("Waiting for window to start.");

            while (!Window.IsReady)
            {

            }

            Debug.SendDebugMessage("Detected that window is launched!");
            
            // Load assets from web.
            AssetLoading.LoadAssetsIfNotExist(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            // Start engine.
            RenderingEngine.Start();
            ControllerInput.Start();

        //    AudioPlayer.PlayAudioSource("background.mp3");

            //  Load main menu level / start.           
            LevelLoading.LoadFromFile("test_level");

            while(LevelLoading.Loading)
            {

            }

            Time.Start();

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

                ControllerInput.Tick();

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
