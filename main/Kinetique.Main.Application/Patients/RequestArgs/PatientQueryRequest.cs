namespace Kinetique.Main.Application.Patients.RequestArgs;

public class PatientQueryRequest
{
    public long? Id { get; set; }
    public string Pesel { get; set; }
}