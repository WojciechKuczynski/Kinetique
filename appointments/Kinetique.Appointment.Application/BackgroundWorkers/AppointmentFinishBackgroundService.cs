using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Messaging;
using Microsoft.Extensions.Hosting;
namespace Kinetique.Appointment.Application.BackgroundWorkers;

public class AppointmentFinishBackgroundService(
    IAppointmentJournalRepository appointmentJournalRepository,
    IAppointmentRepository appointmentRepository,
    IRabbitPublisher rabbitPublisher)
    : BackgroundService
{
    private readonly IAppointmentJournalRepository _appointmentJournalRepository = appointmentJournalRepository;
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IRabbitPublisher _rabbitPublisher = rabbitPublisher;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await foreach(var appointment in GetFinishedAppointments(stoppingToken))
            {
                // send appointment
                _rabbitPublisher.PublishToExchange(appointment.MapToSharedDto(), "appointment", "appointment.finished");
                // update journal
                await _appointmentJournalRepository.UpdateJournal(appointment.Id, JournalStatus.Sent);
            }
            await Task.Delay(1000 * 60, stoppingToken); // check every minute
        }
    }
    
    private async IAsyncEnumerable<Model.Appointment> GetFinishedAppointments(CancellationToken token)
    {
        var lastCreatedAppointment = await _appointmentJournalRepository.GetLatestJournal();
        var appointmentsToSent = await _appointmentRepository.GetAppointmentsFinishedAfter(lastCreatedAppointment?.CreatedAt);
        
        if (appointmentsToSent.Count == 0)
        {
            yield break;
        }
        
        var appointmentsAlreadyInJournal = await _appointmentJournalRepository.GetJournalsForAppointment(appointmentsToSent.Select(x => x.Id).ToArray());
        foreach (var appointment in appointmentsToSent)
        {
            if (appointmentsAlreadyInJournal.FirstOrDefault(x => x.AppointmentId == appointment.Id) != null)
                continue;
            
            await _appointmentJournalRepository.AddJournal(appointment.Id, JournalStatus.NotSent);
            yield return appointment;
        }
    }
}