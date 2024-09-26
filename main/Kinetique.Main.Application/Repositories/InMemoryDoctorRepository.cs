using Kinetique.Main.Application.Exceptions;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Main.Application.Repositories;

public class InMemoryDoctorRepository : InMemoryBaseRepository<Doctor>, IDoctorRepository
{
    public Task<Doctor> FindByLicence(string licence)
    {
        var patient = _objects.Find(x => x.Licence == licence);
        if (patient == null)
        {
            throw new DoctorNotFoundException(licence);
        }

        return Task.FromResult(patient);
    }
}