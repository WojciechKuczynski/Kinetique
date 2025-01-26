using Kinetique.Schedule.Dtos;
using Kinetique.Schedule.Models;

namespace Kinetique.Schedule.Mappers;

public static class ScheduleMapper
{
    public static DoctorScheduleDto MapToDto(this DoctorSchedule schedule)
    {
        return new DoctorScheduleDto()
        {
            DoctorCode = schedule.DoctorCode,
            StartDate = schedule.StartDate,
            EndDate = schedule.EndDate,
            Slots = schedule.Slots.Select(x => new ScheduleSlotDto(x.DayOfWeek,x.StartTime,x.EndTime) ).ToList()
        };
    }
}