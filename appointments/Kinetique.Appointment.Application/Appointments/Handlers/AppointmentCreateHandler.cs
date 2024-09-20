using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentCreateHandler : ICommandHandler<AppointmentCreateCommand>
{
}

internal sealed class AppointmentCreateHandler(IAppointmentRepository _appointmentRepository, IResponseStorage _responseStorage)
    : IAppointmentCreateHandler
{
    public async Task Handle(AppointmentCreateCommand request, CancellationToken cancellationToken)
    {
        var appointmentEntity = request.Appointment.MapToEntity();
        var result = await _appointmentRepository.Add(appointmentEntity);
        _responseStorage.Set(ObjectConstants.Appointment, result.Id);
    }
}