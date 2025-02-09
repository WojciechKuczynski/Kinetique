using Kinetique.Nfz.DAL;
using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Nfz.Model;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Nfz.Application.Repositories;

public class PostgresPatientProcedureRepository(DataContext context) : PostgresRepositoryBase<PatientProcedure>(context), IPatientProcedureRepository
{
    public async Task<IList<PatientProcedure>> GetProceduresToFill()
    {
        return await _objects.Where(x => x.Procedures.Count == 0).ToListAsync();
    }

    public new async Task<PatientProcedure> Add(PatientProcedure obj)
    {
        var appointmentPatient = await _objects.FirstOrDefaultAsync(x => x.AppointmentExternalId == obj.AppointmentExternalId);
        if (appointmentPatient != null)
        {
            return appointmentPatient;
        }

        var patientProcedure = await _objects.AddAsync(obj);
        await context.SaveChangesAsync();
        return patientProcedure.Entity;
    }
}