using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.Application.Dtos;

public class ReferralDto
{
    public long Id { get; set; }
    public string Uid { get; set; }
    public string Code { get; set; }
    public Pesel Pesel { get; set; }
    public DateTime CreatedOn { get; set; }
}