using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsolePluginLoader
{
    class Program
    {
        static EventWaitHandle _waitHandle = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string pluginAssembily =  args.Last();

                if (!string.IsNullOrEmpty(pluginAssembily) && File.Exists(pluginAssembily))
                {
                    PluginContainer.PluginContainer.Instance.LoadPlugin(pluginAssembily);
                    PluginContainer.PluginContainer.Instance.Stopped += PluginContainer_Stopped;
                    Console.WriteLine("Console plugin container is running");
                    _waitHandle.WaitOne();
                }
                else
                {
                    Console.WriteLine("Plugin assembily " + pluginAssembily + " not found.");
                }
            }
            else
            {
                Console.WriteLine("Plugin assembily not specified. Please use ConsolePluginLoader.exe");
            }
            Console.WriteLine("Console plugin container is exiting");
        }

        static void PluginContainer_Stopped(object sender, EventArgs e)
        {
            _waitHandle.Set();
        }
    }
}
