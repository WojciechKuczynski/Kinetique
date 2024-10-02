using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Nfz.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Nfz.Application.Repositories;

public class InMemoryPatientProcedureRepository : InMemoryBaseRepository<PatientProcedure>, IPatientProcedureRepository
{
    public async Task<IList<PatientProcedure>> GetProceduresToFill()
    {
        return await Task.FromResult(_objects.Where(x => x.Procedures.Count == 0).ToList());
    }
}