using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class PatientProcedure : BaseModel
{
    public long PatientId { get; init; }
    public long AppointmentId { get; init; }
    public virtual List<StatisticProcedure> Procedures { get; init; } = [];
    
    public void AddProcedure(StatisticProcedure procedure)
    {
        if (procedure.SettlementProcedure == null)
        {
            throw new Exception("Please assign statistic Procedure to SettlementProcedure");
        }
        
        if (Procedures.Count(x => x.SettlementProcedure.Code == procedure.SettlementProcedure.Code) >= 2)
        {
            throw new Exception($"Cannot add more than 2 procedures with the same code {procedure.SettlementProcedure.Code}");
        }
        
        Procedures.Add(procedure);
        Status = SendStatus.InProgress;
    }
    public SendStatus Status { get; set; }
    
    public decimal Points => Procedures.Sum(x => x.SettlementProcedure.Points);    

    public bool IsValid => Procedures
        .GroupBy(x => x.SettlementProcedure.Code)
        .All(c => c.Count() <= 2);
}

public enum SendStatus
{
    New,
    InProgress,
    Error,
    Sent
}