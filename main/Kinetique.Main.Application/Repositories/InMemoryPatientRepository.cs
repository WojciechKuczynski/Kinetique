using Kinetique.Main.Application.Exceptions;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Main.Application.Repositories;

public class InMemoryPatientRepository : InMemoryBaseRepository<Patient>, IPatientRepository
{
    public Task<Patient> FindByPesel(string pesel)
    {
        var patient = _objects.Find(x => x.Pesel == pesel);
        if (patient == null)
        {
            throw new PatientNotFoundException(pesel);
        }

        return Task.FromResult(patient);
    }
}