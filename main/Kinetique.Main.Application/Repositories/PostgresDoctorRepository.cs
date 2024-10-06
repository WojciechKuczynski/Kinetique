using Kinetique.Main.Application.Exceptions;
using Kinetique.Main.DAL;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Main.Application.Repositories;

public sealed class PostgresDoctorRepository(DataContext _context) : PostgresRepositoryBase<Doctor>(_context), IDoctorRepository
{
    public async Task<Doctor> FindByLicence(string licence)
    {
        var doctor = await _objects.SingleOrDefaultAsync(x => x.Licence == licence);
        if (doctor == null)
        {
            throw new DoctorNotFoundException(licence);
        }

        return doctor;
    }
}