using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Patients;
using Kinetique.Main.Application.Patients.Handlers;
using Kinetique.Main.Application.Patients.RequestArgs;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;

namespace Kinetique.Main.API.Services;

public class PatientDetailsRabbitService : IHostedService
{
    private RcpServer<PatientDetailsRequest, PatientDetailsResponse> _rabbitServer;
    private readonly IServiceProvider _serviceProvider;

    public PatientDetailsRabbitService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _rabbitServer =
                new RcpServer<PatientDetailsRequest, PatientDetailsResponse>("patient-details-queue", HandleRequest);
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
        var patientSingleHandler = scope.ServiceProvider.GetRequiredService<IPatientSingleHandler>();
        if (request.PatientId == null)
        {
            response = await patientSingleHandler.Handle(new PatientSingleQuery(new PatientQueryRequest() { Pesel = request.Pesel }));
        }
        else
        {
            response = await patientSingleHandler.Handle(new PatientSingleQuery(new PatientQueryRequest(){Id = request.PatientId}));
        }

        if (response == null)
        {
            return null;
        }
        
        return new PatientDetailsResponse(response.Id, response.FirstName, response.LastName, response.PhoneNumber,response.Address,response.Pesel);
    }
}