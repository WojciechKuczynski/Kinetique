using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class StatisticProcedureGroup : BaseModel
{
    public List<string> Codes { get; set; }
    public virtual SettlemenetProcedure SettlemenetProcedure { get; set; }
}