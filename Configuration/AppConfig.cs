using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftKvmSwitch.Configuration
{
    internal class AppConfig
    {
        [JsonProperty(PropertyName = "Events")]
        internal Event[] Events { get; set; }
    }
}
