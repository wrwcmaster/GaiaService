using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.ServiceDefinition
{
    [DataContract]
    public class PanCreateResponse
    {
        [DataMember(Name = "fs_id")]
        public long FileId { get; set; }
        [DataMember(Name = "md5")]
        public string MD5 { get; set; }
        [DataMember(Name = "server_filename")]
        public string ServerFileName { get; set; }
        [DataMember(Name = "category")]
        public int Category { get; set; }
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "size")]
        public long Size { get; set; }
        [DataMember(Name = "ctime")]
        public long CreateTime { get; set; }
        [DataMember(Name = "mtime")]
        public long ModifiedTime { get; set; }
        [DataMember(Name = "isdir")]
        public int IsDir { get; set; }
        [DataMember(Name = "errno")]
        public int ErrorNumber { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
