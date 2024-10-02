using Kinetique.Nfz.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Nfz.DAL.Repositories;

public interface IPatientProcedureRepository : IBaseRepository<PatientProcedure>
{
    Task<IList<PatientProcedure>> GetProceduresToFill();
}