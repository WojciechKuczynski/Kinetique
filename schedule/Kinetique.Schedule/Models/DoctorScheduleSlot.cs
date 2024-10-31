namespace Kinetique.Schedule.Models;

public class DoctorScheduleSlot
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    
    public virtual DoctorSchedule DoctorSchedule { get; set; }
}