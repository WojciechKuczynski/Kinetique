using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;

namespace Kinetique.Main.Application.Repositories;

public class InMemoryDoctorRepository : IDoctorRepository
{
    private readonly List<Doctor> _doctors = [];

    public Task<IEnumerable<Doctor>> GetAll()
        => Task.FromResult(_doctors.Select(x => x));

    public Task<Doctor?> Get(long id)
        => Task.FromResult(_doctors.FirstOrDefault(x => x.Id == id));

    public Task<Doctor> Add(Doctor doctor)
    {
        _doctors.Add(doctor);
        
        return Task.FromResult(doctor);
    }

    public Task Update(Doctor doctor)
    {
        var doctorInSet = Get(doctor.Id).Result;
        if (doctorInSet != null)
        {
            _doctors.Remove(doctorInSet);
            _doctors.Add(doctor);
        }

        return Task.CompletedTask;
    }
}