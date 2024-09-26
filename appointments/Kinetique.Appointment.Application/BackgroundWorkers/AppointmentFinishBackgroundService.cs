using Microsoft.Extensions.Hosting;
namespace Kinetique.Appointment.Application.BackgroundWorkers;

public class AppointmentFinishBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}