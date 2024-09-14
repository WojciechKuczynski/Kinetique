using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Model;

namespace Kinetique.Main.Application.Mappers;

public static partial class Mapper
{
    public static DoctorDto MapToDto(this Doctor doctor)
    {
        return new DoctorDto
        (
            Id: doctor.Id,
            FirstName: doctor.FirstName,
            LastName: doctor.LastName,
            Gender: doctor.Gender,
            Licence: doctor.Licence
        );
    }

    public static Doctor MapToEntity(this DoctorDto doctorDto)
    {
        return new Doctor
        {
            Id = doctorDto.Id,
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            Gender = doctorDto.Gender,
            Licence = doctorDto.Licence
        };
    }
}