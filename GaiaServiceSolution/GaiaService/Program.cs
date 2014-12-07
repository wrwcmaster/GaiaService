using PluginManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GaiaService
{
    enum Platform
    {
        Windows,
        Linux,
        Mac
    }

    class Program
    {
        static void Main(string[] args)
        {
            PluginManager.PluginManager.Instance.PluginRegistered += PluginManager_PluginRegistered;
            ServiceHost host = new ServiceHost(typeof(PluginManagerService), new Uri("net.tcp://localhost:8001/"));
            host.Open();

            while (true)
            {
                
                string line = Console.ReadLine();
                Console.WriteLine();
                if(!String.IsNullOrEmpty(line)){
                    if (String.Equals(line, "exit"))
                    {
                        break;
                    }
                    else if (line.StartsWith("load "))
                    {
                        string fileName = line.Substring(5);
                        if (!String.IsNullOrEmpty(fileName))
                        {
                            fileName = fileName.Trim('"');
                            if (!File.Exists(fileName))
                            {
                                Console.WriteLine("Plugin file not found: " + fileName);
                            }
                            else
                            {
                                LoadPlugin(fileName);
                            }
                        }
                    }
                    else if (String.Equals(line, "list"))
                    {
                        PluginManager.PluginManager.Instance.Plugins.ForEach((pluginInfo) =>
                        {
                            Console.WriteLine(pluginInfo.Name);
                        });
                    }
                }
            }

            PluginManager.PluginManager.Instance.StopPlugins();
            Console.WriteLine("Closing Host");
            host.Close();
        }

        static void PluginManager_PluginRegistered(object sender, PluginInfoEventArgs e)
        {
            Console.WriteLine("Plugin registered: " + e.PluginInfo.Name);
        }

        static void LoadPlugin(string pluginAssembly)
        {
            if (!String.IsNullOrEmpty(pluginAssembly) && File.Exists(pluginAssembly))
            {
                ProcessStartInfo psi = null;
                if(RunningPlatform() == Platform.Linux){
                    psi = new ProcessStartInfo("mono", "ConsolePluginLoader.exe \"" + pluginAssembly + "\"") { UseShellExecute = false };
                }else{
                    psi = new ProcessStartInfo("ConsolePluginLoader.exe", "\"" + pluginAssembly + "\"") { UseShellExecute = false };
                }
                Process.Start(psi);
            }
        }

        static Platform RunningPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    // Well, there are chances MacOSX is reported as Unix instead of MacOSX.
                    // Instead of platform check, we'll do a feature checks (Mac specific root folders)
                    if (Directory.Exists("/Applications")
                        & Directory.Exists("/System")
                        & Directory.Exists("/Users")
                        & Directory.Exists("/Volumes"))
                        return Platform.Mac;
                    else
                        return Platform.Linux;

                case PlatformID.MacOSX:
                    return Platform.Mac;

                default:
                    return Platform.Windows;
            }
        }
    }
}
