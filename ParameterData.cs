using System.Text.Json.Serialization;

namespace ParameterDataLib
{
    [JsonConverter(typeof(ParameterDataConverter))]
    public class ParameterData
    {
        public required string Name { get; init; }

        public required DataType Type { get; init; }

        public required TelemetryUnit Units { get; init; }

        public required ValueBase Value {  get; init; }

        public required double TimestampMS { get; init; }
    }
}