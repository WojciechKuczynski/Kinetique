using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentCreateHandler : ICommandHandler<AppointmentCreateCommand>
{
}

internal sealed class AppointmentCreateHandler(IAppointmentAvailabilityService _appointmentAvailabilityService, IResponseStorage _responseStorage)
    : IAppointmentCreateHandler
{
    public async Task Handle(AppointmentCreateCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentAvailabilityService.TryBook(request.Appointment);
        
        _responseStorage.Set(ObjectConstants.Appointment, appointment.Id);
    }
}