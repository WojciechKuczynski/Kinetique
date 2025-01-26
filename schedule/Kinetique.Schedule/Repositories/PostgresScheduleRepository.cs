using Kinetique.Schedule.DAL;
using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Schedule.Repositories;

public class PostgresScheduleRepository(DataContext context) : PostgresRepositoryBase<DoctorSchedule>(context), IScheduleRepository
{
    public async Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctorPeriod(string doctorCode, DateTime startTime, DateTime endTime)
    {
        return await _objects.Where(x => x.DoctorCode == doctorCode && x.StartDate <= startTime && x.EndDate >= endTime).ToListAsync();
    }

    public async Task<IEnumerable<DoctorSchedule>> GetSchedulesForDoctor(string doctorCode)
    {
        return await _objects.Where(x => x.DoctorCode == doctorCode).ToListAsync();
    }
}