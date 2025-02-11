using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Nfz.Model;
using Kinetique.Shared.Dtos;
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
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppointmentEndRabbitService>>();
        
        logger.LogTrace("AppointmentEndRabbitService :: Starting to listen for finished appointments");
        var patientProcedureRepository = scope.ServiceProvider.GetRequiredService<IPatientProcedureRepository>();
        
        await rabbitConsumer.OnMessageReceived<AppointmentSharedDto>("appointment-finished-queue", async message =>
        {
            logger.LogTrace($"AppointmentEndRabbitService :: Received message for patient {message.PatientPesel}");
            
            _ = await patientProcedureRepository.Add(new PatientProcedure()
            {
                PatientPesel = message.PatientPesel,
                AppointmentExternalId = message.Id,
                StartDate = message.StartDate,
                Duration = message.Duration,
                Status = SendStatus.New
            });
        }, stoppingToken);
    }
}