namespace Shared.Services;

public static class DatetimeConverter
{
    public static string Write(this DateTime date)
    {
        var day = date.Day;
        var monthNumber = date.Month;
        var month = "";
        var year = date.Year;

        switch (monthNumber)
        {
            case 1:
                month = "January";
                break;
            case 2:
                month = "February";
                break;
            case 3:
                month = "March";
                break;
            case 4:
                month = "April";
                break;
            case 5:
                month = "May";
                break;
            case 6:
                month = "June";
                break;
            case 7:
                month = "July";
                break;
            case 8:
                month = "August";
                break;
            case 9:
                month = "September";
                break;
            case 10:
                month = "October";
                break;
            case 11:
                month = "November";
                break;
            case 12:
                month = "December";
                break;
            default:
                break;
        }

        return $"{day} {month} {year}";
    }

    public static string GetTimeSpan(this DateTime? dateTime)
    {
        var localeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        var timespan = localeNow - dateTime;
        var dif = timespan.Value;

        List<string> parts = new List<string>();

        if (dif.Days > 0)
        {
            parts.Add($"{dif.Days} day");
        }
        if (dif.Hours > 0)
        {
            parts.Add($"{dif.Hours} hours");
        }
        if (dif.Minutes > 0)
        {
            parts.Add($"{dif.Minutes} minutes");
        }
        if (dif.Seconds > 0)
        {
            parts.Add($"{dif.Seconds} seconds");
        }

        return string.Join(", ", parts);
    }
}
