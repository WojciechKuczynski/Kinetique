using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Services;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests.Handlers;

public interface IDoctorScheduleRequestHandler : ICommandHandler<DoctorScheduleRequest>;

public class ScheduleCommandHandler : IDoctorScheduleRequestHandler
{
    private readonly ScheduleBookingService _scheduleBookingService;

    public ScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleBookingService = new ScheduleBookingService(scheduleRepository);
    }
    
    public async Task Handle(DoctorScheduleRequest command, CancellationToken token = default)
    {
        await _scheduleBookingService.TryCreateDoctorSchedule(command);
    }
}