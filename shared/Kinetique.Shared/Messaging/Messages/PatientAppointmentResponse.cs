namespace Kinetique.Shared.Messaging.Messages;

public record PatientAppointmentResponse(string TestResponse) : IRabbitRequest;