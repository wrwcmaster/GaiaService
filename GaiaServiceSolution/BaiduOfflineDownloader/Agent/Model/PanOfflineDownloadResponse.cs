using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.Model
{
    [DataContract]
    public class PanOfflineDownloadResponse
    {
        [DataMember(Name = "task_id")]
        public string TaskId { get; set; }
        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
        [DataMember(Name = "rapid_download")]
        public int RapidDownload { get; set; }
    }
}
