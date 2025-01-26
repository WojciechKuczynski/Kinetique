
using Kinetique.Schedule.Models.ValueObjects;
using Kinetique.Shared.Model;

namespace Kinetique.Schedule.Models;

public class DoctorSchedule : BaseModel
{
    public string DoctorCode { get; set; }
    public Date StartDate { get; set; }
    public Date EndDate { get; set; }
    public virtual List<DoctorScheduleSlot> Slots { get; set; } = [];
    public virtual List<ScheduleBlocker> Blockers { get; set; } = [];

    public void AddSlots(IEnumerable<DoctorScheduleSlot> slots)
    {
        Slots.AddRange(slots);
    }

    public void AddBlocks(IEnumerable<ScheduleBlocker> blocks)
    {
        Blockers.AddRange(blocks);
    }
}