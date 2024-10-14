using Kinetique.Nfz.Application.Dtos;
using Kinetique.Nfz.DAL;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Nfz.Application.Procedures.Handlers;

public interface IProcedureAllListHandler : IQueryHandler<ProcedureAllListQuery,IEnumerable<TreatmentProcedureDto>>;

internal class ProcedureAllListHandler(DataContext _context) : IProcedureAllListHandler
{
    public async Task<IEnumerable<TreatmentProcedureDto>> Handle(ProcedureAllListQuery query, CancellationToken token = default)
    {
        var procedures = await _context.SettlementProcedures.ToListAsync(token);
        return procedures.Select(x =>
            new TreatmentProcedureDto(x.Code, x.StatisticProcedure.Code, x.StatisticProcedure.Treatment, x.Points));
    }
}