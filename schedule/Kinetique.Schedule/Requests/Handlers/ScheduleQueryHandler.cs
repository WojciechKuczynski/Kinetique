using Kinetique.Schedule.Dtos;
using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Services;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests.Handlers;

public interface IDoctorScheduleListHandler : IQueryHandler<DoctorScheduleListQuery,IEnumerable<DoctorSchedule>>;
public interface IDoctorScheduleSlotHandler : IQueryHandler<DoctorScheduleSlotQuery, IEnumerable<ScheduleSlotDto>>;

public class ScheduleQueryHandler : IDoctorScheduleListHandler, IDoctorScheduleSlotHandler
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly ScheduleBookingService _scheduleBookingService;
    private readonly ILogger<ScheduleQueryHandler> _logger;
    public ScheduleQueryHandler(IScheduleRepository scheduleRepository, ILogger<ScheduleQueryHandler> logger)
    {
        _scheduleRepository = scheduleRepository;
        _scheduleBookingService = new ScheduleBookingService(scheduleRepository);
        _logger = logger;
    }
    
    public async Task<IEnumerable<DoctorSchedule>> Handle(DoctorScheduleListQuery query, CancellationToken token = default)
    {
        _logger.LogTrace("DoctorScheduleListQuery :: Handling request for doctor {DoctorCode}", query.DoctorCode);
        return await _scheduleRepository.GetSchedulesForDoctor(query.DoctorCode);
    }

    public async Task<IEnumerable<ScheduleSlotDto>> Handle(DoctorScheduleSlotQuery query, CancellationToken token = default)
    {
        var request = new BookTimeRequest
        {
            StartDate = query.StartDate,
            EndDate = query.EndDate,
            DoctorCode = query.DoctorCode
        };
        var slots = await _scheduleBookingService.GetSlotsForRequestedTime(request);
        return slots.Select(x => new ScheduleSlotDto(x.DayOfWeek, x.StartTime, x.EndTime));
    }
}