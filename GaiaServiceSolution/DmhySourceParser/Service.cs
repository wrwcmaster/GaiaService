
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;


namespace DmhySourceParser
{
    [ServiceContract]
    public class Service
    {
        private static DateTime _lastSyncTime = DateTime.MinValue;
        [OperationContract]
        [WebGet]
        public List<RawResource> SearchDmhyResource(string keyword)
        {
            DateTime now = DateTime.Now;
            if ((now - _lastSyncTime).TotalMinutes > 5)
            {
                _lastSyncTime = now;
                RawResourceProvider.SyncDmhyResource();
            }
            
            List<RawResource> rtn = new List<RawResource>();
            List<RawResource> result = RawResourceProvider.FindResource(keyword);
            for (int i = 0; i < 100 && i < result.Count; i++)
            {
                rtn.Add(result[i]);
            }
            return rtn;
        }
    }
}
