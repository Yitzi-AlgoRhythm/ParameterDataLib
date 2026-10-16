using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ParameterDataLib.Enums;

namespace ParameterDataLib
{
    public class ParameterDataConverter : JsonConverter<ParameterData>
    {
        public override ParameterData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string name = "";
            TelemetryUnit unit = TelemetryUnit.Unit;
            double value = 0;
            ulong timestamp = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                string propertyName = reader.GetString() ?? throw new JsonException("Unexpected null reading property name");

                reader.Read();

                switch (propertyName)
                {
                    case "name":

                        name = reader.GetString() ?? throw new JsonException("Expected 'name' to be a non-null string but got null.");

                        break;

                    case "units":

                        switch (reader.GetString() ?? throw new JsonException("Expected 'Units' to be a non-null string but got null."))
                        {
                            case "HOURS":
                                unit = TelemetryUnit.Hours;
                                break;
                            case "DEG":
                                unit = TelemetryUnit.Degrees;
                                break;
                            case "FEET":
                                unit = TelemetryUnit.Feet;
                                break;
                            case "PSI":
                                unit = TelemetryUnit.PSI;
                                break;
                            case "KNOTS":
                                unit = TelemetryUnit.Knots;
                                break;
                            case "COUNTS":
                                unit = TelemetryUnit.Counts;
                                break;
                            case "LBS":
                                unit = TelemetryUnit.LBS;
                                break;
                            case "LSB":
                                unit = TelemetryUnit.LSB;
                                break;
                            case "DDM":
                                unit = TelemetryUnit.DDM;
                                break;
                            case "MMACH":
                                unit = TelemetryUnit.MMach;
                                break;
                            case "FT/MIN":
                                unit = TelemetryUnit.FtPerMin;
                                break;
                            case "MB":
                                unit = TelemetryUnit.MB;
                                break;
                            case "IN":
                                unit = TelemetryUnit.IN;
                                break;
                            case "LBS/HR":
                                unit = TelemetryUnit.LbsPerHour;
                                break;
                            case "G":
                                unit = TelemetryUnit.G;
                                break;
                            case "MACH":
                                unit = TelemetryUnit.Mach;
                                break;
                            case "%RPM":
                                unit = TelemetryUnit.RPM;
                                break;
                            case "IN/SEC":
                                unit = TelemetryUnit.InPerSec;
                                break;
                        }
                        break;

                    case "value":

                        value = reader.GetDouble();

                        break;

                    case "timestamp":

                        timestamp = reader.GetUInt64();

                        break;
                }
            }

            return new ParameterData()
            {
                Name = name,
                Units = unit,
                Value = value,
                TimestampMS = timestamp
            };
        }

        public override void Write(Utf8JsonWriter writer, ParameterData data, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("name", data.Name);

            writer.WriteString("units", data.Units switch
            {
                TelemetryUnit.Hours => "HOURS",
                TelemetryUnit.Degrees => "DEG",
                TelemetryUnit.Feet => "FEET",
                TelemetryUnit.PSI => "PSI",
                TelemetryUnit.Knots => "KNOTS",
                TelemetryUnit.Counts => "COUNTS",
                TelemetryUnit.LBS => "LBS",
                TelemetryUnit.LSB => "LSB",
                TelemetryUnit.DDM => "DDM",
                TelemetryUnit.MMach => "MMACH",
                TelemetryUnit.FtPerMin => "FT/MIN",
                TelemetryUnit.MB => "MB",
                TelemetryUnit.IN => "IN",
                TelemetryUnit.LbsPerHour => "LBS/HR",
                TelemetryUnit.G => "G",
                TelemetryUnit.Mach => "MACH",
                TelemetryUnit.RPM => "%RPM",
                TelemetryUnit.InPerSec => "IN/SEC",
                TelemetryUnit.Unit => "",
                _ => throw new JsonException($"Unexpected TelemetryUnit value: {data.Units}")
            });

            writer.WriteNumber("value", data.Value);

            writer.WriteNumber("timestamp", data.TimestampMS);

            writer.WriteEndObject();
        }
    }
}
