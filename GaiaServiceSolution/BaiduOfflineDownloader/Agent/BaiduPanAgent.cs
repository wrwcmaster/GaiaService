using BaiduOfflineDownloader.Agent.ServiceDefinition;
using BaiduOfflineDownloader.WCF;
using Gaia.CommonLib;
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
                    new HttpRequestSimpleHeaderModifier("Cookie", "BDUSS=" + BDUSS),
                    new HttpRequestMultipartFormModifier(null, new KeyValuePairList<string, HttpRequestMultipartFormModifier.FileInfo>(){
                        { "Filedata", new HttpRequestMultipartFormModifier.FileInfo(stream) }
                    })
                },
                new HttpResponseJSONObjectParser<PCSUploadResponse>(),
                null);
            return result;
        }

        public PanCreateResponse CreateFile(string path, long size, string md5)
        {
            KeyValuePairList<string, string> parameters = new KeyValuePairList<string, string>(){
                { "path", path },
                { "isdir", "0" },
                { "size", size.ToString() },
                { "block_list", "[\"" + md5 + "\"]" }
            };

            PanCreateResponse result = HttpHelper.SendRequest(new Uri("http://pan.baidu.com/api/create?a=commit&channel=chunlei&clienttype=0&web=1"),
                HttpMethod.POST,
                new List<IHttpRequestModifier>(){
                    new HttpRequestSimpleUriModifier("bdstoken", BDSToken),
                    new HttpRequestSimpleHeaderModifier("Cookie", "BDUSS=" + BDUSS),
                    new HttpRequestUrlEncodedFormModifier(parameters)
                },
                new HttpResponseJSONObjectParser<PanCreateResponse>(),
                null);
            return result;
        }
    }
}
