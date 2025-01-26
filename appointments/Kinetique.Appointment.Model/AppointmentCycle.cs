using Kinetique.Appointment.Model.Exceptions;
using Kinetique.Shared.Model;
using Kinetique.Shared.Model.ValueObjects;
using Microsoft.AspNetCore.Http.Features;

namespace Kinetique.Appointment.Model;

public class AppointmentCycle : BaseModel
{
    public DateTime? StartDate { get; set; }
    public byte Limit { get; init; }
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual Referral? Referral { get; set; }
    public Pesel PatientPesel { get; set; }
    public string DoctorCode { get; set; }
    public bool CycleFull { get; set; }
    public bool CycleReady => Referral != null && Limit > 0;
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
            throw new CycleAlreadyHavingReferralException();
        Referral = referral;
        referral.AppointmentCycleId = this.Id;
    }

    public void RemoveReferral()
    {
        Referral = null;
    }
    
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments.Count >= Limit)
            throw new Exception("Limit reached out");

        if (Appointments.Count == 0)
            StartDate = appointment.StartDate;
        
        // Validation of Appointment not required.
        Appointments.Add(appointment);
        appointment.Cycle = this;
        if (Appointments.Count == Limit)
            CycleFull = true;
    }
}