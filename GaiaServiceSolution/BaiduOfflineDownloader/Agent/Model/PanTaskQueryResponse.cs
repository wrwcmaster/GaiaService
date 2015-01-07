using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.Model
{
    [DataContract]
    public class PanTaskQueryResponse
    {
        [DataMember(Name = "task_info")]
        public List<TaskInfo> TaskList { get; set; }
        [DataMember(Name = "total")]
        public int Total { get; set; }
        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }

        [DataContract]
        public class TaskInfo
        {
            [DataMember(Name = "task_id")]
            public string TaskId { get; set; }
            [DataMember(Name = "od_type")]
            public string OdType { get; set; }
            [DataMember(Name = "source_url")]
            public string SourceUrl { get; set; }
            [DataMember(Name = "save_path")]
            public string SavePath { get; set; }
            [DataMember(Name = "rate_limit")]
            public string RateLimit { get; set; }
            [DataMember(Name = "timeout")]
            public string Timeout { get; set; }
            [DataMember(Name = "callback")]
            public string Callback { get; set; }
            [DataMember(Name = "status")]
            public string Status { get; set; }
            [DataMember(Name = "create_time")]
            public string CreateTime { get; set; }
            [DataMember(Name = "task_name")]
            public string TaskName { get; set; }
        }
    }


}
