using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.Application.Repositories;

public class InMemoryAppointmentCycleRepository : InMemoryBaseRepository<Model.AppointmentCycle>, IAppointmentCycleRepository
{
    public Task<AppointmentCycle?> GetOngoingCycleForPatient(long patientId)
    {
        var cycles = _objects.Where(x => x.PatientId == patientId && !x.CycleFull).SingleOrDefault();
        return Task.FromResult(cycles);
    }

    public Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(long doctorId)
    {
        var cycles = _objects.Where(x => x.DoctorId == doctorId && !x.CycleFull);
        return Task.FromResult(cycles);
    }
}