using Kinetique.Appointment.Application.BackgroundWorkers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Dtos;
using Kinetique.Shared.Messaging;
using NSubstitute;
using Xunit;

namespace Kinetique.Appointment.Test;

public class AppointmentFinishBackgroundServiceTests
{
    private readonly IAppointmentJournalRepository _mockAppointmentJournalRepository;
    private readonly IAppointmentRepository _mockAppointmentRepository;
    private readonly IRabbitPublisher _mockRabbitPublisher;
    private readonly AppointmentFinishBackgroundService _service;

    public AppointmentFinishBackgroundServiceTests()
    {
        _mockAppointmentJournalRepository = Substitute.For<IAppointmentJournalRepository>();
        _mockAppointmentRepository = Substitute.For<IAppointmentRepository>();
        _mockRabbitPublisher = Substitute.For<IRabbitPublisher>();

        _service = new AppointmentFinishBackgroundService(
            _mockAppointmentJournalRepository,
            _mockAppointmentRepository,
            _mockRabbitPublisher);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldPublishFinishedAppointments()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var startDate = DateTime.UtcNow.AddMinutes(-10);
        var appointmentId = 1;
        
        var finishedAppointments = new List<Model.Appointment>
        {
            new Model.Appointment { Id = appointmentId, StartDate = startDate, Duration = TimeSpan.FromMinutes(1) }
        };

        _mockAppointmentJournalRepository.GetLatestJournal()
            .Returns(new Model.AppointmentJournal { CreatedAt = startDate.AddMinutes(-10) });
        

        _mockAppointmentRepository.GetAppointmentsFinishedAfter(Arg.Any<DateTime>())
            .Returns(finishedAppointments);

        _mockAppointmentJournalRepository.GetJournalsForAppointment(Arg.Any<long[]>())
            .Returns(new List<Model.AppointmentJournal>());

        // Act
        await _service.StartAsync(cancellationToken);
        await Task.Delay(500); // Wait for the background service to finish
        await _service.StopAsync(cancellationToken);

        // Assert
        _mockRabbitPublisher.Received(1).PublishToExchange(Arg.Is<AppointmentSharedDto>(dto => dto.Id == appointmentId && dto.StartDate == startDate), "appointment", "appointment.finished");
        await _mockAppointmentJournalRepository.Received(1).UpdateJournal(Arg.Any<long>(), JournalStatus.Sent);
    }
}