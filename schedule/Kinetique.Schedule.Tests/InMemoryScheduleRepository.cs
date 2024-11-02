using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Schedule.Tests;

public class InMemoryScheduleRepository : InMemoryBaseRepository<DoctorSchedule>, IScheduleRepository
{
    public Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctorPeriod(long doctorId, DateTime startTime, DateTime endTime)
    {
        return Task.FromResult(_objects.Where(x => x.DoctorId == doctorId && x.StartDate <= startTime && x.EndDate >= endTime));
    }
}