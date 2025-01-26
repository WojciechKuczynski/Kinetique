using Kinetique.Schedule.Dtos;
using Kinetique.Schedule.Models.ValueObjects;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests;

[Serializable]
public record DoctorScheduleRequest(
    string DoctorCode,
    Date StartDate,
    Date EndDate, 
    IList<ScheduleSlotDto> Slots) : ICommandRequest;
