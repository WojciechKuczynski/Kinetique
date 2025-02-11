using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Services;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;

namespace Kinetique.Schedule.BackgroundServices;

public class AppointmentRemovedSubscriber(IServiceProvider _serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var consumer = scope.ServiceProvider.GetRequiredService<IRabbitConsumer>();
        var logger = _serviceProvider.GetRequiredService<ILogger<AppointmentRemovedSubscriber>>();
        var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
        var scheduleService = new ScheduleBookingService(scheduleRepository);
        logger.LogTrace("AppointmentRemovedSubscriber :: Starting to listen for removed appointments");
        
        await consumer.OnMessageReceived<AppointmentRemovedEvent>("appointment-removed-queue",message =>
        {
            logger.LogTrace($"AppointmentRemovedSubscriber :: Received message for doctor {message.DoctorCode}");
            // set block for doctor on this time.
            scheduleService.UnBlockDoctorScheduleSlot(message).Wait(stoppingToken);
        }, stoppingToken);
    }
}