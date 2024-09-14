using Kinetique.Main.Application.Abstractions;
using Kinetique.Main.Application.Mappers;
using Kinetique.Main.Application.Storage;
using Kinetique.Main.DAL.Repositories;

namespace Kinetique.Main.Application.Doctors.Handlers;

public interface IDoctorCreateHandler : ICommandHandler<DoctorCreateCommand>
{
}

internal sealed class DoctorCreateHandler(IDoctorRepository _doctorRepository, IResponseStorage _responseStorage)
    : IDoctorCreateHandler
{
    public async Task Handle(DoctorCreateCommand request, CancellationToken cancellationToken)
    {
        var doctorEntity = request.Doctor.MapToEntity();
        var result = await _doctorRepository.Add(doctorEntity);
        _responseStorage.Set(ObjectConstants.Doctor, result.Id);
    }
}