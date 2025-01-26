using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;
using Kinetique.Shared.Rpc;

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
        var client = new RpcClient<DoctorScheduleRequest, DoctorScheduleResponse>("doctor-schedule-queue");
        var message = new DoctorScheduleRequest(request.Appointment.DoctorCode, request.Appointment.StartDate,
            request.Appointment.StartDate.Add(request.Appointment.Duration));

        var response = await client.CallAsync(message, cancellationToken);
        if (response is not { CanAssign: true })
        {
            throw new Exception("Slot is not available for this time");
        }
        var appointment = await _appointmentAvailabilityService.TryBook(request);
        
        _responseStorage.Set(ObjectConstants.Appointment, appointment.Id);
    }

    public async Task Handle(AppointmentCycleCreateCommand command, CancellationToken token = default)
    {
        var onGoingCycle = await _appointmentRepository.GetOngoingCycleForPatient(command.AppointmentCycle.PatientPesel);
        if (onGoingCycle != null)
        {
            throw new PatientHasActiveCycleException();
        }
        
        var cycle = await _appointmentRepository.Add(command.AppointmentCycle.MapToEntity());
        _responseStorage.Set(ObjectConstants.AppointmentCycle, cycle.Id);
    }
}