using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.DAL.Repositories;

public interface IAppointmentRepository : IBaseRepository<AppointmentCycle>
{
    Task<IList<Model.Appointment>> GetAppointmentsForDoctor(long doctorId, DateTime? start = null, DateTime? end = null);
    Task<IList<Model.Appointment>> GetAppointmentsForPatient(long patientId, DateTime? start = null, DateTime? end = null);
    Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date);
    Task<Model.Appointment?> GetById(long id);
    Task<AppointmentCycle?> GetOngoingCycleForPatient(long patientId);
    Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(long doctorId);
}