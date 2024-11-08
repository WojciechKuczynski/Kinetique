using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Shared;

public class UtcClock : IClock
{
    public DateTime GetNow() => DateTime.UtcNow;

    public DateTime GetTodayMidnight()
    {
        var now = DateTime.UtcNow;
        now = now.AddHours(-1 * now.Hour).AddMinutes(-1 * now.Minute).AddSeconds(-1 * now.Second);
        return now;
    }
}