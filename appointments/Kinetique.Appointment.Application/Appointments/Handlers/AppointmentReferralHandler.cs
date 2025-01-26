using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Rpc;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentReferralAddHandler : ICommandHandler<AppointmentReferralAddCommand>;
public interface IAppointmentReferralRemoveHandler : ICommandHandler<AppointmentReferralRemoveCommand>;


internal class AppointmentReferralHandler : IAppointmentReferralAddHandler, IAppointmentReferralRemoveHandler
{
    private readonly IAppointmentRepository _appointmentRepository;
    
    public AppointmentReferralHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    public async Task Handle(AppointmentReferralAddCommand command, CancellationToken cts = default)
    {
        var cycle = await _appointmentRepository.GetOngoingCycleForPatient(command.Referral.Pesel);
        if (cycle != null)
        {
            cycle.AddReferral(command.Referral.MapToEntity());
            await _appointmentRepository.Update(cycle);
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