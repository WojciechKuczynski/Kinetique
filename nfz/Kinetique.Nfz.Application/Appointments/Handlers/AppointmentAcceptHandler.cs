using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Nfz.Application.Appointments.Handlers;

public interface IAppointmentAcceptHandler : ICommandHandler<AppointmentAcceptCommand>;


public class AppointmentAcceptHandler(IPatientProcedureRepository patientProcedureRepository) : IAppointmentAcceptHandler
{
    public Task Handle(AppointmentAcceptCommand command, CancellationToken token = default)
    {
        // check if patient procedures are filled already
        
        // sending message to NFZ system
        Console.WriteLine("Message sent to NFZ system");
        return Task.CompletedTask;
    }
}