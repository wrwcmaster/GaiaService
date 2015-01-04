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
    [ServiceContract]
    public interface IPCSService
    {
        [FileMessage("Filedata")]
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/rest/2.0/pcs/file?method=upload&type=tmpfile&app_id=250528",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json
            )]
        PCSUploadResponse UploadFile(Stream file);//TODO: better input type, add stream as knowntype


    }
}
