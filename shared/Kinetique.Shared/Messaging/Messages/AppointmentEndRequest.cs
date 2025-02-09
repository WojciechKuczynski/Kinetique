namespace Kinetique.Shared.Messaging.Messages;

public record AppointmentEndRequest(long AppointmentId, string PatientPesel) : IRabbitRequest;
