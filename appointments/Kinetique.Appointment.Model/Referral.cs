using Kinetique.Shared.Model;
using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.Model;

public class Referral : BaseModel
{
    public string Uid { get; set; }
    public string Code { get; set; }
    public Pesel Pesel { get; set; }
    public DateTime CreatedOn { get; set; }
}