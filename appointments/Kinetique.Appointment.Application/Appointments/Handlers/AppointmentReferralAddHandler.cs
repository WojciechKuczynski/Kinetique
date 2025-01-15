using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Rpc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentReferralAddHandler : ICommandHandler<AppointmentReferralAddCommand>;

internal class AppointmentReferralAddHandler : IAppointmentReferralAddHandler
{
    private readonly IAppointmentRepository _appointmentRepository;
    public AppointmentReferralAddHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    public async Task Handle(AppointmentReferralAddCommand command, CancellationToken cts = default)
    {
        // validate referral UID
        
        var message = new PatientDetailsRequest(command.Referral.Pesel);
        var client = new RpcClient<PatientDetailsRequest,PatientDetailsResponse>("patient-details-queue");
        var response = await client.CallAsync(message,cts);
        if (response != null)
        {
            var cycle = await _appointmentRepository.GetOngoingCycleForPatient(response.Id);
            if (cycle != null)
            {
                cycle.AddReferral(command.Referral.MapToEntity());
                await _appointmentRepository.Update(cycle);
            }
        }
    }
}