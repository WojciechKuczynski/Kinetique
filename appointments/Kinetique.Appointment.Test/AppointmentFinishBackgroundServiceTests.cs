using Kinetique.Appointment.Application.BackgroundWorkers;
using Kinetique.Appointment.Application.Repositories;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Dtos;
using Kinetique.Shared.Messaging;
using NSubstitute;
using Xunit;

namespace Kinetique.Appointment.Test;

public class AppointmentFinishBackgroundServiceTests
{
    private readonly IAppointmentJournalRepository _appointmentJournalRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IRabbitPublisher _mockRabbitPublisher;
    private readonly AppointmentFinishBackgroundService _service;

    public AppointmentFinishBackgroundServiceTests()
    {
        _appointmentJournalRepository = new InMemoryAppointmentJournalRepository();
        _appointmentRepository = new InMemoryAppointmentRepository();
        _mockRabbitPublisher = Substitute.For<IRabbitPublisher>();

        _service = new AppointmentFinishBackgroundService(
            _appointmentJournalRepository,
            _appointmentRepository,
            _mockRabbitPublisher);
    }

    [Fact]
    public async Task appointments_ongoing_publish_only_finished()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var startDate = DateTime.UtcNow.AddMinutes(-10);
        long appointmentId = 1;
        
        var ongoingAppointment1 = new Model.Appointment()
        {
            Id = appointmentId, StartDate = startDate, Duration = TimeSpan.FromMinutes(20)
        };
        var ongoingAppointment2 = new Model.Appointment()
        {
            Id = appointmentId, StartDate = startDate.AddMinutes(-10), Duration = TimeSpan.FromMinutes(40)
        };
        var finishedAppointment1 = new Model.Appointment()
        {
            Id = appointmentId, StartDate = startDate, Duration = TimeSpan.FromMinutes(1)
        };
        var finishedAppointment2 = new Model.Appointment()
        {
            Id = appointmentId, StartDate = startDate, Duration = TimeSpan.FromMinutes(2)
        };

        await _appointmentRepository.Add(ongoingAppointment1);
        await _appointmentRepository.Add(ongoingAppointment2);
        await _appointmentRepository.Add(finishedAppointment1);
        await _appointmentRepository.Add(finishedAppointment2);

        // Act
        await _service.StartAsync(cancellationToken);
        await Task.Delay(500); // Wait for the background service to finish
        await _service.StopAsync(cancellationToken);

        // Assert
        _mockRabbitPublisher.Received(2)
            .PublishToExchange(Arg.Any<AppointmentSharedDto>(), "appointment", "appointment.finished");
        Assert.NotNull((await _appointmentJournalRepository.GetJournalsForAppointment(new [] {appointmentId}))
            .FirstOrDefault(x => x.Status == JournalStatus.Sent));
    }
    
    [Fact]
    public async Task appointment_finished_should_publish_to_rabbit()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var startDate = DateTime.UtcNow.AddMinutes(-10);
        long appointmentId = 1;
        
        var finishedAppointment = new Model.Appointment()
        {
            Id = appointmentId, StartDate = startDate, Duration = TimeSpan.FromMinutes(1)
        };
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