using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;

namespace Kinetique.Appointment.Application.Repositories;

public class InMemoryAppointmentJournalRepository : IAppointmentJournalRepository
{
    private readonly List<AppointmentJournal> _objects = [];
    
    public Task AddJournal(long appointmentId, JournalStatus status)
    {
        var journal = new AppointmentJournal
        {
            AppointmentId = appointmentId,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };
        
        if (_objects.Find(x => x.AppointmentId == appointmentId) == null)
        {
            _objects.Add(journal);
        }
        
        return Task.CompletedTask;
    }

    public Task UpdateJournal(long appointmentId, JournalStatus status)
    {
        var journal = _objects.Find(x => x.AppointmentId == appointmentId);

        if (journal == null)
        {
            throw new Exception("Journal not found");
        }

        journal.Status = status;
            if (status == JournalStatus.Sent)
                journal.SentAt = DateTime.UtcNow;
        
        return Task.CompletedTask;
    }

    public Task<AppointmentJournal> GetLatestJournal()
    {
        return Task.FromResult(_objects.OrderByDescending(x => x.CreatedAt).FirstOrDefault());
    }

    public async Task<IList<AppointmentJournal>> GetJournalsForAppointment(long[] appointmentId)
    {
        return await Task.FromResult(_objects.Where(x => appointmentId.Contains(x.AppointmentId)).ToList());
    }
}