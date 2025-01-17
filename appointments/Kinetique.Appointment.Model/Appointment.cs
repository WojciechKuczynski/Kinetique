using Kinetique.Shared.Model;

namespace Kinetique.Appointment.Model;

public class Appointment : BaseModel
{
    public DateTime StartDate { get; set; }
    public TimeSpan Duration { get; set; }
    public string Description { get; set; }
    
    public long CycleId { get; set; }
    public virtual AppointmentCycle Cycle { get; set; }
}