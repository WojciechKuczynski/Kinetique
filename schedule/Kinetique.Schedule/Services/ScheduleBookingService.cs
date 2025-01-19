using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Requests;
using Kinetique.Shared.ExtensionMethods;

namespace Kinetique.Schedule.Services;

public class ScheduleBookingService(IScheduleRepository _repository)
{
    public async Task<List<DoctorScheduleSlot>> GetSlotsForRequestedTime(BookTimeRequest request)
    {
        var schedulesFound = await _repository.GetSchedulesForDoctorPeriod(request.DoctorId, request.StartDate, request.EndDate);
        var requestDayOfWeek = request.StartDate.DayOfWeek;
        // schedule slots in current Datetime period for certain DayOfWeek
        var scheduleSlots = schedulesFound.SelectMany(x => x.Slots)
            .Where(x => x.DayOfWeek == requestDayOfWeek);

        // schedule slots that are between requested appointment
        var bookSlots = scheduleSlots.Where(x =>
            (x.StartTime <= request.StartDate.TimeOfDay && x.EndTime >= request.StartDate.TimeOfDay) 
            || (x.StartTime <= request.EndDate.TimeOfDay && x.EndTime >= request.EndDate.TimeOfDay)
            );

        var blocks = schedulesFound.SelectMany(x => x.Blockers)
            .Where(x => x.StartDate.IsBetween(request.StartDate,request.EndDate) 
                                  || x.EndDate.IsBetween(request.StartDate,request.EndDate));
        bookSlots = bookSlots.Where(x =>
            blocks.All(y => !x.StartTime.IsBetween(y.StartDate.TimeOfDay, y.EndDate.TimeOfDay)
                                        && !x.EndTime.IsBetween(y.StartDate.TimeOfDay, y.EndDate.TimeOfDay)));
        
        return bookSlots.ToList();
    }

    public async Task TryCreateDoctorSchedule(DoctorScheduleRequest request)
    {
        // if there is already some Schedule on same time
        var scheduleInDb =
            await _repository.GetSchedulesForDoctorPeriod(request.DoctorId, request.StartDate.Value,
                request.EndDate.Value);
        if (scheduleInDb.Any())
            throw new Exception("There is already some slot for this doctor in this period of time.");

        var schedule = new DoctorSchedule()
        {
            DoctorId = request.DoctorId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        var slots = request.Slots.Select(x => new DoctorScheduleSlot
            { DayOfWeek = x.Day, StartTime = x.StartTime, EndTime = x.EndTime });
        schedule.AddSlots(slots);

        await _repository.Add(schedule);
    }
}