using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Mappers;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Main.Application.Doctors.Handlers;

public interface IDoctorListHandler : IQueryHandler<DoctorListQuery, IList<DoctorDto>?>;

internal sealed class DoctorListHandler(IDoctorRepository _doctorRepository) : IDoctorListHandler
{
    public async Task<IList<DoctorDto>?> Handle(DoctorListQuery query, CancellationToken token = default)
    {
        var doctors = await _doctorRepository.GetAll();
        return doctors.Select(x => x.MapToDto()).ToList();
    }
}