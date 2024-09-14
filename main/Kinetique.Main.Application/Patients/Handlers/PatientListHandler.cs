using Kinetique.Main.Application.Abstractions;
using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Mappers;
using Kinetique.Main.DAL.Repositories;

namespace Kinetique.Main.Application.Patients.Handlers;

public interface IPatientListHandler : IQueryHandler<PatientListQuery, IList<PatientDto>?>;

internal sealed class PatientListHandler(IPatientRepository _patientRepository) : IPatientListHandler
{
    public async Task<IList<PatientDto>?> Handle(PatientListQuery query, CancellationToken token = default)
    {
        var patients = await _patientRepository.GetAll();
        return patients.Select(x => x.MapToDto()).ToList();
    }
}