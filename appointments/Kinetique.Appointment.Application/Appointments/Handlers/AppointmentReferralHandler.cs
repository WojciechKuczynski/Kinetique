using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Rpc;
using Microsoft.Extensions.Logging;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentReferralAddHandler : ICommandHandler<AppointmentReferralAddCommand>;
public interface IAppointmentReferralRemoveHandler : ICommandHandler<AppointmentReferralRemoveCommand>;


internal class AppointmentReferralHandler : IAppointmentReferralAddHandler, IAppointmentReferralRemoveHandler
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILogger<AppointmentReferralHandler> _logger;
    
    public AppointmentReferralHandler(IAppointmentRepository appointmentRepository, ILogger<AppointmentReferralHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _logger = logger;
    }
    
    public async Task Handle(AppointmentReferralAddCommand command, CancellationToken cts = default)
    {
        _logger.LogInformation("AppointmentReferralHandler :: Handle adding referral");
        var cycle = await _appointmentRepository.GetOngoingCycleForPatient(command.Referral.Pesel);
        if (cycle != null)
        {
            _logger.LogInformation("AppointmentReferralHandler :: addingReferral");
            cycle.AddReferral(command.Referral.MapToEntity());
            await _appointmentRepository.Update(cycle);
        }
        else
        {
            throw new KinetiqueException($"No active cycle for Patient {command.Referral.Pesel}");
        }
    }

    public async Task Handle(AppointmentReferralRemoveCommand command, CancellationToken token = default)
    {
        if (command.CycleId == 0)
        {
            throw new Exception("Please provide valid Cycle");
        }

        var cycle = await _appointmentRepository.Get(command.CycleId);
        if (cycle?.Referral == null)
        {
            throw new Exception("Cycle not found or not found referral to remove.");
        }

        cycle.RemoveReferral();
        await _appointmentRepository.Update(cycle);
    }
}