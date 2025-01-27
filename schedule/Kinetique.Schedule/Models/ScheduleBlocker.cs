using System.ComponentModel.DataAnnotations.Schema;
using Kinetique.Shared.Model;

namespace Kinetique.Schedule.Models;

public class ScheduleBlocker : BaseModel
{
    // it may be 2h in some day or 2 weeks
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    [ForeignKey("DoctorSchedule")]
    public long DoctorScheduleId { get; set; }
    public virtual DoctorSchedule DoctorSchedule { get; set; }
}