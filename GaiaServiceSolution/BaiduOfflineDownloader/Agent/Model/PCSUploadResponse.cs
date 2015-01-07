using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.ServiceDefinition
{
    [DataContract]
    public class PCSUploadResponse
    {
        [DataMember(Name = "md5")]
        public string MD5 { get; set; }
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; }
    }
}
