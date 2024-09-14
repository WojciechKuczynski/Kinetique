using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;

namespace Kinetique.Main.Application.Repositories;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients;

    public Task<IEnumerable<Patient>> GetAll()
        => Task.FromResult(_patients.Select(x => x));

    public Task<Patient?> Get(long id)
        => Task.FromResult(_patients.FirstOrDefault(x => x.Id == id));

    public Task<Patient> Add(Patient patient)
    {
        _patients.Add(patient);
        
        return Task.FromResult(patient);
    }

    public Task Update(Patient patient)
    {
        var patientInSet = Get(patient.Id).Result;
        if (patientInSet != null)
        {
            _patients.Remove(patientInSet);
            _patients.Add(patient);
        }

        return Task.CompletedTask;
    }
}