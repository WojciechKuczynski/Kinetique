using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.Application.Repositories;

public class InMemoryAppointmentRepository : InMemoryBaseRepository<Model.Appointment>, IAppointmentRepository
{
    
}