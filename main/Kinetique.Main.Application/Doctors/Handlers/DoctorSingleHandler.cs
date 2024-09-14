using Kinetique.Main.Application.Abstractions;
using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Mappers;
using Kinetique.Main.DAL.Repositories;

namespace Kinetique.Main.Application.Doctors.Handlers;

public interface IDoctorSingleHandler : IQueryHandler<DoctorSingleQuery, DoctorDto?>;

internal sealed class DoctorSingleHandler(IDoctorRepository _doctorRepository) : IDoctorSingleHandler
{
    public async Task<DoctorDto?> Handle(DoctorSingleQuery query, CancellationToken token = default)
    {
        var doctor = await _doctorRepository.Get(query.Id);
        return doctor?.MapToDto();
    }
}