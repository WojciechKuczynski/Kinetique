using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Microsoft.Extensions.Hosting;
namespace Kinetique.Appointment.Application.BackgroundWorkers;

public class AppointmentFinishBackgroundService(
    IAppointmentJournalRepository appointmentJournalRepository,
    IAppointmentRepository appointmentRepository)
    : BackgroundService
{
    private readonly IAppointmentJournalRepository _appointmentJournalRepository = appointmentJournalRepository;
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // do some work
            await foreach(var appointment in GetFinishedAppointments(stoppingToken))
            {
                // send appointment
                
                // update journal
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
    
    private async IAsyncEnumerable<Model.Appointment> GetFinishedAppointments(CancellationToken token)
    {
        var lastCreatedAppointment = await _appointmentJournalRepository.GetLatestJournal();
        var appointmentsToSent = await _appointmentRepository.GetAppointmentsFinishedAfter(lastCreatedAppointment.CreatedAt);
        
        if (appointmentsToSent.Count == 0)
        {
            yield break;
        }
        
        var appointmentsAlreadyInJournal = await _appointmentJournalRepository.GetJournalsForAppointment(appointmentsToSent.Select(x => x.Id).ToArray());
        //create list from appointentsToSent without those in appointmentsAlreadyInJournal
        foreach (var appointment in appointmentsToSent)
        {
            if (appointmentsAlreadyInJournal.FirstOrDefault(x => x.AppointmentId == appointment.Id) != null)
                continue;
            
            await _appointmentJournalRepository.AddJournal(appointment.Id, JournalStatus.NotSent);
            yield return appointment;
        }
    }
}