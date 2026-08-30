[![](https://img.shields.io/nuget/v/soenneker.extensions.datetime.day.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.day/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.day/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.day/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.datetime.day.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.day/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.day/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.day/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.DateTime.Day

Computes current, previous, and next day boundaries for `DateTime`, either from its existing clock fields or from a UTC instant in a specified time zone.

## Installation

```bash
dotnet add package Soenneker.Extensions.DateTime.Day
```

## Clock-field boundaries

```csharp
using Soenneker.Extensions.DateTime.Day;

System.DateTime value = new(2026, 8, 29, 16, 42, 30, DateTimeKind.Utc);

System.DateTime start = value.ToStartOfDay();
System.DateTime end = value.ToEndOfDay();
System.DateTime previousStart = value.ToStartOfPreviousDay();
System.DateTime nextEnd = value.ToEndOfNextDay();
```

These methods do not perform a time-zone conversion. They operate on the date already present in the value and preserve its `Kind`.

| Method | Result |
| --- | --- |
| `ToStartOfDay()` | `00:00:00` on the same date |
| `ToEndOfDay()` | One tick before the next date |
| `ToStartOfPreviousDay()` | `00:00:00` on the previous date |
| `ToEndOfPreviousDay()` | One tick before the current date |
| `ToStartOfNextDay()` | `00:00:00` on the next date |
| `ToEndOfNextDay()` | One tick before the date after next |

## Time-zone-aware boundaries

```csharp
TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
System.DateTime utc = new(2026, 8, 29, 18, 0, 0, DateTimeKind.Utc);

System.DateTime localDayStartUtc = utc.ToStartOfTzDay(eastern);
System.DateTime localDayEndUtc = utc.ToEndOfTzDay(eastern);
```

The `...TzDay()` methods use the supplied UTC clock fields to determine the corresponding local date, calculate that date's boundary in the target zone, and return the boundary as a UTC `DateTime`.

Available methods cover the start and end of the current, previous, and next local day:

- `ToStartOfTzDay()` / `ToEndOfTzDay()`
- `ToStartOfPreviousTzDay()` / `ToEndOfPreviousTzDay()`
- `ToStartOfNextTzDay()` / `ToEndOfNextTzDay()`

If the input `Kind` is not `Utc`, its fields are treated as UTC rather than converted from the machine's local zone. Supply an actual UTC value to avoid ambiguity.

Day boundaries use local calendar math rather than fixed 24-hour durations. If local midnight falls in a daylight-saving gap, the start advances to the first valid local minute. If it is ambiguous, the earlier UTC instant is selected. An end boundary is one tick before the following valid day boundary, so 23-hour, 25-hour, and skipped local dates are handled consistently.

## Day-of-week mapping

```csharp
DayOfWeekType day = value.ToDayOfWeekType();
```

`ToDayOfWeekType()` maps `System.DayOfWeek` directly to the corresponding `Soenneker.Enums.DayOfWeek.DayOfWeekType`; it performs no time-zone conversion.
