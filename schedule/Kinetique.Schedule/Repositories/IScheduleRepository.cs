using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Schedule.Repositories;

public interface IScheduleRepository
{
    Task<List<DoctorSchedule>> GetSchedulesForDoctorPeriod(long doctorId, DateTime startTime, DateTime endTime)
}