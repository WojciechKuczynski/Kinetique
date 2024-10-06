using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class PostgresAppointmentJournalRepository(DataContext context) : PostgresRepositoryBase<AppointmentJournal>(context), IAppointmentJournalRepository
{
    private readonly DataContext _context = context;
    public async Task AddJournal(long appointmentId, JournalStatus status)
    {
        var journal = new AppointmentJournal
        {
            AppointmentId = appointmentId,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };
        
        if (await _objects.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId) == null)
        {
            await _objects.AddAsync(journal);
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateJournal(long appointmentId, JournalStatus status)
    {
        var journal = await _objects.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);

        if (journal == null)
        {
            throw new Exception("Journal not found");
        }

        journal.Status = status;
        if (status == JournalStatus.Sent)
            journal.SentAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task<AppointmentJournal?> GetLatestJournal()
    {
        return await _objects.OrderByDescending(x => x.CreatedAt).SingleOrDefaultAsync();
    }

    public async Task<IList<AppointmentJournal>> GetJournalsForAppointment(long[] appointmentId)
    {
        return await _objects.Where(x => appointmentId.Contains(x.AppointmentId)).ToListAsync();
    }
}