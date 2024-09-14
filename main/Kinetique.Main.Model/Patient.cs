using Kinetique.Main.Model.ValueObjects;

namespace Kinetique.Main.Model;

public class Patient : BaseModel
{
    public Pesel Pesel { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Address Address { get; set; }
    public Gender Gender { get; set; }
    public string Description { get; set; }
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Kid = 3
}