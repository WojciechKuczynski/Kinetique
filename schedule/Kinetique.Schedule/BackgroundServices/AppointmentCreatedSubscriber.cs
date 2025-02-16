using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Services;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;

namespace Kinetique.Schedule.BackgroundServices;

public class AppointmentCreatedSubscriber(IServiceProvider _serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var consumer = scope.ServiceProvider.GetRequiredService<IRabbitConsumer>();
        var logger = _serviceProvider.GetRequiredService<ILogger<AppointmentCreatedSubscriber>>();
        var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
        var scheduleService = new ScheduleBookingService(scheduleRepository);
        logger.LogTrace("AppointmentCreatedSubscriber :: Starting to listen for new appointments");
        await consumer.OnMessageReceived<AppointmentCreatedEvent>("appointment-created-queue",message =>
        {
            logger.LogTrace($"AppointmentCreatedSubscriber :: Received message for doctor {message.DoctorCode}");
            // set block for doctor on this time.
            scheduleService.BlockDoctorScheduleSlot(message).Wait(stoppingToken);
        }, stoppingToken);
    }
}