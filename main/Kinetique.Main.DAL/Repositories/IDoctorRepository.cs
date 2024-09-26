using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Main.DAL.Repositories;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<Doctor> FindByLicence(string licence);
}