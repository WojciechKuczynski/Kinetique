using Kinetique.Appointment.Application.Dtos;
using Kinetique.Shared.Dtos;

namespace Kinetique.Appointment.Application.Mappers;

public static partial class Mapper
{
    public static AppointmentCycleDto MapToDto(this Model.AppointmentCycle cycle)
        => new AppointmentCycleDto()
        {
            Id = cycle.Id,
            StartDate = cycle.StartDate,
            Limit = cycle.Limit,
            DoctorId = cycle.DoctorId,
            PatientId = cycle.PatientId,
            Referral = cycle.Referral?.MapToDto() ?? null,
            Appointments = cycle.Appointments.Select(x => x.MapToDto()).ToList()
        };

    public static Model.AppointmentCycle MapToEntity(this AppointmentCycleDto cycleDto)
        => new Model.AppointmentCycle()
        {
            Id = cycleDto.Id,
            StartDate = cycleDto.StartDate,
            Limit = cycleDto.Limit,
            DoctorId = cycleDto.DoctorId,
            PatientId = cycleDto.PatientId,
            Referral = cycleDto.Referral?.MapToEntity() ?? null,
            Appointments = cycleDto.Appointments?.Select(x => x.MapToEntity()).ToList() ?? new List<Model.Appointment>()
        };
}