using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftKvmSwitch.Configuration
{
    internal class Action
    {
        [JsonProperty(PropertyName = "Device")]
        internal string Device { get; set; }

        [JsonProperty(PropertyName = "VideoSource")]
        internal VideoSources VideoSource { get; set; }
    }
}
