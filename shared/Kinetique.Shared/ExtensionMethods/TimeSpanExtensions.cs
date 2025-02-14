namespace Kinetique.Shared.ExtensionMethods;

public static class TimeSpanExtensions
{
    public static bool IsBetween(this TimeSpan checkedTime, TimeSpan start, TimeSpan end, bool withoutCheckedDate = false)
    {
        if (withoutCheckedDate)
        {
            return checkedTime > start && checkedTime < end;
        }
        
        return checkedTime >= start && checkedTime <= end;
    }
}