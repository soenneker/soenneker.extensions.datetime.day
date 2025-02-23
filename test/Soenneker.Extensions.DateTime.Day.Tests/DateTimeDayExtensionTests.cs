using FluentAssertions;
using System;
using Soenneker.Tests.Unit;
using Soenneker.Utils.TimeZones;
using Xunit;

namespace Soenneker.Extensions.DateTime.Day.Tests;

public class DateTimeDayExtensionTests : UnitTest
{
    private readonly System.DateTime _utcNow;

    public DateTimeDayExtensionTests()
    {
        _utcNow = System.DateTime.UtcNow;
    }

    [Fact]
    public void ToStartOfTzDay_should_give_offset()
    {                               
        System.DateTime startOfDay = _utcNow.ToStartOfNextTzDay(Tz.Eastern);

        startOfDay.Hour.Should().Be(-Tz.Eastern.GetUtcOffset(startOfDay).Hours);
    }

    [Fact]
    public void ToStartOfTzDay_at_midnight()
    {
        System.DateTime midnightEasternInUtc = new(2022, 1, 1, 5, 0, 0);

        System.DateTime startOfDay = midnightEasternInUtc.ToStartOfNextTzDay(Tz.Eastern);

        startOfDay.Hour.Should().Be(-Tz.Eastern.GetUtcOffset(startOfDay).Hours);
    }

    [Fact]
    public void ToStartOfDay_kind_should_be_utc()
    {
        System.DateTime result = _utcNow.ToStartOfDay();
        result.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void ToStartOfTzDay_twice_should_equal()
    {
        System.DateTime result1 = _utcNow.ToStartOfTzDay(Tz.Eastern);
        System.DateTime result2 = result1.ToStartOfTzDay(Tz.Eastern);
        result1.Should().Be(result2);
    }

    [Fact]
    public void ToStartOfNextTzDay_kind_should_be_utc()
    {
        System.DateTime startOfDay = _utcNow.ToStartOfNextTzDay(Tz.Eastern);
        startOfDay.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void ToStartOfTzDay_kind_should_be_utc()
    {
        System.DateTime result = _utcNow.ToStartOfTzDay(Tz.Eastern);
        result.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void ToStartOfPreviousTzDay_kind_should_be_utc()
    {
        System.DateTime result = _utcNow.ToStartOfPreviousTzDay(Tz.Eastern);
        result.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void StartOfTzDay_ToEastern_hour_should_be_0()
    {
        System.DateTime startOfDay = _utcNow.ToStartOfNextTzDay(Tz.Eastern).ToTz(Tz.Eastern);

        startOfDay.Hour.Should().Be(0);
    }

    [Fact]
    public void ToStartOfPreviousTzDay_ToEastern_hour_should_be_0()
    {
        System.DateTime startOfDay = _utcNow.ToStartOfPreviousTzDay(Tz.Eastern).ToTz(Tz.Eastern);

        startOfDay.Hour.Should().Be(0);
    }

    [Fact]
    public void ToEndOfTzDay_ToEastern_should_be_2359()
    {
        System.DateTime eastern = _utcNow.ToTz(Tz.Eastern);
        System.DateTime endOfDay = _utcNow.ToEndOfTzDay(Tz.Eastern).ToTz(Tz.Eastern);

        endOfDay.Hour.Should().Be(23);
        endOfDay.Minute.Should().Be(59);
        endOfDay.Second.Should().Be(59);
        endOfDay.Date.Should().Be(eastern.Date);
    }

    [Fact]
    public void ToEndOfPreviousTzDay_kind_should_be_utc()
    {
        System.DateTime result = _utcNow.ToEndOfPreviousTzDay(Tz.Eastern);
        result.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void ToStartOfTzDay_Should_ReturnCorrectDateTime_WhenDSTChanges()
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(2023, 3, 12, 12, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 8:00 AM in Eastern Time Zone before DST start
        var expected = new System.DateTime(2023, 3, 12, 5, 0, 0, DateTimeKind.Utc); // Represents 1:00 AM in Eastern Time Zone

        // Act
        System.DateTime result = utcNow.ToStartOfTzDay(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToStartOfPreviousTzDay_Should_ReturnCorrectDateTime_WhenDSTChanges()
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(2023, 11, 5, 9, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 9:00 AM in Eastern Time Zone after DST end
        System.DateTime expected = new System.DateTime(2023, 11, 4, 0, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 1:00 AM in Eastern Time Zone

        // Act
        System.DateTime result = utcNow.ToStartOfPreviousTzDay(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToStartOfNextTzDay_Should_ReturnCorrectDateTime_WhenDSTChanges()
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(2023, 3, 12, 11, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 7:00 AM in Eastern Time Zone before DST start
        System.DateTime expected = new System.DateTime(2023, 3, 13, 0, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 1:00 AM in Eastern Time Zone after DST shift

        // Act
        System.DateTime result = utcNow.ToStartOfNextTzDay(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToEndOfTzDay_Should_ReturnCorrectDateTime_WhenDSTChanges()
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(2023, 11, 5, 7, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 7:00 AM in Eastern Time Zone after DST end
        var expected = new System.DateTime(2023, 11, 6, 4, 59, 59, 999, 999, DateTimeKind.Utc); // Represents 11:59:59.999 PM in Eastern Time Zone

        // Act
        System.DateTime result = utcNow.ToEndOfTzDay(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToEndOfPreviousTzDay_Should_ReturnCorrectDateTime_WhenDSTChanges()
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(2023, 3, 11, 7, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 7:00 AM in Eastern Time Zone before DST start
        System.DateTime expected = new System.DateTime(2023, 3, 10, 23, 59, 59, 999, 999, DateTimeKind.Utc).ToUtc(Tz.Eastern); // Represents 11:59:59.999 PM in Eastern Time Zone

        // Act
        System.DateTime result = utcNow.ToEndOfPreviousTzDay(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToEndOfNextTzDay_Should_ReturnCorrectDateTime_WhenDSTChanges()
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(2023, 11, 5, 9, 0, 0, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 9:00 AM in Eastern Time Zone after DST end
        System.DateTime expected = new System.DateTime(2023, 11, 6, 23, 59, 59, 999, 999, DateTimeKind.Unspecified).ToUtc(Tz.Eastern); // Represents 11:59:59.999 PM in Eastern Time Zone

        // Act
        System.DateTime result = utcNow.ToEndOfNextTzDay(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Theory]
    [InlineData(DayOfWeek.Sunday)]
    [InlineData(DayOfWeek.Monday)]
    [InlineData(DayOfWeek.Tuesday)]
    [InlineData(DayOfWeek.Wednesday)]
    [InlineData(DayOfWeek.Thursday)]
    [InlineData(DayOfWeek.Friday)]
    [InlineData(DayOfWeek.Saturday)]
    public void ToDayOfWeekType_ShouldReturnCorrectDayOfWeekType(DayOfWeek dayOfWeek)
    {
        // Arrange
        System.DateTime date = new System.DateTime(2024, 1, 7).Date; // Start from a known Sunday
        while (date.DayOfWeek != dayOfWeek)
        {
            date = date.AddDays(1);
        }

        // Act
        var result = date.ToDayOfWeekType();

        // Assert
        result.Should().NotBeNull();
        result.ToString().Should().Be(dayOfWeek.ToString());
    }
}
