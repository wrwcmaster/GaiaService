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
            string bduss = ""
            BaiduPanAgent agent = new BaiduPanAgent(bduss);
            agent.RefreshToken();
            Console.WriteLine(agent.BDSToken);
            var fs = new System.IO.FileStream("/home/scottwu/AlphaDiscLog.txt", System.IO.FileMode.Open);
            long size = fs.Length;
            var uploadResponse = agent.UploadFile(fs);
            Console.WriteLine(uploadResponse.MD5);
            var createResponse = agent.CreateFile("/AlphaDiscLog.txt", size, uploadResponse.MD5);
            Console.WriteLine(createResponse.Path);
        }
    }
}
