using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PluginCore
{
    [DataContract]
    public class PluginInfo
    {
        /// <summary>
        /// Plugin Name
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        public IServiceCallback Callback
        {
            get;
            set;
        }
    }
}
