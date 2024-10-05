using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class PatientProcedure : BaseModel
{
    public long PatientId { get; init; }
    public long AppointmentId { get; init; }
    public virtual List<StatisticProcedureGroup> Procedures { get; init; } = [];
    public SendStatus Status { get; init; }

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