using Kinetique.Main.Model;

namespace Kinetique.Main.Application.Dtos;

[Serializable]
public record PatientDto(long Id, string FirstName = "", string LastName = "", string PhoneNumber = "", string Address = "", Gender Gender = Gender.Male, string Pesel = "");

