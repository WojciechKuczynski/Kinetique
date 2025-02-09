namespace Kinetique.Nfz.Application.Dtos;

public class PatientProcedureDto
{
    public string PatientPesel { get; set; }
    public long AppointmentExternalId { get; set; }
    public virtual List<string> Procedures { get; set; }
}