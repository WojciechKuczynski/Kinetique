namespace Kinetique.Shared.Messaging.Messages;

public record AppointmentCreatedEvent(string DoctorCode, DateTime StartDate, DateTime EndDate) : IRabbitRequest;
