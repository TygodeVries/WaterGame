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


      

    static void Main(string[] args)
        {
            if (InputManager.UseController)
            {
                // Start controller thread
                Thread thr = new Thread(ControllerInput.LookForControllers);
                thr.Start();
            }

            RenderingEngine.Start();

            Console.WriteLine("");

            Debug.SendDebugMessage("Loading assets from web...");
            // TODO
            Debug.SendDebugMessage("Unable to download files, using cache");

            ConsoleCommands.Start();

            LevelLoading.LoadFromFile("test_level");

            Window.CreateWindow();

            Debug.SendDebugMessage("Waiting for window to start.");

            while (!Window.IsReady)
            {

            }

            Debug.SendDebugMessage("Window has started.");

            Debug.SendDebugMessage("Running game Start() Function.");

            // Make sure timer is running correctly
            Time.Tick();
            Time.Tick();

            Game.Main.Start();
            Debug.SendDebugMessage("DONE!");

            while (true)
            {
                if (!Game.Main.paused)
                {
                    if (!LevelLoading.Loading)
                    {
                        // Update Inputs
                        Time.Tick();

                        // Tick game
                        Game.Main.Update();
                    }
                    // Render Frame
                    RenderingEngine.RenderFrame();
                    // Update Window
                    Window.TickWindow();

                    Console.Title = $"FPS: {Time.FPS}, Rigidbodies: {PhysicsEngine.bodies.Count}";
                }
            }
        }
    }
}
