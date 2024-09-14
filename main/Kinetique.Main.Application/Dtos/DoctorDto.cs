using Kinetique.Main.Model;

namespace Kinetique.Main.Application.Dtos;

[Serializable]

public record DoctorDto(long Id, string FirstName, string LastName, Gender Gender, string Licence);
