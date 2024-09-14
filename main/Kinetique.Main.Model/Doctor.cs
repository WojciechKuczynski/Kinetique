namespace Kinetique.Main.Model;

public class Doctor : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    /// <summary>
    /// Medical licence number
    /// </summary>
    public string Licence { get; set; }
}