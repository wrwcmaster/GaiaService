using BaiduOfflineDownloader.Agent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string bduss = "DM4aTBzTTlrS29uUkFMNkY5YmpTMEJlVlVSZn5TY2dWY0t0cm5KbEpJRWE1dFJVQVFBQUFBJCQAAAAAAAAAAAEAAABIEo1NU2NvdHRUZXN0MDgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABpZrVQaWa1UR";
            BaiduPanAgent agent = new BaiduPanAgent(bduss);
            agent.RefreshToken();
            Console.WriteLine(agent.BDSToken);
            var fs = new System.IO.FileStream("test.torrent", System.IO.FileMode.Open);
            long size = fs.Length;
            var uploadResponse = agent.UploadTempFile(fs);
            Console.WriteLine(uploadResponse.MD5);
            var createResponse = agent.CreateCloudFile("/test.torrent", size, uploadResponse.MD5);
            Console.WriteLine(createResponse.Path);
            var torrentQueryResponse = agent.QueryTorrentInfo(createResponse.Path);
            Console.WriteLine(torrentQueryResponse.Info.SHA1);
            var offlineDownloadResponse = agent.OfflineDownload(createResponse.Path, torrentQueryResponse.Info.SHA1, "/", new int[] { 1 });
        }
    }
}
