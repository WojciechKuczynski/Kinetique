using Kinetique.Main.Application.Mappers;
using Kinetique.Main.Application.Storage;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;

namespace Kinetique.Main.Application.Patients.Handlers;

public interface IPatientCreateHandler : ICommandHandler<PatientCreateCommand>
{
}

internal sealed class PatientCreateHandler(IPatientRepository _patientRepository, IResponseStorage _responseStorage)
    : IPatientCreateHandler
{
    public async Task Handle(PatientCreateCommand request, CancellationToken cancellationToken)
    {
        var patientEntity = request.Patient.MapToEntity();
        var result = await _patientRepository.Add(patientEntity);
        _responseStorage.Set(ObjectConstants.Patient, result.Id);
    }
}