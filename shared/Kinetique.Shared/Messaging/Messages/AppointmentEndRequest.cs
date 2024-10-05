namespace Kinetique.Shared.Messaging.Messages;

public record AppointmentEndRequest(long AppointmentId, long PatientId) : IRabbitRequest;
