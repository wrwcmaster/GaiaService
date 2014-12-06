using PluginCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PluginManager
{
    public class PluginManager
    {
        #region Singleton
        private class SingletonHelper
        {
            static SingletonHelper() { }
            internal static readonly PluginManager _instance = new PluginManager();
        }

        public static PluginManager Instance
        {
            get
            {
                return SingletonHelper._instance;
            }
        }
        private PluginManager()
        {
            //Pre init codes come here
        }
        #endregion

        public event EventHandler<PluginInfoEventArgs> PluginRegistered;
        List<PluginInfo> _plugins = new List<PluginInfo>();

        public List<PluginInfo> Plugins
        {
            get { return _plugins; }
        }

        public void Register(PluginInfo info)
        {
            Plugins.Add(info);
            PluginRegistered.Invoke(this, new PluginInfoEventArgs() { PluginInfo = info });
        }

        public void StopPlugins()
        {
            foreach (PluginInfo info in Plugins)
            {
                info.Callback.OnCallback();
            }
        }

    }
}
