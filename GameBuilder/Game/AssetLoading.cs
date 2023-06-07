using System;

using System.IO;
using System.Net;
using System.IO.Compression;
using System.Threading;
namespace GameBuilder.Game
{
    internal class AssetLoading
    {
        public static bool dontUpdate = true;

        public static void LoadAssetsIfNotExist(string path)
        {
            if (dontUpdate) return;


            string DownloadURL = "https://www.dropbox.com/s/p1auy9hl7w2atqc/.midnight.zip?dl=1";

            if(!Directory.Exists(path + "\\.midnight"))
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

                Thread.Sleep(1000);
            }
        }
    }
}
