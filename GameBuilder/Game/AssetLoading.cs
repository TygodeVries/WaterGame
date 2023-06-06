using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using System.IO.Compression;

namespace GameBuilder.Game
{
    internal class AssetLoading
    {
        public static void LoadAssetsIfNotExist(string path)
        {
            string DownloadURL = "https://www.dropbox.com/s/h02kvx1d4acy428/aaaaaaa.zip?dl=1";

            if(!Directory.Exists(path))
            {
                Console.WriteLine("Assets not found, Downloading latest version.");
                WebClient webClient = new WebClient();
                string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);

                webClient.DownloadFile(DownloadURL, targetPath + "\\midnight.zip");
                Console.WriteLine("Extracting data...");
                ZipFile.ExtractToDirectory(targetPath + "\\midnight.zip", path);
            }
        }
    }
}
