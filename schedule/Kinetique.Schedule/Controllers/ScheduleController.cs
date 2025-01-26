using Kinetique.Schedule.DAL;
using Kinetique.Schedule.Dtos;
using Kinetique.Schedule.Mappers;
using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Requests;
using Kinetique.Schedule.Requests.Handlers;
using Kinetique.Schedule.Services;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Schedule.Controllers;

public class ScheduleController(DataContext context, IScheduleRepository scheduleRepository, IDoctorScheduleRequestHandler _doctorScheduleRequestHandler,
    IDoctorScheduleListHandler _doctorScheduleListHandler) : BaseController
{
    private readonly ScheduleBookingService _scheduleBookingService = new ScheduleBookingService(scheduleRepository);
    
    [HttpPost]
    public async Task<ActionResult<bool>> CanBook(BookTimeRequest request)
    {
        var schedules = context.DoctorSchedules.AsQueryable();
        var blockers = context.ScheduleBlockers.AsQueryable();

        var res = _scheduleBookingService.GetSlotsForRequestedTime(request);
        
        var schedulesFound = await schedules.Where(x => x.StartDate>= request.StartDate && x.EndDate <= request.EndDate)
            .ToListAsync();
        return false;
    }

    [HttpPost("slot")]
    public async Task<ActionResult> CreateSlots(DoctorScheduleRequest request)
    {
        await _doctorScheduleRequestHandler.Handle(request);
        return Ok();
    }

    [HttpGet("{code:string}")]
    public async Task<ActionResult<DoctorScheduleDto>> GetSlotsForDoctor(string code)
    {
        var result = await _doctorScheduleListHandler.Handle(new DoctorScheduleListQuery(code));
        return Ok(result.Select(x => x.MapToDto()).ToList());
    }
}