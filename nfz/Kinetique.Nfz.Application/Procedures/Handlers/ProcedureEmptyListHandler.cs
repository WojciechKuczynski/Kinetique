using Kinetique.Nfz.Application.Dtos;
using Kinetique.Nfz.Application.Mappers;
using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Nfz.Application.Procedures.Handlers;

public interface IProcedureEmptyListHandler : IQueryHandler<ProcedureEmptyListQuery, IList<PatientProcedureDto>>;

public class ProcedureEmptyListHandler(IPatientProcedureRepository patientProcedureRepository)
    : IProcedureEmptyListHandler
{
    private readonly IPatientProcedureRepository _patientProcedureRepository = patientProcedureRepository;
    public async Task<IList<PatientProcedureDto>> Handle(ProcedureEmptyListQuery query, CancellationToken token = default)
    {
        return (await _patientProcedureRepository.GetProceduresToFill()).Select(x => x.MapToDto()).ToList();
    }
}