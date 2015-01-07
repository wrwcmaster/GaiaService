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
            string bduss = "HdZRjlDQmtKMnEtUWhJZGJoSDh1ZnZhWGYzd04wZ1RBaX5CVkNMNHVoYlp4OVJVQVFBQUFBJCQAAAAAAAAAAAEAAABIEo1NU2NvdHRUZXN0MDgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANk6rVTZOq1UO";
            BaiduPanAgent agent = new BaiduPanAgent(bduss);
            agent.RefreshToken();
            Console.WriteLine(agent.BDSToken);
            var fs = new System.IO.FileStream("test.torrent", System.IO.FileMode.Open);
            long size = fs.Length;
            var uploadResponse = agent.UploadFile(fs);
            Console.WriteLine(uploadResponse.MD5);
            var createResponse = agent.CreateFile("/test.torrent", size, uploadResponse.MD5);
            Console.WriteLine(createResponse.Path);
        }
    }
}
