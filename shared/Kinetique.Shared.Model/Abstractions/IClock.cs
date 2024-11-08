namespace Kinetique.Shared.Model.Abstractions;

public interface IClock
{
    DateTime GetNow();

    DateTime GetTodayMidnight();
}