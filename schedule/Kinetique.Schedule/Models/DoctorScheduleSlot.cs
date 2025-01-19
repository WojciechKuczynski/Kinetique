using System.ComponentModel.DataAnnotations.Schema;
using Kinetique.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Schedule.Models;

public class DoctorScheduleSlot : BaseModel
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    
    [ForeignKey("DoctorSchedule")]
    public long DoctorScheduleId { get; set; }
    public virtual DoctorSchedule DoctorSchedule { get; set; }
}