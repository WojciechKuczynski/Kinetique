using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;

namespace Kinetique.Schedule.BackgroundServices;

public class DoctorScheduleRabbitService : IHostedService
{
    private RcpServer<Shared.Messaging.Messages.DoctorScheduleRequest, DoctorScheduleResponse> _rabbitServer;
    private readonly IServiceProvider _serviceProvider;

    public DoctorScheduleRabbitService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _rabbitServer =
                new RcpServer<Shared.Messaging.Messages.DoctorScheduleRequest, DoctorScheduleResponse>("doctor-schedule-queue", HandleRequest);
        }
        catch
        {
            
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitServer.Dispose();
        return Task.CompletedTask;
    }
    
    private Task<DoctorScheduleResponse?> HandleRequest(Shared.Messaging.Messages.DoctorScheduleRequest request)
    {
        return Task.FromResult(new DoctorScheduleResponse(true));
    }
}