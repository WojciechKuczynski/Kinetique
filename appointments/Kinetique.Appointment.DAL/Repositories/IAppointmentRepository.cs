using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.DAL.Repositories;

public interface IAppointmentRepository : IBaseRepository<Model.Appointment>
{
    Task<IList<Model.Appointment>> GetAppointmentsForDoctor(long doctorId, DateTime? start = null, DateTime? end = null);
    Task<IList<Model.Appointment>> GetAppointmentsForPatient(long patientId, DateTime? start = null, DateTime? end = null);
    Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date);
}