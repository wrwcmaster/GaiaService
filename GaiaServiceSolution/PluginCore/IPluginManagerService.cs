using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PluginCore
{
    /// <summary>
    /// The wcf protocol for plugin manager 
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IPluginManagerService
    {
        [OperationContract(IsOneWay = true)]
        void Register(PluginInfo info);
    }

    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnCallback();
    }
}
