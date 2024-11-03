namespace Kinetique.Shared.Extensions;

public static class TimeSpanExtensions
{
    public static bool IsBetween(this TimeSpan checkedTime, TimeSpan start, TimeSpan end)
    {
        return checkedTime >= start && checkedTime <= end;
    }
}