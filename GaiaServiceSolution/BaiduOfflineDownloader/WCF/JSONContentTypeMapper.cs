using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.WCF
{
    public class JsonContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            if (contentType.ToLower() == "text/plain" || contentType == "text/javascript" || contentType == "text/html" || contentType.Contains("application/json"))
            {
                return WebContentFormat.Json;
            }
            else
            {
                return WebContentFormat.Raw;
            }
        }
    }
}
