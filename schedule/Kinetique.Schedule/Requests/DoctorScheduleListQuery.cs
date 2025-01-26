using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests;

public record DoctorScheduleListQuery(string DoctorCode) : IQuery<IEnumerable<DoctorSchedule>>;
