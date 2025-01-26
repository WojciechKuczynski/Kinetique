using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.Application.Dtos;

public class AppointmentDto
{
    public long Id { get; set; }
    public Pesel PatientPesel { get; set; }
    public string DoctorCode { get; set; }
    
    public long? CycleId { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Duration { get; set; }
    public string Description { get; set; }
}