namespace Kinetique.Shared.ExtensionMethods;

public static class DateTimeExtensions
{
    public static bool IsBetween(this DateTime checkedTime, DateTime start, DateTime end, bool withoutCheckedDate = false)
    {
        if (withoutCheckedDate)
        {
            return checkedTime > start && checkedTime < end;
        }
        
        return checkedTime >= start && checkedTime <= end;
    }
}