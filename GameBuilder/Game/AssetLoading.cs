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
                DateTime startTime = DateTime.Now;

                Debug.SendDebugMessage("Assets not found, Downloading latest version from internet.");
                Debug.SendDebugMessage("This might take some time!");

                WebClient webClient = new WebClient();
                string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);

                webClient.DownloadFile(DownloadURL, targetPath + "\\midnight.zip");
                Debug.SendDebugMessage("Extracting data from file \"" + targetPath + "\\midnight.zip\"");
                ZipFile.ExtractToDirectory(targetPath + "\\midnight.zip", path);

                Debug.SendDebugMessage("Completed asset download in: " + DateTime.Now.Subtract(startTime).TotalMilliseconds + "ms");
            }
        }
    }
}
