using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class PostgresAppointmentCycleRepository(DataContext context) : PostgresRepositoryBase<Model.AppointmentCycle>(context), IAppointmentCycleRepository
{
    public async Task<AppointmentCycle?> GetOngoingCycleForPatient(long patientId)
    {
        var cycles = await _objects.Where(x => x.PatientId == patientId && !x.CycleFull).SingleOrDefaultAsync();
        return cycles;
    }

    public async Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(long doctorId)
    {
        var cycles = await _objects.Where(x => x.DoctorId == doctorId && !x.CycleFull).ToListAsync();
        return cycles;
    }
}