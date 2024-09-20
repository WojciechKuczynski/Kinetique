using Kinetique.Appointment.Application.Dtos;

namespace Kinetique.Appointment.Application.Mappers;

public static partial class Mapper
{
    public static AppointmentDto MapToDto(this Model.Appointment appointment)
        => new AppointmentDto()
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            Description = appointment.Description,
            Duration = appointment.Duration,
            StartDate = appointment.StartDate
        };

    public static Model.Appointment MapToEntity(this AppointmentDto appointmentDto)
        => new Model.Appointment()
        {
            Id = appointmentDto.Id,
            DoctorId = appointmentDto.DoctorId,
            PatientId = appointmentDto.PatientId,
            Description = appointmentDto.Description,
            Duration = appointmentDto.Duration,
            StartDate = appointmentDto.StartDate
        };
}