using Kinetique.Nfz.Application.Dtos;
using Kinetique.Nfz.Model;

namespace Kinetique.Nfz.Application.Mappers;

public static partial class Mapper
{
    public static PatientProcedureDto MapToDto(this PatientProcedure patientProcedure)
    {
        return new PatientProcedureDto
        {
            PatientId = patientProcedure.PatientId,
            AppointmentId = patientProcedure.AppointmentId,
            Procedures = patientProcedure.Procedures.SelectMany(x => x.Codes).ToList()
        };
    }
}