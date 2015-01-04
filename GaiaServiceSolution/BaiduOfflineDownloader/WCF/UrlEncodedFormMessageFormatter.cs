using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaiduOfflineDownloader.WCF
{
    public class UrlEncodedFormMessageFormatter : IClientMessageFormatter
    {
        private IClientMessageFormatter _defaultFormatter;

        public UrlEncodedFormMessageFormatter(IClientMessageFormatter originalFormatter)
        {
            this._defaultFormatter = originalFormatter;
        }

        public object DeserializeReply(Message message, object[] parameters)
        {
            return this._defaultFormatter.DeserializeReply(message, parameters);
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
        {
            if (parameters.Length > 0 && parameters[0] is Stream)
            {
                Stream steam = parameters[0] as Stream;
                Message request = this._defaultFormatter.SerializeRequest(messageVersion, parameters);

                HttpRequestMessageProperty http = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                http.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                http.Headers.Remove(HttpRequestHeader.Expect);
                http.Headers.Set(HttpRequestHeader.ContentLength, steam.Length.ToString());
                return request;
            }
            else
            {
                return this._defaultFormatter.SerializeRequest(messageVersion, parameters);
            }
        }
    }

}
