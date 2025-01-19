namespace Kinetique.Schedule.Dtos;

[Serializable]
public record ScheduleSlotDto(DayOfWeek Day, TimeSpan StartTime, TimeSpan EndTime);
