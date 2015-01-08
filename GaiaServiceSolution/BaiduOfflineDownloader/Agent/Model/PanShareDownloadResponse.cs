using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.Model
{
    [DataContract]
    public class PanShareDownloadResponse
    {
        [DataMember(Name = "server_time")]
        public long ServerTime { get; set; }
        [DataMember(Name = "errno")]
        public int ErrorNumber { get; set; }
        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
        [DataMember(Name = "list")]
        public List<FileInfo> FileList { get; set; }

        [DataContract]
        public class FileInfo
        {
            [DataMember(Name = "fs_id")]
            public long FileId { get; set; }
            [DataMember(Name = "dlink")]
            public string DownloadLink { get; set; }
            [DataMember(Name = "path")]
            public string Path { get; set; }
            [DataMember(Name = "server_filename")]
            public string ServerFileName { get; set; }
            [DataMember(Name = "size")]
            public long Size { get; set; }
            [DataMember(Name = "server_mtime")]
            public long ServerModifiedTime { get; set; }
            [DataMember(Name = "server_ctime")]
            public long ServerCreatedTime { get; set; }
            [DataMember(Name = "local_mtime")]
            public long LocalModifiedTime { get; set; }
            [DataMember(Name = "local_ctime")]
            public long LocalCreatedTime { get; set; }
            [DataMember(Name = "isdir")]
            public int IsDir { get; set; }
            [DataMember(Name = "category")]
            public int Category { get; set; }
            [DataMember(Name = "md5")]
            public string MD5 { get; set; }
        }
    }
}
