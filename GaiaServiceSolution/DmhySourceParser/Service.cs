
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;


namespace DmhySourceParser
{
    [ServiceContract]
    public class Service
    {
        private static DateTime _lastSyncTime = DateTime.MinValue;
        [OperationContract]
        [WebGet]
        public List<RawResource> SearchDmhyResource(string keyword)
        {
            DateTime now = DateTime.Now;
            if ((now - _lastSyncTime).TotalMinutes > 5)
            {
                _lastSyncTime = now;
                RawResourceProvider.SyncDmhyResource();
            }
            
            List<RawResource> rtn = new List<RawResource>();
            List<RawResource> result = RawResourceProvider.FindResource(keyword);
            for (int i = 0; i < 100 && i < result.Count; i++)
            {
                RawResource res = result[i];
                rtn.Add(result[i]);
            }
            return rtn;
        }

        [OperationContract]
        [WebGet]
        public Stream DownloadFile(string url)
        {
            Uri uri = new Uri(url);
            string path = uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            string fileName = Path.GetFileName(path) + ".torrent";
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
            WebOperationContext.Current.OutgoingResponse.Headers.Set("Content-Disposition", "attachment; filename=" + fileName);

            WebClient client = new WebClient();
            client.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
            client.Headers.Set("Cookie", "rsspass=59d8de300951337103b3b54a73; uid=62326");
            byte[] file = client.DownloadData(uri);
            WebOperationContext.Current.OutgoingResponse.ContentLength = file.Length;
            return new MemoryStream(file); 
        }
    }
}
