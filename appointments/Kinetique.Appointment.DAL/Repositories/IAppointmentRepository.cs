using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;
using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.DAL.Repositories;

public interface IAppointmentRepository : IBaseRepository<AppointmentCycle>
{
    Task<IList<Model.Appointment>> GetAppointmentsForDoctor(string doctorCode, DateTime? start = null, DateTime? end = null);
    Task<IList<Model.Appointment>> GetAppointmentsForPatient(Pesel patientPesel, DateTime? start = null, DateTime? end = null);
    Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date);
    Task<Model.Appointment?> GetById(long id);
    Task RemoveAppointment(Model.Appointment appointment);
    Task<AppointmentCycle?> GetOngoingCycleForPatient(Pesel patientPesel);
    Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(string doctorCode);
    Task<AppointmentCycle> AddOrUpdate(AppointmentCycle cycle);
}