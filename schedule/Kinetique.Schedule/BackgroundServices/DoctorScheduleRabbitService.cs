using Kinetique.Schedule.Requests;
using Kinetique.Schedule.Requests.Handlers;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;
using RabbitMQ.Client;

namespace Kinetique.Schedule.BackgroundServices;

public class DoctorScheduleRabbitService : IHostedService
{
    private const string ConfigSection = "RabbitMqConnection";
    
    private RcpServer<Shared.Messaging.Messages.DoctorScheduleRequest, DoctorScheduleResponse> _rabbitServer;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public DoctorScheduleRabbitService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var rabbitMqConnection = _configuration.GetConnectionString(ConfigSection);
            _rabbitServer =
                new RcpServer<Shared.Messaging.Messages.DoctorScheduleRequest, DoctorScheduleResponse>(rabbitMqConnection);
            _rabbitServer.Configure("doctor-schedule-queue", HandleRequest);
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
    
    private async Task<DoctorScheduleResponse?> HandleRequest(Shared.Messaging.Messages.DoctorScheduleRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var scheduleSlotQueryHandler = scope.ServiceProvider.GetRequiredService<IDoctorScheduleSlotHandler>();
        var res = await scheduleSlotQueryHandler.Handle(new DoctorScheduleSlotQuery(request.StartDate, request.EndDate,
            request.DoctorCode, false));
        // For now just check if any.
        //TODO: Check if it occupies more than 1 slot etc.
        return new DoctorScheduleResponse(res.Any());
    }
}