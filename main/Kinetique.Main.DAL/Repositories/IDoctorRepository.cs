using Kinetique.Main.Model;

namespace Kinetique.Main.DAL.Repositories;

public interface IDoctorRepository
{
    public Task<IEnumerable<Doctor>> GetAll();
    public Task<Doctor?> Get(long id);
    public Task<Doctor> Add(Doctor appointment);
    public Task Update(Doctor appointment);
}