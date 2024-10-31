using Kinetique.Schedule.DAL;
using Kinetique.Schedule.Requests;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Schedule.Controllers;

public class ScheduleController(DataContext context) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<bool>> CanBook(BookTimeRequest request)
    {
        var schedules = context.DoctorSchedules.AsQueryable();
        var blockers = context.ScheduleBlockers.AsQueryable();

        var schedulesFound = await schedules.Where(x => x.StartDate>= request.StartDate && x.EndDate <= request.EndDate)
            .ToListAsync();

        var requestDayOfWeek = request.StartDate.DayOfWeek;
        var scheduleSlots = schedulesFound.SelectMany(x => x.Slots)
                                                                     .Where(x => x.DayOfWeek == requestDayOfWeek);

        var bookSlots = scheduleSlots.Where(x =>
                                                    x.StartTime >= request.StartDate.TimeOfDay 
                                                    && x.EndTime <= request.EndDate.TimeOfDay);
        
        
        
    }
}