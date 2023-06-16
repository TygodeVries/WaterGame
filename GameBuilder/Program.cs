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
            Window.CreateWindow();

            Debug.SendDebugMessage("Waiting for window to start.");

            while (!Window.IsReady)
            {

            }

            AssetLoading.LoadAssetsIfNotExist(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            // Start controller thread
            Thread thr = new Thread(ControllerInput.LookForControllers);
            thr.Start();

            RenderingEngine.Start();
            ConsoleCommands.Start();

            LevelLoading.LoadFromFile("level_one");


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
                    }

                    if (Time.FPS > 5 && !LevelLoading.Loading)
                    {   // Tick Input
                        InputManager.Tick();
                        // Tick game
                        Game.Main.Update();
                        // Render Frame
                        RenderingEngine.RenderFrame();
                        // Update Window
                        Window.TickWindow();
                    }

                    GameObject.DeleteQueue();
                        
                    Console.Title = $"FPS: {Time.FPS}, Rigidbodies: {PhysicsEngine.bodies.Count}";
                }
            }
        }
    }
}
