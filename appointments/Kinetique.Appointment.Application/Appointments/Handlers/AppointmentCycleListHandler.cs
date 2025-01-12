using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentCycleListHandler : IQueryHandler<AppointmentCycleListQuery, IList<AppointmentCycleDto>>;

public class AppointmentListHandler(IAppointmentRepository _appointmentRepository) : IAppointmentCycleListHandler
{
    public async Task<IList<AppointmentCycleDto>> Handle(AppointmentCycleListQuery query, CancellationToken token = default)
    {
        var cycle = await _appointmentRepository.GetAll();
        if (cycle == null)
        {
            // create custom exception
            throw new Exception();
        }
        
        return cycle.Select(x => x.MapToDto()).ToList();
    }
}