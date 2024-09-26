using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Appointment.DAL.Repositories;

public interface IAppointmentJournalRepository
{
    Task AddJournal(long appointmentId, JournalStatus status);
    Task UpdateJournal(long appointmentId, JournalStatus status);
    Task<AppointmentJournal> GetLatestJournal();
    Task<IList<AppointmentJournal>> GetJournalsForAppointment(long[] appointmentId);
}