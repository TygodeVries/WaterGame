using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Rendering;
using GameBuilder.Particle;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace GameBuilder.User
{
    internal class ConsoleCommands
    {
        public static void Start()
        {
            // CONSOLE message
            Console.WriteLine("\r\n   _____ ____  _   _  _____  ____  _      ______ \r\n  / ____/ __ \\| \\ | |/ ____|/ __ \\| |    |  ____|\r\n | |   | |  | |  \\| | (___ | |  | | |    | |__   \r\n | |   | |  | | . ` |\\___ \\| |  | | |    |  __|  \r\n | |___| |__| | |\\  |____) | |__| | |____| |____ \r\n  \\_____\\____/|_| \\_|_____/ \\____/|______|______|\r\n                                                 \r\n                                                 \r\n");

            // Start thread.
            Thread thread = new Thread(ConsoleCommandThread);
            thread.Start();
        }

        static void ConsoleCommandThread()
        {
            while (true)
            {
                string Command = Console.ReadLine();

                switch (Command.ToLower().Split(' ')[0])
                {
                    case "help":
                        Command_Help();
                        break;
                    case "level":
                        Command_Level();
                        break;
                    case "loadlevel":
                        Command_LoadLevel(Command);
                        break;
                    case "reload":
                        Command_Reload();
                        break;
                    case "ping":
                        Console.WriteLine("Pong!");
                        break;
                    case "camera_debug":
                        Camera.currectCameraState = Camera.state.debug;
                        break;
                    case "camera_follow":
                        Camera.currectCameraState = Camera.state.follow;
                        break;
                    case "particle":
                        SpawnParticles();
                        break;
                    default:
                        Console.WriteLine("Unknown command. defaulting to Help");
                        Command_Help();
                        break;
                }
            }
        }

        static void SpawnParticles()
        {

        }

        static void Command_Help()
        {
            Console.WriteLine("help | Show this message");
            Console.WriteLine("level | Show info about current loaded level");
            Console.WriteLine("loadlevel [level] | load a level");
            Console.WriteLine("reload | reload the current level");
        }

        static void Command_LoadLevel(string Command)
        {
            LevelLoading.LoadFromFile(Command.Split(' ')[1]);
        }

        static void Command_Level()
        {
            Console.WriteLine("Name: -");
            Console.WriteLine("Objects: " + RenderingEngine.CurrentLoadedLevel.Objects.Count);
        }

        static void Command_Reload()
        {
            LevelLoading.LoadFromFile(LevelLoading.LastLoaded);
        }
    }
}
