using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.Model
{
    [DataContract]
    public class PanTorrentQueryResponse
    {
        [DataMember(Name = "torrent_info")]
        public TorrentInfo Info { get; set; }
        [DataMember(Name = "total")]
        public int Total { get; set; }
        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }

        [DataContract]
        public class TorrentInfo
        {
            [DataMember(Name = "sha1")]
            public string SHA1 { get; set; }
            [DataMember(Name = "file_count")]
            public int FileCount { get; set; }
            [DataMember(Name = "file_info")]
            public List<FileInfo> FileList { get; set; }
            [DataMember(Name = "real_file_count")]
            public int RealFileCount { get; set; }
            [DataMember(Name = "in_bdyy")]
            public int InBaiduYingyin { get; set; }

            [DataContract]
            public class FileInfo
            {
                [DataMember(Name = "file_name")]
                public string FileName { get; set; }
                [DataMember(Name = "size")]
                public string Size { get; set; }
            }
        }
    }


}
