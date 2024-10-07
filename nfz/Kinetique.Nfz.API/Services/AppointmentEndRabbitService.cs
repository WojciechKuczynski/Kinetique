using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Nfz.Model;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;

namespace Kinetique.Nfz.API.Services;

public class AppointmentEndRabbitService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var rabbitConsumer = scope.ServiceProvider.GetRequiredService<IRabbitConsumer>();
        var patientProcedureRepository = scope.ServiceProvider.GetRequiredService<IPatientProcedureRepository>();
        
        await rabbitConsumer.OnMessageReceived<AppointmentEndRequest>("appointment-finished-queue", async message =>
        {
            await patientProcedureRepository.Add(new PatientProcedure()
            {
                PatientId = message.PatientId,
                AppointmentId = message.AppointmentId,
                Status = SendStatus.New
            });
        }, stoppingToken);
    }
}