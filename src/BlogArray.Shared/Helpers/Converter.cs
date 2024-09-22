using System.Globalization;

namespace BlogArray.Shared.Helpers;

public class Converter
{
    /// <summary>
    /// Converts the object to an integer. If the conversion fails, returns the specified default value.
    /// </summary>
    public static int ToInt(object? value, int defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return int.TryParse(valueConverted.Replace(",", ""), out int retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a nullable integer. If the conversion fails, returns the specified default value.
    /// </summary>
    public static int? ToInt(object? value, int? defaultValue)
    {
        string valueConverted = CheckNulls(value);

        return int.TryParse(valueConverted.Replace(",", ""), out int retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a double. If the conversion fails, returns the specified default value.
    /// </summary>
    public static double ToDouble(object? value, double defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return double.TryParse(valueConverted, out double retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a decimal. If the conversion fails, returns the specified default value.
    /// </summary>
    public static decimal ToDecimal(object? value, decimal defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return decimal.TryParse(valueConverted, out decimal retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a float. If the conversion fails, returns the specified default value.
    /// </summary>
    public static float ToFloat(object? value, float defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return float.TryParse(valueConverted, out float retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a boolean. If the conversion fails, returns the specified default value.
    /// </summary>
    public static bool ToBoolean(object? value, bool defaultValue = true)
    {
        string valueConverted = CheckNulls(value);

        return bool.TryParse(valueConverted, out bool retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a string. If the object is null, returns an empty string.
    /// </summary>
    public static string ToString(object? value)
    {
        try
        {
            return value?.ToString() ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Converts the object to a DateTime. If the conversion fails, returns the current DateTime.
    /// </summary>
    public static DateTime ToDateTime(object? value)
    {
        string valueConverted = CheckNulls(value);

        return DateTime.TryParse(valueConverted, out DateTime retValue) ? retValue : DateTime.Now;
    }

    /// <summary>
    /// Converts the string to a DateTime using the specified DateTimeStyles. If the conversion fails, returns the current DateTime.
    /// </summary>
    public static DateTime ToDateTime(string value, DateTimeStyles style)
    {
        return DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, style, out DateTimeOffset retValue)
            ? retValue.UtcDateTime
            : DateTime.Now;
    }

    /// <summary>
    /// Converts the object to a long. If the conversion fails, returns the specified default value.
    /// </summary>
    public static long ToLong(object? value, long defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return long.TryParse(valueConverted, out long retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Converts the object to a short. If the conversion fails, returns the specified default value.
    /// </summary>
    public static short ToShort(object? value, short defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return short.TryParse(valueConverted, out short retValue) ? retValue : defaultValue;
    }

    /// <summary>
    /// Returns the string representation of the value, or the specified default value if null.
    /// </summary>
    public static string CheckNulls(object? value, string defaultValue = "")
    {
        return value != null ? value.ToString() : defaultValue;
    }

    /// <summary>
    /// Converts the string to a DateTime using the specified format. If the conversion fails, returns the current DateTime.
    /// </summary>
    public static DateTime ToDateTime(string value, string format)
    {
        string valueConverted = CheckNulls(value);

        return DateTime.TryParseExact(valueConverted, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime retValue)
            ? retValue
            : DateTime.Now;
    }
}
