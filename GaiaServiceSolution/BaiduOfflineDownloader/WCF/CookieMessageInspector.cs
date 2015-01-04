using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.WCF
{
    public class CookieMessageInspector : IClientMessageInspector
    {
        private string cookie;

        public CookieMessageInspector(string cookie)
        {
            this.cookie = cookie;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply,
            object correlationState) { }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request,
            System.ServiceModel.IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name
                , out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject
                    as HttpRequestMessageProperty;
                if (string.IsNullOrEmpty(httpRequestMessage.Headers["Cookie"]))
                {
                    httpRequestMessage.Headers["Cookie"] = cookie;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                httpRequestMessage.Headers.Add("Cookie", cookie);
                request.Properties.Add(HttpRequestMessageProperty.Name
                    , httpRequestMessage);
            }

            return null;
        }
    }
}
