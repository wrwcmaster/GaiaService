using PluginCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.ServiceModel.Description;

namespace PluginContainer
{

    public class PluginContainer : IServiceCallback
    {
        #region Singleton
        private class SingletonHelper
        {
            static SingletonHelper() { }
            internal static readonly PluginContainer _instance = new PluginContainer();
        }

        public static PluginContainer Instance
        {
            get
            {
                return SingletonHelper._instance;
            }
        }
        private PluginContainer()
        {
            //Pre init codes come here
        }
        #endregion

        public event EventHandler Stopped;

        private IPluginManagerService _managerService;
        private List<IPlugin> _pluginInstances = new List<IPlugin>();

        public void LoadPlugin(string pluginAssembily)
        {
            //_managerService = ChannelFactory<IPluginManagerService>.CreateChannel(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8001"));
            DuplexChannelFactory<IPluginManagerService> channelFactory = new DuplexChannelFactory<IPluginManagerService>(new InstanceContext(this), new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8001"));
            _managerService = channelFactory.CreateChannel();
            //_managerService = new PluginManagerProxy(this, new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8001"));
            string fileName = Path.GetFullPath(pluginAssembily);
            Assembly asm = Assembly.LoadFile(fileName);
            if (asm != null)
            {
                List<Type> pluginTypes = LoadAssembly(asm);

                if (pluginTypes.Count > 0)
                {
                    foreach (Type pluginType in pluginTypes)
                    {
                        IPlugin plugin = Activator.CreateInstance(pluginType) as IPlugin;
                        if (plugin != null)
                        {
                            InitiatePlugin(plugin);
                        }
                    }

                    //Register Plugin
                    PluginInfo info = new PluginInfo() { Name = fileName };
                    _managerService.Register(info);
                }
            }
        }

        private List<Type> LoadAssembly(Assembly asm)
        {
            List<Type> rtn = new List<Type>();
            if (asm != null)
            {
                foreach (Type t in asm.GetTypes())
                {
                    foreach (Type interfaceType in t.GetInterfaces())
                    {
                        if (interfaceType == typeof(IPlugin))
                        {
                            rtn.Add(t);
                            break;
                        }
                    }
                }
            }
            return rtn;
        }

        private void InitiatePlugin(IPlugin plugin)
        {
            _pluginInstances.Add(plugin);
            plugin.Initiate();
        }

        public void OnCallback()
        {
            Console.WriteLine("Callback received.");
            Console.WriteLine("Stopping plugin container...");
            Stopped.Invoke(this, new EventArgs());
        }
    }

}
