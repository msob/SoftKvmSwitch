using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftKvmSwitch.Configuration
{
    internal class KeyboardShortcut
    {
        [JsonProperty(PropertyName = "Shortcut")]
        internal string Shortcut { get; set; }
    }
}
