namespace BlogArray.Shared.Extensions;

public static class DateTimeManager
{
    /// <summary>
    /// Returns Current UTC DateTime
    /// </summary>
    /// <returns>UTC Date Time</returns>
    public static DateTime Now()
    {
        return DateTime.UtcNow;
    }
}