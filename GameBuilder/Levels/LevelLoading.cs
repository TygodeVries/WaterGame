using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using GameBuilder.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GameBuilder.Levels
{
    internal class LevelLoading
    {
        public static bool Loading = false;


        public static string LastLoaded = "null";
        public static void UnloadActiveLevel()
        {

            int unloaded = 0;

            if (RenderingEngine.CurrentLoadedLevel != null)
            {
                List<GameObject> gameObjects = RenderingEngine.CurrentLoadedLevel.Objects;
                
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    unloaded += 1;
                    GameObject.Destroy(gameObjects[i]);
                }

                gameObjects.Clear();
                RenderingEngine.CurrentLoadedLevel.Objects.Clear();
                RenderingEngine.CurrentLoadedLevel = null;
            }

            Debug.SendDebugMessage($"Unloading level [1 / 4] - Unloaded Objects ({unloaded})");
            
            PhysicsEngine.Unload();
            Debug.SendDebugMessage("Unloading level [3 / 4] - Unloaded Physics");

            SpriteManager.UnloadSprites();
            Debug.SendDebugMessage("Unloading level [4 / 4] - Unloaded Sprite Manager");
        }

        static List<Mapping> loadMappings(string path)
        {
            List<Mapping> mappings = new List<Mapping>();

            string[] lines = LevelCache.getMappingData(path);


            int readIndex = 0;
            while(readIndex < lines.Length)
            {
                string line = lines[readIndex].Trim();
                string[] args = line.Split(' ');
                Mapping mapping = new Mapping();

                mapping.color = Color.FromArgb(255, int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));

                mapping._object = args[4].Trim();

                readIndex++;
                if (readIndex == lines.Length)
                {
                    mappings.Add(mapping);
                    Console.WriteLine("Loaded mapping for: " + mapping._object);
                    continue;
                }

                line = lines[readIndex].Trim();

                Dictionary<string, string> options = new Dictionary<string, string>();

                while (line.StartsWith("-"))
                {
                    options.Add(line.Split(' ')[0].Replace("-", ""), line.Split(' ')[2]);

                    readIndex++;

                    if (readIndex == lines.Length)
                    {
                        break;
                    }
                    line = lines[readIndex].Trim();
                }

                mapping.options = options;

                Console.WriteLine("Loaded mapping for: " + mapping._object);

                mappings.Add(mapping);
            }


            return mappings;
        }

        public static void LoadFromFile(string path)
        {
            if (Loading)
            {
                Console.WriteLine("Already loading, so skipping the load request.");
                return;
            }

            Loading = true;
            LastLoaded = path;

            DateTime loadingStartTime = DateTime.Now;
            Camera.currectCameraState = Camera.state.off;

            int playerX = 0;
            int playerY = 0; 

            // Unloading
            UnloadActiveLevel();

            // # Loading new level
            string mappingPath = Program.AssetPath + "\\level\\" + path + ".mapping";
            string levelDataPath = Program.AssetPath + "\\level\\" + path + ".png";

            RenderingEngine.CurrentLoadedLevel = new Level();
            // Load mappings
            Debug.SendDebugMessage("Loading mappings...");

            List<Mapping> mappings = loadMappings(mappingPath);

            Bitmap levelbitmap = LevelCache.getLevelLayout(levelDataPath);

            for(int x = 0; x < levelbitmap.Width; x++)
            {
                for (int y = 0; y < levelbitmap.Height; y++)
                {
                    Color color = levelbitmap.GetPixel(x, y);

                    foreach(Mapping mapping in mappings)
                    {
                        if(mapping.color.ToArgb() != color.ToArgb())
                        {
                            continue;
                        }

                        if (mapping._object == "ground")
                        {
                            Color topColor = levelbitmap.GetPixel(x, y - 1);
                            if (topColor != null && topColor.ToArgb() == mapping.color.ToArgb()) ObjectLoader.LoadGroundAt(x, y, mapping.options["type"], false);
                            else
                            {
                                ObjectLoader.LoadGroundAt(x, y, mapping.options["type"], true);
                            }
                        }

                        if (mapping._object == "spike")
                        {
                            ObjectLoader.LoadSpikeAt(x, y);
                        }


                        if (mapping._object == "npc")
                        {
                            ObjectLoader.LoadNpcAt(x, y, mapping.options["version"]);
                        }

                        if (mapping._object == "coin")
                        {
                            ObjectLoader.LoadCoinAt(x, y);
                        }

                        if (mapping._object == "filler")
                        {
                            ObjectLoader.LoadFillerAt(x, y, mapping.options["type"]);
                        }

                        if (mapping._object == "player")
                        {
                            Console.WriteLine("Loading player..");
                            ObjectLoader.LoadPlayerAt(x, y);
                            playerX = x;
                            playerY = y;
                        }

                        if(mapping._object == "bounce")
                        {
                            ObjectLoader.LoadBouncePadAt(x, y);
                        }
                    }
                }
            }

            Time.Tick();
            Time.Tick();
            Time.Tick();


            Thread.Sleep(50);
            Camera.currectCameraState = Camera.state.follow;

            Console.WriteLine("Finished loading in " + DateTime.Now.Subtract(loadingStartTime).TotalMilliseconds + "ms");
            Loading = false;


            return;
        }
    }

    class Mapping
    {
        public Color color;

        public string _object;

        /// <summary>
        /// (Mapping / Setting) : (Value)
        /// </summary>
        public Dictionary<string, string> options;
    }
}
