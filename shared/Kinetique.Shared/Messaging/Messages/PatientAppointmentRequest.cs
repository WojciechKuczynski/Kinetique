namespace Kinetique.Shared.Messaging.Messages;

public record PatientAppointmentRequest(long PatientId) : IRabbitRequest;