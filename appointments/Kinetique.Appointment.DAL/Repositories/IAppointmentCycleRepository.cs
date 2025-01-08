using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.DAL.Repositories;

public interface IAppointmentCycleRepository : IBaseRepository<AppointmentCycle>
{
    Task<AppointmentCycle?> GetOngoingCycleForPatient(long patientId);
    Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(long doctorId);
}