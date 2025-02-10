using Kinetique.Appointment.Application.BackgroundWorkers;
using Kinetique.Appointment.Application.Repositories;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Appointment.Test.Factories;
using Kinetique.Shared;
using Kinetique.Shared.Dtos;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Kinetique.Appointment.Test;

public class AppointmentFinishBackgroundServiceTests
{
    private readonly IAppointmentJournalRepository _appointmentJournalRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IRabbitPublisher _mockRabbitPublisher;
    private readonly AppointmentFinishBackgroundService _service;
    private readonly IClock _clock;
    private readonly AppointmentFactory _appointmentFactory = new();

    public AppointmentFinishBackgroundServiceTests()
    {
        _clock = new UtcClock();
        _appointmentJournalRepository = new InMemoryAppointmentJournalRepository(_clock);
        _appointmentRepository = new InMemoryAppointmentRepository(_clock);
        _mockRabbitPublisher = Substitute.For<IRabbitPublisher>();
        var logger = Substitute.For<ILogger<AppointmentFinishBackgroundService>>();
        _service = new AppointmentFinishBackgroundService(_appointmentJournalRepository, _appointmentRepository, _mockRabbitPublisher, logger);
    }

     [Fact]
     public async Task appointments_ongoing_publish_only_finished()
     {
         // Arrange
         var cancellationToken = new CancellationTokenSource().Token;
         var startDate = _clock.GetNow().AddMinutes(-10);

         var ongoing1 =
             _appointmentFactory.CreateCycleWithOneAppointment("000444", "93884383756", startDate,
                 TimeSpan.FromMinutes(20), 1);
         var ongoing2 = _appointmentFactory.CreateCycleWithOneAppointment("000443", "93884383753", startDate.AddMinutes(-10), TimeSpan.FromMinutes(40), 2);

         var finished1 = _appointmentFactory.CreateCycleWithOneAppointment("000442", "93884383751", startDate, TimeSpan.FromMinutes(1), 3);
         var finished2 = _appointmentFactory.CreateCycleWithOneAppointment("000441", "93884383752", startDate, TimeSpan.FromMinutes(2), 4);

         await _appointmentRepository.Add(ongoing1);
         await _appointmentRepository.Add(ongoing2);
         await _appointmentRepository.Add(finished1);
         await _appointmentRepository.Add(finished2);

         // Act
         await _service.StartAsync(cancellationToken);
         await Task.Delay(500); // Wait for the background service to finish
         await _service.StopAsync(cancellationToken);

         // Assert
         _mockRabbitPublisher.Received(2)
             .PublishToExchange(Arg.Any<AppointmentSharedDto>(), "appointment", "appointment.finished");
         Assert.NotNull((await _appointmentJournalRepository.GetJournalsForAppointment(new long[] {3,4}))
             .FirstOrDefault(x => x.Status == JournalStatus.Sent));
     }
     
     [Fact]
     public async Task appointment_finished_should_publish_to_rabbit()
     {
         // Arrange
         var cancellationToken = new CancellationTokenSource().Token;
         var startDate = _clock.GetNow().AddMinutes(-10);
         long appointmentId = 1;
         
         var finishedAppointment = _appointmentFactory.CreateCycleWithOneAppointment("000444","93884383756",startDate,TimeSpan.FromMinutes(1),1);
         await _appointmentRepository.Add(finishedAppointment);
         
         // Act
         await _service.StartAsync(cancellationToken);
         await Task.Delay(500); // Wait for the background service to finish
         await _service.StopAsync(cancellationToken);
         
         // Assert
         _mockRabbitPublisher.Received(1)
             .PublishToExchange(Arg.Is<AppointmentSharedDto>(dto => 
                 dto.Id == appointmentId && dto.StartDate == startDate), 
                 "appointment", "appointment.finished");
         Assert.NotNull((await _appointmentJournalRepository.GetJournalsForAppointment(new [] {appointmentId})).FirstOrDefault(x => x.Status == JournalStatus.Sent));
     }
}