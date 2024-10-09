using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class StatisticProcedure : BaseModel
{
    // code, for example 73
    public string Code { get; set; }
    // treatment, for example 1139
    public string Treatment { get; set; }
    public virtual required SettlementProcedure SettlementProcedure { get; set; }
}