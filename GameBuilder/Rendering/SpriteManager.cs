using GameBuilder.Game;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameBuilder.Rendering
{
    internal class SpriteManager
    {
        static Dictionary<string, Sprite> Imagecache = new Dictionary<string, Sprite>();

        public static void UnloadSprites()
        {
            Imagecache.Clear();
            Imagecache = new Dictionary<string, Sprite>();
        }

        public static Sprite loadSprite(string path)
        {
            try
            {
                if (Imagecache.ContainsKey(path))
                {
                    return Imagecache[path];
                }

                Sprite sprite = new Sprite();
                sprite.image = Image.FromFile(Program.AssetPath + "\\sprite\\" + path);

                Imagecache.Add(path, sprite);

                return sprite;
            }
            catch (Exception e)
            {
                Debug.SendFatalErrorMessage("Error loading sprite! \n" + e);
                return null;
            }

        }

        // used if a direct path is used, and not an custom one.
        public static Sprite loadSpriteRaw(string Directpath)
        {
            try
            {
                if (Imagecache.ContainsKey(Directpath))
                {
                    return Imagecache[Directpath];
                }

                Sprite sprite = new Sprite();
                sprite.image = Image.FromFile(Directpath);

                Imagecache.Add(Directpath, sprite);

                return sprite;
            }
            catch (Exception e)
            {
                Debug.SendFatalErrorMessage("Error loading sprite! \n" + e);
                return null;
            }

        }

    }
}
