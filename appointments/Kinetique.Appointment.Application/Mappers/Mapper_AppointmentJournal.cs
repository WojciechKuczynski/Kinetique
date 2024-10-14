using Kinetique.Appointment.Application.Dtos;

namespace Kinetique.Appointment.Application.Mappers;

public partial class Mapper
{
    public static AppointmentJournalDto MapToDto(this Model.AppointmentJournal appointmentJournal)
        => new AppointmentJournalDto()
        {
            AppointmentId = appointmentJournal.AppointmentId,
            Status = appointmentJournal.Status,
            CreatedAt = appointmentJournal.CreatedAt,
            SentAt = appointmentJournal.SentAt
        };

    public static Model.AppointmentJournal MapToEntity(this AppointmentJournalDto appointmentJournalDto)
        => new Model.AppointmentJournal()
        {
            AppointmentId = appointmentJournalDto.AppointmentId,
            Status = appointmentJournalDto.Status,
            CreatedAt = appointmentJournalDto.CreatedAt,
            SentAt = appointmentJournalDto.SentAt
        };
}