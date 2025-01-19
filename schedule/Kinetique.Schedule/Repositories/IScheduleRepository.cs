using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Schedule.Repositories;

public interface IScheduleRepository : IBaseRepository<DoctorSchedule>
{
    Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctorPeriod(long doctorId, DateTime startTime, DateTime endTime);
    Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctor(long doctorId);
}