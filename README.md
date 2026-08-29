[![](https://img.shields.io/nuget/v/soenneker.extensions.datetime.day.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.day/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.day/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.day/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.datetime.day.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.day/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.day/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.day/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.DateTime.Day

A collection of helpful DateTime day-based extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.DateTime.Day
```

## Quick start

```csharp
using Soenneker.Extensions.DateTime.Day;

DateTime dateTime = DateTime.UtcNow;
var result = dateTime.ToStartOfDay();
```

## Common operations

- `ToStartOfDay()` - Adjusts the given `dateTime` to the start of the current day (i.e., 00:00:00 or 12:00 AM). Returns a new `System.DateTime` instance representing the start of the current day of the input date. This method does not consider timezone differences.
- `ToEndOfDay()` - Adjusts the given `dateTime` to the end of the current day (i.e., 23:59:59.9999999 or one tick before midnight). Returns a new `System.DateTime` instance representing the very end of the current day of the input date. This method does not consider timezone differences.
- `ToStartOfNextDay()` - Adjusts the given `dateTime` to the start of the next day. Returns a new `System.DateTime` instance representing the start of the day following the input date. This method does not consider timezone differences.
- `ToStartOfPreviousDay()` - Adjusts the given `dateTime` to the start of the previous day. Returns a new `System.DateTime` instance representing the start of the day prior to the input date. This method does not consider timezone differences.
- `ToEndOfPreviousDay()` - Extends the `System.DateTime` struct with a method to get the end of the previous day. Returns a new `System.DateTime` instance representing the end of the previous day (23:59:59.9999999) based on the input `dateTime` value.
- `ToEndOfNextDay()` - Extends the `System.DateTime` struct with a method to get the end of the next day. Returns a new `System.DateTime` instance representing the end of the next day (23:59:59.9999999) based on the input `dateTime` value.
- `ToStartOfTzDay()` - Converts the given UTC datetime (`utcNow`) to the timezone specified by `tzInfo`, adjusts it to the start of the current day in that timezone, then converts back to UTC. Returns a new `System.DateTime` instance representing the start of the current day in the specified timezone, converted back to UTC.
- `ToStartOfPreviousTzDay()` - Converts the given UTC datetime (`utcNow`) to the timezone specified by `tzInfo`, adjusts it to the start of the previous day in that timezone, then converts back to UTC. Returns a new `System.DateTime` instance representing the start of the previous day in the specified timezone, converted back to UTC.
- `ToStartOfNextTzDay()` - Converts the given UTC datetime (`utcNow`) to the timezone specified by `tzInfo`, adjusts it to the start of the next day in that timezone, then converts back to UTC. Returns a new `System.DateTime` instance representing the start of the next day in the specified timezone, converted back to UTC.
- `ToEndOfTzDay()` - Calculates the very last moment of the current day in the specified timezone (`tzInfo`) from the given UTC datetime (`utcNow`), then converts it back to UTC. Returns a new `System.DateTime` instance representing the very last tick of the current day in the specified timezone, converted back to UTC. Useful for end-of-day calculations across timezones.
- `ToEndOfPreviousTzDay()` - Calculates the very last moment of the previous day in the specified timezone (`tzInfo`) from the given UTC datetime (`utcNow`), then converts it back to UTC. Returns a new `System.DateTime` instance representing the very last tick of the previous day in the specified timezone, converted back to UTC.
- `ToEndOfNextTzDay()` - Extends the `System.DateTime` struct with a method to get the end of the next day in a specified time zone. Returns a new `System.DateTime` instance representing the end of the next day (23:59:59.9999999) in the specified time zone, based on the input `utcNow` value.

The package also includes one additional operation for more specialized cases.
