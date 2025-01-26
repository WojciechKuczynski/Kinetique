using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests.Handlers;

public interface IDoctorScheduleListHandler : IQueryHandler<DoctorScheduleListQuery,IEnumerable<DoctorSchedule>>;

public class ScheduleQueryHandler : IDoctorScheduleListHandler
{
    private readonly IScheduleRepository _scheduleRepository;
    public ScheduleQueryHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<IEnumerable<DoctorSchedule>> Handle(DoctorScheduleListQuery query, CancellationToken token = default)
    {
        return await _scheduleRepository.GetSchedulesForDoctor(query.DoctorCode);
    }
}