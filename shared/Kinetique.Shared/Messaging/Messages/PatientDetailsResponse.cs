namespace Kinetique.Shared.Messaging.Messages;

public record PatientDetailsResponse(long Id, string FirstName = "", string LastName = "", string PhoneNumber = "", string Address = "", string Pesel = ""): IRabbitRequest;