using Kinetique.Shared.Messaging;

namespace Kinetique.Shared.Dtos;

public class AppointmentSharedDto : IRabbitRequest
{
    public long Id { get; set; }
    public long? PatientId { get; set; }
    public long DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Duration { get; set; }
    public string Description { get; set; }
}