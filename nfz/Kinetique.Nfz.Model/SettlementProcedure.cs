using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class SettlementProcedure : BaseModel
{
    // procedure number/name
    public string Name { get; set; }
    // code, for example 84
    public string Code { get; set; }
    // for what ever Statistic Procedure, always there is same amount of points.
    public decimal Points { get; set; }
    
    public virtual StatisticProcedure StatisticProcedure { get; set; }
}