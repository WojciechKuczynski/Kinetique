using System.ComponentModel.DataAnnotations.Schema;

namespace Kinetique.Shared.Model;

public abstract class BaseModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime? LastUpdate { get; set; }
}