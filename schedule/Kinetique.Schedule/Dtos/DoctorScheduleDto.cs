using Kinetique.Schedule.Models.ValueObjects;

namespace Kinetique.Schedule.Dtos;

[Serializable]
public class DoctorScheduleDto
{
    public long DoctorId { get; set; }
    public Date StartDate { get; set; }
    public Date EndDate { get; set; }
    public List<ScheduleSlotDto> Slots { get; set; } = [];
}