using Kinetique.Shared.Model;

namespace Kinetique.Appointment.Model;

public class AppointmentCycle : BaseModel
{
    public DateTime? StartDate { get; set; }
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public byte Limit { get; set; }

    public AppointmentCycle(byte limit)
    {
        Limit = limit;
    }
    
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments.Count >= Limit)
            throw new Exception("Limit reached out");

        if (Appointments.Count == 0)
            StartDate = appointment.StartDate;
        
        // Validation of Appointment not required.
        Appointments.Add(appointment);
    }
}