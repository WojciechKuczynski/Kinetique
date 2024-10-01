using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class SettlemenetProcedure : BaseModel
{
    // procedure number/name
    public string Name { get; set; }
    // code
    public string Code { get; set; }
    public decimal Points { get; set; }
    
    public virtual List<StatisticProcedureGroup> StatisticProcedures { get; set; }
    
    
}