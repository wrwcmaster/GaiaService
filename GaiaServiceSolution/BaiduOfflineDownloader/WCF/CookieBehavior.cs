using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.WCF
{
    public class CookieBehavior : IEndpointBehavior
    {
        private string cookie;

        public CookieBehavior(string cookie)
        {
            this.cookie = cookie;
        }

        public void AddBindingParameters(ServiceEndpoint serviceEndpoint,
            System.ServiceModel.Channels
            .BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint,
            System.ServiceModel.Dispatcher.ClientRuntime behavior)
        {
            behavior.MessageInspectors.Add(new CookieMessageInspector(cookie));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint,
            System.ServiceModel.Dispatcher
            .EndpointDispatcher endpointDispatcher) { }

        public void Validate(ServiceEndpoint serviceEndpoint) { }
    }
}
