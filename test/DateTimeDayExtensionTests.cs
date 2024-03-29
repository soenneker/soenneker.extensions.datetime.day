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
}
