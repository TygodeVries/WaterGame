using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Levels
{
    internal class LevelCache
    {
        static Dictionary<string, string[]> keysMapping = new Dictionary<string, string[]>();
        static Dictionary<string, Bitmap> mapMappings = new Dictionary<string, Bitmap>();

        public static string[] getMappingData(string path)
        {
            if(keysMapping.ContainsKey(path))
            {
                return keysMapping[path];
            }

            string[] s = File.ReadAllText(path).Trim().Split('\n');
            keysMapping.Add(path, s);
            return s;
        }

        public static Bitmap getLevelLayout(string path)
        {
            if (mapMappings.ContainsKey(path))
            {
                return mapMappings[path];
            }

            Bitmap s = new Bitmap(Bitmap.FromFile(path));
            mapMappings.Add(path, s);
            return s;
        }
    }
}
