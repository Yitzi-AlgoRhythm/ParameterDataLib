using System.Text.Json.Serialization;
using ParameterDataLib.Enums;

namespace ParameterDataLib
{
    [JsonConverter(typeof(ParameterDataConverter))]
    public class ParameterData
    {
        public required string Name { get; init; }

        public required TelemetryUnit Units { get; init; }

        public required double Value {  get; init; }

        public required double TimestampMS { get; init; }
    }
}