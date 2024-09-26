using Kinetique.Shared.Model;

namespace Kinetique.Appointment.Model;

public class AppointmentJournal : BaseModel
{
    public long AppointmentId { get; set; }
    public JournalStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
}

public enum JournalStatus
{
    Sent,
    NotSent
}