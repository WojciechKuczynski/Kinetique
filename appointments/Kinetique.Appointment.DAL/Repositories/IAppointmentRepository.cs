using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.DAL.Repositories;

public interface IAppointmentRepository : IBaseRepository<Model.Appointment>
{
    public IList<Model.Appointment> GetAppointmentsForDoctor(long doctorId, DateTime? start = null, DateTime? end = null);
    public IList<Model.Appointment> GetAppointmentsForPatient(long patientId, DateTime? start = null, DateTime? end = null);
}