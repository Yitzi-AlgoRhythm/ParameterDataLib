using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ParameterDataLib.Enums
{
    [JsonConverter(typeof(TelemetryUnitConverter))]
    public enum TelemetryUnit
    {
        [JsonStringEnumMemberName("HOURS")]
        Hours,
        [JsonStringEnumMemberName("DEG")]
        Degrees,
        [JsonStringEnumMemberName("FEET")]
        Feet,
        [JsonStringEnumMemberName("PSI")]
        PSI,
        [JsonStringEnumMemberName("KNOTS")]
        Knots,
        [JsonStringEnumMemberName("COUNTS")]
        Counts,
        [JsonStringEnumMemberName("LBS")]
        LBS,
        [JsonStringEnumMemberName("LSB")]
        LSB,
        [JsonStringEnumMemberName("DDM")]
        DDM,
        [JsonStringEnumMemberName("MMACH")]
        MMach,
        [JsonStringEnumMemberName("FT/MIN")]
        FtPerMin,
        [JsonStringEnumMemberName("MB")]
        MB,
        [JsonStringEnumMemberName("IN")]
        IN,
        [JsonStringEnumMemberName("LBS/HR")]
        LbsPerHour,
        [JsonStringEnumMemberName("G")]
        G,
        [JsonStringEnumMemberName("MACH")]
        Mach,
        [JsonStringEnumMemberName("%RPM")]
        RPM,
        [JsonStringEnumMemberName("IN/SEC")]
        InPerSec,
        [JsonStringEnumMemberName("")]
        Unit
    }
}
