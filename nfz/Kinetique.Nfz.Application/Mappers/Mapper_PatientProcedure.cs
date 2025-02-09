using Kinetique.Nfz.Application.Dtos;
using Kinetique.Nfz.Model;

namespace Kinetique.Nfz.Application.Mappers;

public static partial class Mapper
{
    public static PatientProcedureDto MapToDto(this PatientProcedure patientProcedure)
    {
        return new PatientProcedureDto
        {
            PatientPesel = patientProcedure.PatientPesel,
            AppointmentExternalId = patientProcedure.AppointmentExternalId,
            Procedures = patientProcedure.Procedures.Select(x => x.Code).ToList()
        };
    }
}