using Newtonsoft.Json;

namespace SoftKvmSwitch.Configuration
{
    [JsonConverter(typeof(StringNullableEnumConverter))]
    internal enum VideoSources
    {
        // Default Value
        Unknown = 0x00,

        VGA1 = 0x01,
        VGA2 = 0x02,
        DVI1 = 0x03,
        DVI2 = 0x04,
        CompositeVideo1 = 0x05,
        CompositeVideo2 = 0x06,
        SVideo1 = 0x07,
        SVideo2 = 0x08,
        Tuner1 = 0x09,
        Tuner2 = 0x0A,
        Tuner3 = 0x0B,
        ComponentVideo1 = 0x0C,
        ComponentVideo2 = 0x0D,
        ComponentVideo3 = 0x0E,
        DisplayPort1 = 0x0F,
        DisplayPort2 = 0x10,
        HDMI1 = 0x11,
        HDMI2 = 0x12
    }
}
