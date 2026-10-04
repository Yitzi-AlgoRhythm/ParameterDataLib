using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParameterDataLib.Enums
{
    public class TelemetryUnitConverter : JsonConverter<TelemetryUnit>
    {
        private static readonly Dictionary<string, TelemetryUnit> StringToEnum = BuildLookup();
        private static readonly Dictionary<TelemetryUnit, string> EnumToString = BuildReverseLookup();

        private const TelemetryUnit FallbackValue = TelemetryUnit.Unit;

        public override TelemetryUnit Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string? rawValue = reader.GetString();

            if (rawValue is not null && StringToEnum.TryGetValue(rawValue, out TelemetryUnit result))
                return result;

            return FallbackValue;
        }

        public override void Write(
            Utf8JsonWriter writer,
            TelemetryUnit value,
            JsonSerializerOptions options)
        {
            if (EnumToString.TryGetValue(value, out string? stringValue))
                writer.WriteStringValue(stringValue);
            else
                writer.WriteStringValue(value.ToString());
        }

        private static Dictionary<string, TelemetryUnit> BuildLookup()
        {
            Dictionary<string, TelemetryUnit> map = new Dictionary<string, TelemetryUnit>(StringComparer.Ordinal);

            foreach (FieldInfo field in typeof(TelemetryUnit).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                JsonStringEnumMemberNameAttribute? attribute = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
                string key = attribute?.Name ?? field.Name;
                TelemetryUnit value = (TelemetryUnit)field.GetValue(null)!;

                map[key] = value;
            }

            return map;
        }

        private static Dictionary<TelemetryUnit, string> BuildReverseLookup()
        {
            Dictionary<TelemetryUnit, string> map = new Dictionary<TelemetryUnit, string>();

            foreach (FieldInfo field in typeof(TelemetryUnit).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                JsonStringEnumMemberNameAttribute? attribute = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
                string key = attribute?.Name ?? field.Name;
                TelemetryUnit value = (TelemetryUnit)field.GetValue(null)!;

                map[value] = key;
            }

            return map;
        }
    }
}