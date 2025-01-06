using Kinetique.Shared.Model;

namespace Kinetique.Appointment.Model;

public class AppointmentCycle : BaseModel
{
    public DateTime? StartDate { get; set; }
    public byte Limit { get; set; }
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual Referral? Referral { get; set; }

    public bool CycleCompleted => Referral != null && Limit > 0;
    public AppointmentCycle()
    {
        
    }
    public AppointmentCycle(byte limit)
    {
        Limit = limit;
    }

    public AppointmentCycle(Referral referral)
    {
        AddReferral(referral);
    }

    public void AddReferral(Referral referral)
    {
        if (Referral != null)
            throw new Exception("Referral is already present in this cycle");
        Referral = referral;
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