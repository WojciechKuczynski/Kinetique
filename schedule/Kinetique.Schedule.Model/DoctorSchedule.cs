
using Kinetique.Schedule.Model.ValueObjects;
using Kinetique.Shared.Model;

namespace Kinetique.Schedule.Model;

public class DoctorSchedule : BaseModel
{
    public long DoctorId { get; set; }
    public Date StartDate { get; set; }
    public Date EndDate { get; set; }
    public virtual List<DoctorScheduleSlot> Slots { get; set; }
}