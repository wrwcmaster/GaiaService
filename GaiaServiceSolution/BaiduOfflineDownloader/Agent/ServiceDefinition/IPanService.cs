using BaiduOfflineDownloader.WCF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.ServiceDefinition
{

    [ServiceKnownType("GetKnownTypes", typeof(KnownTypeProvider))]
    [ServiceContract]
    public interface IPanService
    {
        [UrlEncodedFormMessage]
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/api/create?a=commit&channel=chunlei&clienttype=0&web=1&bdstoken={bdstoken}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json
            )]
        PanCreateResponse CreateFile(Stream parameters, string bdstoken);//TODO: better input type, add stream as knowntype
    }
}
