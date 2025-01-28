namespace Kinetique.Shared.Messaging.Messages;

public record AppointmentRemovedEvent(string DoctorCode, DateTime StartDate, DateTime EndDate) : IRabbitRequest;