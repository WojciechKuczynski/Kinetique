using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Shared.Messaging.Messages;

public class PatientDetailsRequest : IRabbitRequest
{
    public string Pesel { get; set; }
    public long? PatientId { get; set; }

    public PatientDetailsRequest(string pesel)
    {
        Pesel = pesel;
    }

    public PatientDetailsRequest(long id)
    {
        PatientId = id;
    }

    public PatientDetailsRequest()
    {
        
    }
}
