using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParameterDataLib
{
    public class ParameterDataConverter : JsonConverter<ParameterData>
    {
        public override ParameterData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string name = "";
            DataType type = DataType.Type;
            string typeStr = "";
            TelemetryUnit unit = TelemetryUnit.Unit;
            ValueBase value = new IntValue(0);
            double timestamp = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                string propertyName = reader.GetString() ?? throw new JsonException("Unexpected null reading property name");

                reader.Read();

                switch (propertyName)
                {
                    case "name":

                        name = reader.GetString() ?? throw new JsonException("Expected 'name' to be a non-null string but got null.");

                        break;

                    case "type":

                        typeStr = reader.GetString() ?? throw new JsonException("Expected 'type' to be a non-null string but got null.");

                        if (typeStr == "double")
                        {
                            type = DataType.Double;
                        }
                        else if (typeStr == "int")
                        {
                            type = DataType.Int;
                        }
                        else
                        {
                            throw new ArgumentException("JSON object's type property was invalid");
                        }

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

                        if (type == DataType.Double)
                        {
                            value = new DoubleValue(reader.GetDouble());
                        }
                        else if (type == DataType.Int)
                        {
                            value = new IntValue(reader.GetInt32());
                        }

                        break;

                    case "timestamp":

                        timestamp = reader.GetDouble();

                        break;
                }
            }

            return new ParameterData()
            {
                Name = name,
                Type = type,
                Units = unit,
                Value = value,
                TimestampMS = timestamp
            };
        }

        public override void Write(Utf8JsonWriter writer, ParameterData data, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("name", data.Name);

            if (data.Type == DataType.Double)
            {
                writer.WriteString("type", "double");
            }
            else
            {
                writer.WriteString("type", "int");
            }

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

            if (data.Type == DataType.Double)
            {
                writer.WriteNumber("value", (data.Value as DoubleValue)!.Val);
            }
            else
            {
                writer.WriteNumber("value", (data.Value as IntValue)!.Val);
            }

            writer.WriteNumber("timestamp", data.TimestampMS);
        }
    }
}
