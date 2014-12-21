using HtmlAgilityPack;
using PluginCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DmhySourceParser
{
    public class DmhySourceParser : IPlugin
    {
        public void Initiate()
        {
            WebServiceHost host = new WebServiceHost(typeof(Service), new Uri("http://localhost:8801/"));
            host.Open();
        }
    }
}
