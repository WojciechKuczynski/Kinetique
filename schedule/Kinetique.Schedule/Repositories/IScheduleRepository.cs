using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Schedule.Repositories;

public interface IScheduleRepository : IBaseRepository<DoctorSchedule>
{
    Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctorPeriod(string doctorCode, DateTime startTime, DateTime endTime);
    Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctor(string doctorCode);
}