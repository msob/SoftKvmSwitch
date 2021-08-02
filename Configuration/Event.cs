using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftKvmSwitch.Configuration
{
    internal class Event
    {
        [JsonProperty(PropertyName = "KeyboardShortcuts", Required = Required.Default)]
        internal KeyboardShortcut[] KeyboardShortcuts { get; set; }

        [JsonProperty(PropertyName = "DeviceRemovalTriggers", Required = Required.Default)]
        internal DeviceRemovalTrigger[] DeviceRemovalTriggers { get; set; }

        [JsonProperty(PropertyName = "Actions")]
        internal Action[] Actions { get; set; }
    }
}
