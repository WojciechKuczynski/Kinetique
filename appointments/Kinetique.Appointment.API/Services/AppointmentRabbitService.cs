using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;

namespace Kinetique.Appointment.API.Services;

public class AppointmentRabbitService: IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private RcpServer<PatientAppointmentRequest, PatientAppointmentResponse> _rabbitServer;

    public AppointmentRabbitService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _rabbitServer = new RcpServer<PatientAppointmentRequest, PatientAppointmentResponse>("appointment-queue",TestResponse);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitServer.Dispose();
        return Task.CompletedTask;
    }
    
    private async Task<PatientAppointmentResponse> TestResponse(PatientAppointmentRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();
        // Use the repository here
        var response = await repo.GetAll();
        return new PatientAppointmentResponse(response.Select(x => x.MapToSharedDto()).ToList());
    }
}