using Kinetique.Nfz.API.Services;
using Kinetique.Nfz.Application.Repositories;
using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Nfz.Model;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Kinetique.Nfz.Test;

public class JournalTests
{
    private readonly IPatientProcedureRepository _patientProcedureRepository;
    private AppointmentEndRabbitService _appointmentEndRabbitService;

    public JournalTests()
    {
        _patientProcedureRepository = new InMemoryPatientProcedureRepository();
    }
    
    [Fact]
    // Invalid test, should be fixed later.
    public async Task appointment_in_db_should_not_add_finished_appointment()
    {
        // var mockServiceProvider = Substitute.For<IServiceProvider>();
        // var mockRabbitConsumer = Substitute.For<IRabbitConsumer>();
        //
        // mockServiceProvider.When(x => x.GetRequiredService(typeof(IRabbitConsumer)))
        //     .Do(call => call.Returns<IRabbitConsumer>(mockRabbitConsumer));
        //
        // mockServiceProvider.GetRequiredService(typeof(IPatientProcedureRepository))
        //     .Returns(_patientProcedureRepository);
        //
        // var service = new AppointmentEndRabbitService(mockServiceProvider);
        //
        // var patientProcedure = new PatientProcedure
        // {
        //     AppointmentId = 1,
        //     PatientId = 1,
        //     Status = SendStatus.InProgress
        // };
        //
        // var appointmentFinished = new AppointmentEndRequest(1, 1);
        // mockRabbitConsumer
        //     .When(x => x.OnMessageReceived<AppointmentEndRequest>("appointment-finished-queue",
        //         Arg.Any<Action<AppointmentEndRequest>>(),
        //         Arg.Any<CancellationToken>()))
        //     .Do(async call =>
        //     {
        //         var callback = call.Arg<Func<AppointmentEndRequest, Task>>();
        //         await callback(appointmentFinished);
        //     });
        //
        //
        // await _patientProcedureRepository.Add(patientProcedure);
        //
        // await service.StartAsync(CancellationToken.None);
        // await Task.Delay(2000);
        // await service.StopAsync(CancellationToken.None);
        //
        // Assert.Equal(2, (await _patientProcedureRepository.GetAll()).Count());   
    }
}