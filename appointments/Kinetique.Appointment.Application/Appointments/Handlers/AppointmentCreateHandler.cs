using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentCreateHandler : ICommandHandler<AppointmentCreateCommand>, ICommandHandler<AppointmentCycleCreateCommand>
{
}

internal sealed class AppointmentCreateHandler(IAppointmentAvailabilityService _appointmentAvailabilityService,
    IResponseStorage _responseStorage, IAppointmentRepository _appointmentRepository)
    : IAppointmentCreateHandler
{
    public async Task Handle(AppointmentCreateCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentAvailabilityService.TryBook(request);
        
        _responseStorage.Set(ObjectConstants.Appointment, appointment.Id);
    }

    public async Task Handle(AppointmentCycleCreateCommand command, CancellationToken token = default)
    {
        var onGoingCycle = await _appointmentRepository.GetOngoingCycleForPatient(command.AppointmentCycle.PatientId);
        if (onGoingCycle != null)
        {
            throw new PatientHasActiveCycleException();
        }
        
        var cycle = await _appointmentRepository.Add(command.AppointmentCycle.MapToEntity());
        _responseStorage.Set(ObjectConstants.AppointmentCycle, cycle.Id);
    }
}