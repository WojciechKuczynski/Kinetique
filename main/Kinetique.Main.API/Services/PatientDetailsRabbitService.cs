using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Patients;
using Kinetique.Main.Application.Patients.Handlers;
using Kinetique.Main.Application.Patients.RequestArgs;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;

namespace Kinetique.Main.API.Services;

public class PatientDetailsRabbitService : IHostedService
{
    private const string ConfigSection = "RabbitMqConnection";
    private RcpServer<PatientDetailsRequest, PatientDetailsResponse> _rabbitServer;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public PatientDetailsRabbitService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString(ConfigSection);
            _rabbitServer =
                new RcpServer<PatientDetailsRequest, PatientDetailsResponse>(connectionString!);
            _rabbitServer.Configure("patient-details-queue", HandleRequest!);
            
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
    
    private async Task<PatientDetailsResponse?> HandleRequest(PatientDetailsRequest request)
    {
        PatientDto? response;
        using var scope = _serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<PatientDetailsRabbitService>>();
        var patientSingleHandler = scope.ServiceProvider.GetRequiredService<IPatientSingleHandler>();
        logger.LogTrace("Received request for patient details");
        if (request.PatientId == null)
        {
            logger.LogTrace($"Searching for patient by pesel {request.Pesel}");
            response = await patientSingleHandler.Handle(new PatientSingleQuery(new PatientQueryRequest() { Pesel = request.Pesel }));
        }
        else
        {
            logger.LogTrace($"Searching for patient by patient Id {request.PatientId}");
            response = await patientSingleHandler.Handle(new PatientSingleQuery(new PatientQueryRequest(){Id = request.PatientId}));
        }

        if (response == null)
        {
            logger.LogWarning("Patient not found");
            return null;
        }
        
        return new PatientDetailsResponse(response.Id, response.FirstName, response.LastName, response.PhoneNumber,response.Address,response.Pesel);
    }
}