using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Schedule.Tests;

public class InMemoryScheduleRepository : InMemoryBaseRepository<DoctorSchedule>, IScheduleRepository
{
    public Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctorPeriod(string doctorCode, DateTime startTime, DateTime endTime)
    {
        return Task.FromResult(_objects.Where(x => x.DoctorCode == doctorCode && x.StartDate <= startTime && x.EndDate >= endTime));
    }

    public Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctor(string doctorCode)
    {
        throw new NotImplementedException();
    }
}