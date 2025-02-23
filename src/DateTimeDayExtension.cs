using System.Diagnostics.Contracts;
using Soenneker.Enums.DayOfWeek;
using Soenneker.Enums.UnitOfTime;

namespace Soenneker.Extensions.DateTime.Day;

/// <summary>
/// Provides extension methods for <see cref="System.DateTime"/> to facilitate day-based operations.
/// This includes getting the start or end of the current, previous, or next day, with considerations for specific time zones.
/// </summary>
/// <remarks>
/// Note: These methods do not account for timezone differences unless explicitly stated. When dealing with time zones,
/// ensure you use the appropriate methods that accept a <see cref="System.TimeZoneInfo"/> parameter.
/// </remarks>
public static partial class DateTimeDayExtension
{
    /// <summary>
    /// Adjusts the given <paramref name="dateTime"/> to the start of the current day (i.e., 00:00:00 or 12:00 AM).
    /// </summary>
    /// <param name="dateTime">The datetime to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the start of the current day of the input date.</returns>
    /// <remarks>
    /// This method does not consider timezone differences. The returned datetime is in the same timezone as the input.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfDay(this System.DateTime dateTime)
    {
        return dateTime.ToStartOf(UnitOfTime.Day);
    }

    /// <summary>
    /// Adjusts the given <paramref name="dateTime"/> to the end of the current day (i.e., 23:59:59.9999999 or one tick before midnight).
    /// </summary>
    /// <param name="dateTime">The datetime to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the very end of the current day of the input date.</returns>
    /// <remarks>
    /// This method does not consider timezone differences. It effectively goes to the next day and subtracts a single tick.
    /// The returned datetime is in the same timezone as the input.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfDay(this System.DateTime dateTime)
    {
        return dateTime.ToEndOf(UnitOfTime.Day);
    }

    /// <summary>
    /// Adjusts the given <paramref name="dateTime"/> to the start of the next day.
    /// </summary>
    /// <param name="dateTime">The datetime to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the start of the day following the input date.</returns>
    /// <remarks>
    /// This method does not consider timezone differences. The returned datetime is in the same timezone as the input.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfNextDay(this System.DateTime dateTime)
    {
        return dateTime.ToStartOfDay().AddDays(1);
    }

    /// <summary>
    /// Adjusts the given <paramref name="dateTime"/> to the start of the previous day.
    /// </summary>
    /// <param name="dateTime">The datetime to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the start of the day prior to the input date.</returns>
    /// <remarks>
    /// This method does not consider timezone differences. The returned datetime is in the same timezone as the input.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfPreviousDay(this System.DateTime dateTime)
    {
        return dateTime.ToStartOfDay().AddDays(-1);
    }

    /// <summary>
    /// Extends the <see cref="System.DateTime"/> struct with a method to get the end of the previous day.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> value to calculate the end of the previous day from.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the end of the previous day (23:59:59.9999999) based on the input <paramref name="dateTime"/> value.</returns>
    /// <example>
    /// For example, if the input <paramref name="dateTime"/> is "2023-04-01 12:34:56", the method will return "2023-03-31 23:59:59.9999999".
    /// </example>
    /// <remarks>
    /// This method is marked as <c>Pure</c>, which means it has no side effects and its return value is solely determined by its input value.
    /// It uses the <see cref="ToEndOfDay"/> method to get the end of the current day, and then subtracts one day using <see cref="System.DateTime.AddDays(double)"/> to get the end of the previous day.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfPreviousDay(this System.DateTime dateTime)
    {
        return dateTime.ToEndOfDay().AddDays(-1);
    }

    /// <summary>
    /// Extends the <see cref="System.DateTime"/> struct with a method to get the end of the next day.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> value to calculate the end of the next day from.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the end of the next day (23:59:59.9999999) based on the input <paramref name="dateTime"/> value.</returns>
    /// <example>
    /// For example, if the input <paramref name="dateTime"/> is "2023-04-01 12:34:56", the method will return "2023-04-02 23:59:59.9999999".
    /// </example>
    /// <remarks>
    /// This method is marked as <c>Pure</c>, which means it has no side effects and its return value is solely determined by its input value.
    /// It uses the <see cref="ToEndOfDay"/> method to get the end of the current day, and then adds one day using <see cref="System.DateTime.AddDays(double)"/> to get the end of the next day.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfNextDay(this System.DateTime dateTime)
    {
        return dateTime.ToEndOfDay().AddDays(1);
    }

    /// <summary>
    /// Converts the given UTC datetime (<paramref name="utcNow"/>) to the timezone specified by <paramref name="tzInfo"/>, 
    /// adjusts it to the start of the current day in that timezone, then converts back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC datetime.</param>
    /// <param name="tzInfo">The timezone information to use for the conversion.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the start of the current day in the specified timezone, converted back to UTC.</returns>
    /// <remarks>
    /// This method facilitates timezone-specific datetime calculations, ensuring the output is in UTC for consistent further processing.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfTzDay(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        return utcNow.ToTz(tzInfo).ToStartOfDay().ToUtc(tzInfo);
    }

    /// <summary>
    /// Converts the given UTC datetime (<paramref name="utcNow"/>) to the timezone specified by <paramref name="tzInfo"/>, 
    /// adjusts it to the start of the previous day in that timezone, then converts back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC datetime.</param>
    /// <param name="tzInfo">The timezone information to use for the conversion.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the start of the previous day in the specified timezone, converted back to UTC.</returns>
    /// <remarks>
    /// This method is useful for adjusting datetimes across timezones and ensuring the result is in UTC.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfPreviousTzDay(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        return utcNow.ToTz(tzInfo).ToStartOfPreviousDay().ToUtc(tzInfo);
    }

    /// <summary>
    /// Converts the given UTC datetime (<paramref name="utcNow"/>) to the timezone specified by <paramref name="tzInfo"/>, 
    /// adjusts it to the start of the next day in that timezone, then converts back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC datetime.</param>
    /// <param name="tzInfo">The timezone information to use for the conversion.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the start of the next day in the specified timezone, converted back to UTC.</returns>
    /// <remarks>
    /// This method accounts for timezone differences and is useful for date calculations across timezones, with results standardized to UTC.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfNextTzDay(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        return utcNow.ToTz(tzInfo).ToStartOfNextDay().ToUtc(tzInfo);
    }

    /// <summary>
    /// Calculates the very last moment of the current day in the specified timezone (<paramref name="tzInfo"/>) from the given UTC datetime (<paramref name="utcNow"/>), then converts it back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC datetime.</param>
    /// <param name="tzInfo">The timezone information to use for the calculation.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the very last tick of the current day in the specified timezone, converted back to UTC.</returns>
    /// <remarks>
    /// Useful for end-of-day calculations across timezones. The result is adjusted to UTC to facilitate universal application.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfTzDay(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        return utcNow.ToTz(tzInfo).ToEndOfDay().ToUtc(tzInfo);
    }

    /// <summary>
    /// Calculates the very last moment of the previous day in the specified timezone (<paramref name="tzInfo"/>) from the given UTC datetime (<paramref name="utcNow"/>), then converts it back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC datetime.</param>
    /// <param name="tzInfo">The timezone information to use for the calculation.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the very last tick of the previous day in the specified timezone, converted back to UTC.</returns>
    /// <remarks>
    /// This method ensures that end-of-day times are accurately reflected across different timezones, with the final result in UTC.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfPreviousTzDay(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        return utcNow.ToTz(tzInfo).ToEndOfPreviousDay().ToUtc(tzInfo);
    }

    /// <summary>
    /// Extends the <see cref="System.DateTime"/> struct with a method to get the end of the next day in a specified time zone.
    /// </summary>
    /// <param name="utcNow">The <see cref="System.DateTime"/> value in UTC to calculate the end of the next day from.</param>
    /// <param name="tzInfo">The <see cref="System.TimeZoneInfo"/> representing the target time zone.</param>
    /// <returns>A new <see cref="System.DateTime"/> instance representing the end of the next day (23:59:59.9999999) in the specified time zone, based on the input <paramref name="utcNow"/> value.</returns>
    /// <example>
    /// For example, if the input <paramref name="utcNow"/> is "2023-04-01 12:34:56" (in UTC) and the <paramref name="tzInfo"/> is "Eastern Standard Time", the method will return a <see cref="System.DateTime"/> value representing "2023-04-02 23:59:59.9999999" in the Eastern Time Zone.
    /// </example>
    /// <remarks>
    /// This method is marked as <c>Pure</c>, which means it has no side effects and its return value is solely determined by its input values.
    /// It uses the following steps:
    /// <list type="number">
    /// <item>
    /// <description>Converts the input <paramref name="utcNow"/> value from UTC to the specified time zone using the <see cref="DateTimeExtension.ToTz(DateTime,TimeZoneInfo)"/> method.</description>
    /// </item>
    /// <item>
    /// <description>Calls the <see cref="ToEndOfNextDay"/> extension method on the converted <see cref="System.DateTime"/> value to get the end of the next day in the specified time zone.</description>
    /// </item>
    /// <item>
    /// <description>Converts the resulting <see cref="System.DateTime"/> value back to UTC using the <see cref="DateTimeExtension.ToUtc(DateTime,TimeZoneInfo)"/> method.</description>
    /// </item>
    /// </list>
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfNextTzDay(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        return utcNow.ToTz(tzInfo).ToEndOfNextDay().ToUtc(tzInfo);
    }

    /// <summary>
    /// Converts the specified <paramref name="dateTime"/> to a <see cref="DayOfWeekType"/>, which represents the day of the week.
    /// </summary>
    /// <param name="dateTime">The datetime from which to extract the day of the week.</param>
    /// <returns>A <see cref="DayOfWeekType"/> representing the day of the week for the specified datetime.</returns>
    /// <remarks>Does not consider timezone or do any conversion.</remarks>
    [Pure]
    public static DayOfWeekType ToDayOfWeekType(this System.DateTime dateTime)
    {
        return DayOfWeekType.FromValue(dateTime.DayOfWeek.ToString());
    }
}