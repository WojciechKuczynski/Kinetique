using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentSingleHandler : IQueryHandler<AppointmentSingleQuery, AppointmentDto?>;

public class AppointmentSingleHandler(IAppointmentRepository _appointmentRepository) : IAppointmentSingleHandler
{
    public async Task<AppointmentDto?> Handle(AppointmentSingleQuery query, CancellationToken token = default)
    {
        var appointment = await _appointmentRepository.Get(query?.Id ?? 0);
        if (appointment == null)
        {
            // create custom exception
            throw new Exception();
        }
        
        return appointment.MapToDto();
    }
}