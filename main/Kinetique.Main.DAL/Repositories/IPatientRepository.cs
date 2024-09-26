using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Main.DAL.Repositories;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Patient> FindByPesel(string pesel);
}