using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Mappers;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Main.Application.Patients.Handlers;

public interface IPatientSingleHandler : IQueryHandler<PatientSingleQuery, PatientDto?>;

internal sealed class PatientSingleHandler(IPatientRepository _patientRepository) : IPatientSingleHandler
{
    public async Task<PatientDto?> Handle(PatientSingleQuery query, CancellationToken token = default)
    {
        var patient = await _patientRepository.Get(query.Id);
        return patient?.MapToDto();
    }
}