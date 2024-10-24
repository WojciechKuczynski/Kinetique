using Kinetique.Shared.Model;

namespace Kinetique.Schedule.Model;

public class ScheduleBlocker : BaseModel
{
    public long DoctorId { get; set; }
    // it may be 2h in some day or 2 weeks
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}