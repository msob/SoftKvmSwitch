using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftKvmSwitch.Configuration
{
    internal class DeviceRemovalTrigger
    {
        [JsonProperty(PropertyName = "UsbDeviceId")]
        internal string UsbDeviceId { get; set; }
    }
}
