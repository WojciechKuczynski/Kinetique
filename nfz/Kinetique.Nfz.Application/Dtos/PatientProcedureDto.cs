namespace Kinetique.Nfz.Application.Dtos;

public class PatientProcedureDto
{
    public long PatientId { get; set; }
    public long AppointmentId { get; set; }
    public virtual List<string> Procedures { get; set; }
}