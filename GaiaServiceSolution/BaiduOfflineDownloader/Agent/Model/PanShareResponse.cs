using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.Model
{
    [DataContract]
    public class PanShareResponse
    {
        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
        [DataMember(Name = "ctime")]
        public long CreatedTime { get; set; }
        [DataMember(Name = "errno")]
        public int ErrorNumber { get; set; }
        [DataMember(Name = "shareid")]
        public long ShareId { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "shorturl")]
        public string ShortUrl { get; set; }
        [DataMember(Name = "premis")]
        public bool Premis { get; set; }
    }
}
