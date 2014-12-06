using PluginCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPlugin
{
    public class DemoPlugin : IPlugin
    {
        public void Initiate()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
