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
            Console.WriteLine(torrentQueryResponse.Info.FileList[0].FileName);
            var offlineDownloadResponse = agent.OfflineDownload(createResponse.Path, torrentQueryResponse.Info.SHA1, "/", new int[] { 1 });
            Console.WriteLine(offlineDownloadResponse.TaskId);

            var fileName = torrentQueryResponse.Info.FileList[0].FileName;
            var searchResponse = agent.Search(fileName, 1, 100);
            long fileId = searchResponse.ResultList[0].FileId;
            Console.WriteLine(fileId);

            var shareResponse = agent.Share(fileId);
            Console.WriteLine(shareResponse.Link);
            var link = shareResponse.Link;

            var getDirectLinkResponse = agent.GetDirectDownloadLink(link);
            var directLink = getDirectLinkResponse.FileList[0].DownloadLink;
            Console.WriteLine(directLink);

            var tempFileInfo = agent.Download(directLink);
            if (File.Exists(tempFileInfo.PreferredName))
            {
                File.Delete(tempFileInfo.PreferredName);
            }
            File.Move(tempFileInfo.TempFilePath, tempFileInfo.PreferredName);
        }
    }
}
