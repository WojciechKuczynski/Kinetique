using Kinetique.Appointment.Model;

namespace Kinetique.Appointment.Application.Dtos;

public class AppointmentJournalDto
{
    //dto for appointmentJournal
    public long AppointmentId { get; set; }
    public JournalStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
}