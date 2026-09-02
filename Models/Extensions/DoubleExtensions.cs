using System.Globalization;

namespace Models.Extensions;

public static class DoubleExtensions
{
    public static string ToPercentString(this double value)
    {
        return (value * 100).ToString("0.##") + "%";
    }
}