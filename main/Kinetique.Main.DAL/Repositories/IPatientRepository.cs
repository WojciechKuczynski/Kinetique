using Kinetique.Main.Model;

namespace Kinetique.Main.DAL.Repositories;

public interface IPatientRepository
{
    public Task<IEnumerable<Patient>> GetAll();
    public Task<Patient?> Get(long id);
    public Task<Patient> Add(Patient appointment);
    public Task Update(Patient appointment);
}