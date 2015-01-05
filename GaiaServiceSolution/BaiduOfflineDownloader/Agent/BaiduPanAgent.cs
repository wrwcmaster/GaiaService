using BaiduOfflineDownloader.Agent.ServiceDefinition;
using BaiduOfflineDownloader.WCF;
using Gaia.CommonLib.Net.Http;
using Gaia.CommonLib.Net.Http.RequestModifier;
using Gaia.CommonLib.Net.Http.ResponseParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaiduOfflineDownloader.Agent
{
    public class BaiduPanAgent
    {
        
        public string BDUSS { get; set; }
        public string BDSToken { get; set; }
        public string EncodedBDUSS { get; set; }
        public BaiduPanAgent(string bduss)
        {
            BDUSS = bduss;
        }

        public string RefreshToken()
        {
            WebClient client = new WebClient();
            client.Headers.Set("Cookie", "BDUSS=" + BDUSS);
            string content = UTF8Encoding.UTF8.GetString(client.DownloadData("http://pan.baidu.com/disk/home"));
            BDSToken = ExtractString(content, "yunData.MYBDSTOKEN = \"", "\";");
            //EncodedBDUSS = ExtractString(content, "yunData.MYBDUSS = \"", "\";");
            return BDSToken;
        }

        private string ExtractString(string content, string beginMatch, string endMatch){
            var beginIndex = content.IndexOf(beginMatch) + beginMatch.Length;
            if (beginIndex == -1) return null;
            var endIndex = content.IndexOf(endMatch, beginIndex);
            if (endIndex == -1) return null;
            int length = endIndex - beginIndex;
            return content.Substring(beginIndex, length);
        }

        public PCSUploadResponse UploadFile(FileStream stream)
        {
            PCSUploadResponse result = HttpHelper.SendRequest(new Uri("https://c.pcs.baidu.com/rest/2.0/pcs/file?method=upload&type=tmpfile&app_id=250528"), 
                HttpMethod.POST, 
                new List<IHttpRequestModifier>(){
                    new HttpRequestSingleHeaderModifier("Cookie", "BDUSS=" + BDUSS),
                    new HttpRequestMultipartFormModifier(null, new List<KeyValuePair<string, HttpRequestMultipartFormModifier.FileInfo>>(){
                        new KeyValuePair<string, HttpRequestMultipartFormModifier.FileInfo>("Filedata", new HttpRequestMultipartFormModifier.FileInfo(stream))
                    })
                },
                new HttpResponseJSONObjectParser<PCSUploadResponse>(),
                null);
            return result;
            //WebChannelFactory<IPCSService> channelFactory = new WebChannelFactory<IPCSService>(new Uri("https://c.pcs.baidu.com"));
            //channelFactory.Endpoint.EndpointBehaviors.Add(new CookieBehavior("BDUSS=" + BDUSS));
            //((WebHttpBinding)channelFactory.Endpoint.Binding).ContentTypeMapper = new JsonContentTypeMapper();
            
            //IPCSService service = channelFactory.CreateChannel();
            //PCSUploadResponse rtn = service.UploadFile(stream);
            //return rtn;
        }

        public PanCreateResponse CreateFile(string path, long size, string md5)
        {
            WebChannelFactory<IPanService> channelFactory = new WebChannelFactory<IPanService>(new Uri("http://pan.baidu.com"));
            channelFactory.Endpoint.EndpointBehaviors.Add(new CookieBehavior("BDUSS=" + BDUSS));
            ((WebHttpBinding)channelFactory.Endpoint.Binding).ContentTypeMapper = new JsonContentTypeMapper();

            IPanService service = channelFactory.CreateChannel();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("path", path);
            parameters.Add("isdir", "0");
            parameters.Add("size", size.ToString());
            parameters.Add("block_list", "[\"" + md5 + "\"]");
            MemoryStream outStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(outStream);
            StringWriter sw = new StringWriter();
            bool firstFlag = true;
            foreach (string key in parameters.Keys)
            {
                if (firstFlag)
                {
                    firstFlag = false;
                }
                else
                {
                    sw.Write("&");
                }
                string value = parameters[key];
                sw.Write(key);
                sw.Write("=");
                sw.Write(value);
            }
            string text = sw.ToString();
            writer.WriteLine(text);
            writer.Flush();

            outStream.Seek(0, SeekOrigin.Begin);
            PanCreateResponse rtn = service.CreateFile(outStream, BDSToken);
            return rtn;
        }
    }
}
