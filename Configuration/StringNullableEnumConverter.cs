using Newtonsoft.Json;
using System;
using System.Linq;

namespace SoftKvmSwitch.Configuration
{
    internal class StringNullableEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType).IsEnum : objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullableType = IsNullableType(objectType);
            var enumType = isNullableType ? Nullable.GetUnderlyingType(objectType) : objectType;
            var enumNames = Enum.GetNames(enumType);

            // Parse JSON String to Enum
            if (reader.TokenType == JsonToken.String)
            {
                var jsonValueString = reader.Value.ToString();

                if (string.IsNullOrEmpty(jsonValueString))
                {
                    Enum.Parse(enumType, enumNames.First());
                }
                else
                {
                    try
                    {
                        return Enum.Parse(enumType, jsonValueString, true);
                    }
                    catch (Exception err)
                    {
                        throw new ArgumentException($"Could not parse JSON string to enum {enumType.Name}.", err);
                    }
                }
            }
            // Parse JSON Integer to Enum
            else if (reader.TokenType == JsonToken.Integer)
            {
                int jsonInteger = Convert.ToInt32(reader.Value);

                try
                {
                    return Enum.Parse(enumType, jsonInteger.ToString());
                }
                catch (Exception err)
                {
                    throw new ArgumentException($"Could not parse JSON string to enum {enumType.Name}.", err);
                }
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        private bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
