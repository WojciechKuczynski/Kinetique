namespace Kinetique.Shared.Messaging.Messages;

public record DoctorScheduleRequest(string DoctorCode, DateTime StartDate, DateTime EndDate) : IRabbitRequest;
