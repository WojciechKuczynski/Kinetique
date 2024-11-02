
using Kinetique.Schedule.Models.ValueObjects;
using Kinetique.Shared.Model;

namespace Kinetique.Schedule.Models;

public class DoctorSchedule : BaseModel
{
    public long DoctorId { get; set; }
    public Date StartDate { get; set; }
    public Date EndDate { get; set; }
    public virtual List<DoctorScheduleSlot> Slots { get; set; }

    public void AddSlot(DoctorScheduleSlot slot)
    {
        if (Slots == null)
            Slots = [];
        Slots.Add(slot);
    }
    
    public void AddSlots(IEnumerable<DoctorScheduleSlot> slots)
    {
        if (Slots == null)
            Slots = [];
        Slots.AddRange(slots);
    }
}