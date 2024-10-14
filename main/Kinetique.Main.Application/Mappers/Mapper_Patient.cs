using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Model;

namespace Kinetique.Main.Application.Mappers;

public static partial class Mapper
{
    public static PatientDto MapToDto(this Model.Patient patient)
    {
        return new PatientDto
        (
            Id: patient.Id,
            FirstName: patient.FirstName,
            LastName: patient.LastName,
            Gender: patient.Gender,
            PhoneNumber: patient.PhoneNumber,
            Address: patient.Address,
            Pesel: patient.Pesel
        );
    }

    public static Model.Patient MapToEntity(this PatientDto patientDto)
    {
        return new Model.Patient()
        {
            Id = patientDto.Id,
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Gender = patientDto.Gender,
            Pesel = patientDto.Pesel,
            PhoneNumber = patientDto.PhoneNumber,
            Address = patientDto.Address,
            Description = ""
        };
    }
}