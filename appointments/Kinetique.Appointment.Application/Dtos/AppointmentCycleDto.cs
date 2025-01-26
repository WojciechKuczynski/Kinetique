using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.Application.Dtos;

public class AppointmentCycleDto
{
    public long Id { get; set; }
    public DateTime? StartDate { get; set; }                                                        
    public byte Limit { get; set; }                                                                 
    public virtual List<AppointmentDto> Appointments { get; set; }   
    public virtual ReferralDto? Referral { get; set; }         
    public Pesel PatientPesel { get; set; }
    public string DoctorCode { get; set; }
}