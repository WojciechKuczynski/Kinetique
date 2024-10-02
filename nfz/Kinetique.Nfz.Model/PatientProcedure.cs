using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class PatientProcedure : BaseModel
{
    public long PatientId { get; set; }
    public long AppointmentId { get; set; }
    public virtual List<StatisticProcedureGroup> Procedures { get; set; }
    public SendStatus Status { get; set; }

    public bool IsValid => Procedures
        .GroupBy(x => x.SettlemenetProcedure.Code)
        .All(c => c.Count() <= 2);
}

public enum SendStatus
{
    New,
    InProgress,
    Error,
    Sent
}