using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Schedule.Repositories;

public class PostgresScheduleRepository(DbContext context) : PostgresRepositoryBase<DoctorSchedule>(context), IScheduleRepository
{
    public Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctorPeriod(long doctorId, DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }
}