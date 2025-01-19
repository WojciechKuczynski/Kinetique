using Kinetique.Schedule.Models;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Requests;

public record DoctorScheduleListQuery(long DoctorId) : IQuery<IEnumerable<DoctorSchedule>>;
