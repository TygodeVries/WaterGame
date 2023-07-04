using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace GameBuilder.Audio
{
    internal class AudioPlayer
    {
        static Dictionary<string, MediaPlayer> playerTable = new Dictionary<string, MediaPlayer>();

        public static void PlayAudioSource(string audioFileName)
        {
            if(playerTable.ContainsKey(audioFileName))
            {
                playerTable[audioFileName].Play();
            }
            else
            {
                MediaPlayer player = new MediaPlayer();

                player.Open(new Uri(Program.AssetPath + "\\audio\\" + audioFileName));
                playerTable.Add(audioFileName, player);


                player.Play();
            }
        }
    }
}
