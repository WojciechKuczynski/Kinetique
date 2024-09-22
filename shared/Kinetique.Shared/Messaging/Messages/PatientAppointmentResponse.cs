using Kinetique.Shared.Dtos;

namespace Kinetique.Shared.Messaging.Messages;

public record PatientAppointmentResponse(IList<AppointmentSharedDto> Appointments) : IRabbitRequest;