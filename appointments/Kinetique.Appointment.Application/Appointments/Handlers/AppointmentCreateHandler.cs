using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Repositories;
using Kinetique.Shared.Model.Storage;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentCreateHandler : ICommandHandler<AppointmentCreateCommand>, ICommandHandler<AppointmentCycleCreateCommand>
{
}

internal sealed class AppointmentCreateHandler(IAppointmentAvailabilityService _appointmentAvailabilityService,
    IResponseStorage _responseStorage, IAppointmentCycleRepository _appointmentCycleRepository)
    : IAppointmentCreateHandler
{
    public async Task Handle(AppointmentCreateCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentAvailabilityService.TryBook(request.Appointment);
        
        _responseStorage.Set(ObjectConstants.Appointment, appointment.Id);
    }

    public async Task Handle(AppointmentCycleCreateCommand command, CancellationToken token = default)
    {
        if (command.AppointmentCycle.PatientId.HasValue)
        {
            var onGoingCycle = await _appointmentCycleRepository.GetOngoingCycleForPatient(command.AppointmentCycle.PatientId.Value);
            if (onGoingCycle != null)
            {
                throw new Exception("This patient is already having active cycle.");
            }
        }

        var cycle = await _appointmentCycleRepository.Add(command.AppointmentCycle.MapToEntity());
        _responseStorage.Set(ObjectConstants.AppointmentCycle, cycle.Id);
    }
}