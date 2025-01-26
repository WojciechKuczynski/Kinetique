namespace Kinetique.Shared.Messaging.Messages;

public record DoctorScheduleResponse(bool CanAssign) : IRabbitRequest;
