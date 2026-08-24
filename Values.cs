using System;
using System.Collections.Generic;
using System.Text;

namespace ParameterDataLib
{
    public abstract record ValueBase;

    public record IntValue(int Val) : ValueBase;

    public record DoubleValue(double Val) : ValueBase;
}
