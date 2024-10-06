using Kinetique.Main.Application.Exceptions;
using Kinetique.Main.DAL;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Main.Application.Repositories;

public sealed class PostgresPatientRepository(DataContext _context) : PostgresRepositoryBase<Patient>(_context), IPatientRepository
{
    public async Task<Patient> FindByPesel(string pesel)
    {
        var patient = await _objects.SingleOrDefaultAsync(x => x.Pesel == pesel);
        if (patient == null)
        {
            throw new PatientNotFoundException(pesel);
        }

        return patient;
    }
}