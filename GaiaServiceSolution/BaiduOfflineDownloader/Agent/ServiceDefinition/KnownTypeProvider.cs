using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.Agent.ServiceDefinition
{
    static class KnownTypeProvider
    {
        static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            var rtn = new List<Type>();
            rtn.Add(typeof(System.IO.MemoryStream));
            return rtn;
        }
    }
}
