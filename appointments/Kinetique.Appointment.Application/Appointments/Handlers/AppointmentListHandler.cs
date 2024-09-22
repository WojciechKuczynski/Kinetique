using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentListHandler : IQueryHandler<AppointmentListQuery, IList<AppointmentDto>>;

public class AppointmentListHandler(IAppointmentRepository _appointmentRepository) : IAppointmentListHandler
{
    public async Task<IList<AppointmentDto>> Handle(AppointmentListQuery query, CancellationToken token = default)
    {
        var appointments = await _appointmentRepository.GetAll();
        if (appointments == null)
        {
            // create custom exception
            throw new Exception();
        }
        
        return appointments.Select(x => x.MapToDto()).ToList();
    }
}