﻿using System.Globalization;

namespace BlogArray.Shared.Helpers;

public class Converter
{
    public static int ToInt(object value, int defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return int.TryParse(valueConverted.Replace(",", ""), out int retValue) ? retValue : defaultValue;
    }

    public static int? ToInt(object value, int? defaultValue)
    {
        string valueConverted = CheckNulls(value);

        return int.TryParse(valueConverted.Replace(",", ""), out int retValue) ? retValue : defaultValue;
    }

    public static double ToDouble(object value, double defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return double.TryParse(valueConverted, out double retValue) ? retValue : defaultValue;
    }

    public static decimal ToDecimal(object value, decimal defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return decimal.TryParse(valueConverted, out decimal retValue) ? retValue : defaultValue;
    }

    public static float ToFloat(object value, float defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return float.TryParse(valueConverted, out float retValue) ? retValue : defaultValue;
    }

    public static bool ToBoolean(object value, bool defaultValue = true)
    {
        string valueConverted = CheckNulls(value);

        return bool.TryParse(valueConverted, out bool retValue) ? retValue : defaultValue;
    }

    public static string ToString(object value)
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

    public static DateTime ToDateTime(object value)
    {
        string valueConverted = CheckNulls(value);

        return DateTime.TryParse(valueConverted, out DateTime retValue) ? retValue : DateTime.Now;
    }

    public static DateTime ToDateTime(string value, DateTimeStyles style)
    {
        return DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, style, out DateTimeOffset retValue)
            ? retValue.UtcDateTime
            : DateTime.Now;
    }

    public static long ToLong(object value, long defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return long.TryParse(valueConverted, out long retValue) ? retValue : defaultValue;
    }

    public static short ToShort(object value, short defaultValue = 0)
    {
        string valueConverted = CheckNulls(value);

        return short.TryParse(valueConverted, out short retValue) ? retValue : defaultValue;
    }

    public static string CheckNulls(object value, string defaultValue = "")
    {
        return value != null ? value.ToString() : defaultValue;
    }

    public static DateTime ToDateTime(string value, string format)
    {
        string valueConverted = CheckNulls(value);

        return DateTime.TryParseExact(valueConverted, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime retValue)
            ? retValue
            : DateTime.Now;
    }
}
