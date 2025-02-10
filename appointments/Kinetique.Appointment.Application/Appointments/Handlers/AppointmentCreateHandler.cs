using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;
using Kinetique.Shared.Rpc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentCreateHandler : ICommandHandler<AppointmentCreateCommand>, ICommandHandler<AppointmentCycleCreateCommand>
{
}

internal sealed class AppointmentCreateHandler(IAppointmentAvailabilityService _appointmentAvailabilityService,
    IResponseStorage _responseStorage, IAppointmentRepository _appointmentRepository,IRabbitPublisher _rabbitPublisher, IConnection _connection, ILogger<AppointmentCreateHandler> _logger)
    : IAppointmentCreateHandler
{
    
    public async Task Handle(AppointmentCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creating appointment for hostName {_connection.Endpoint.HostName}");
        var client = new RpcClient<DoctorScheduleRequest, DoctorScheduleResponse>(_connection.Endpoint.ToString());
        client.Configure("doctor-schedule-queue");
        
        var message = new DoctorScheduleRequest(request.Appointment.DoctorCode, request.Appointment.StartDate,
            request.Appointment.StartDate.Add(request.Appointment.Duration));

        var response = await client.CallAsync(message, cancellationToken);
        if (response is not { CanAssign: true })
        {
            throw new KinetiqueException("Slot is not available for this time");
        }
        
        var appointment = await _appointmentAvailabilityService.TryBook(request);
        _rabbitPublisher.PublishToExchange(new AppointmentCreatedEvent(message.DoctorCode, message.StartDate, message.EndDate), "appointment", "appointment.created");
        
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