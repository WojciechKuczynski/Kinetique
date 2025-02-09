using Kinetique.Schedule.Dtos;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests;

public record DoctorScheduleSlotQuery(DateTime StartDate, DateTime EndDate, string DoctorCode, bool Occupied) : IQuery<IEnumerable<ScheduleSlotDto>>;
