using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.DAL.Repositories;

public class InMemoryAppointmentRepository : InMemoryBaseRepository<Model.Appointment>, IAppointmentRepository
{
}