using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentSingleHandler : IQueryHandler<AppointmentSingleQuery, AppointmentDto?>, IQueryHandler<AppointmentCycleSingleQuery, AppointmentCycleDto?> ;

public class AppointmentSingleHandler(IAppointmentRepository _appointmentRepository, IAppointmentCycleRepository _appointmentCycleRepository) : IAppointmentSingleHandler
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

    public async Task<AppointmentCycleDto?> Handle(AppointmentCycleSingleQuery query, CancellationToken token = default)
    {
        var appointmentCycle = await _appointmentCycleRepository.Get(query?.Id ?? 0);
        if (appointmentCycle == null)
        {
            // create custom exception
            throw new Exception();
        }
        
        return appointmentCycle.MapToDto();
    }
}