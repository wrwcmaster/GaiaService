using PluginCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PluginManager
{
    public class PluginManagerService : IPluginManagerService
    {
        
        public void Register(PluginInfo info)
        {
            IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            info.Callback = callback;
            PluginManager.Instance.Register(info);
        }
    }
}
