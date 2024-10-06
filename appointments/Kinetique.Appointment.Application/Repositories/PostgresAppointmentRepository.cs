using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class PostgresAppointmentRepository(DbContext context) : PostgresRepositoryBase<Model.Appointment>(context), IAppointmentRepository
{
    public async Task<IList<Model.Appointment>> GetAppointmentsForDoctor(long doctorId, DateTime? start = null, DateTime? end = null)
    {
        var query = _objects.AsQueryable().Where(x => x.DoctorId == doctorId);

        if (start.HasValue)
        {
            query = query.Where(a => 
                a.StartDate >= start.Value 
                || a.StartDate.Add(a.Duration) > start.Value); 
        }
       
        if (end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate <= end.Value 
                && a.StartDate.Add(a.Duration) < end.Value); 
        }

        return await query.ToListAsync();
    }

    public async Task<IList<Model.Appointment>> GetAppointmentsForPatient(long patientId, DateTime? start = null, DateTime? end = null)
    {
        var query = _objects.AsQueryable().Where(x => x.PatientId == patientId);

        if (start.HasValue)
        {
            query = query.Where(a => 
                a.StartDate >= start.Value 
                || a.StartDate.Add(a.Duration) >= start.Value); 
        }
       
        if (end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate <= end.Value 
                && a.StartDate.Add(a.Duration) <= end.Value); 
        }

        return await query.ToListAsync();
    }

    public async Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date)
    {
        var appointments = _objects.AsQueryable();
        if (date == null)
            appointments = appointments.Where(x => x.StartDate.Add(x.Duration) < DateTime.UtcNow);
        else
            appointments = appointments.Where(x => x.StartDate.Add(x.Duration) >= date);
        
        return await appointments.ToListAsync();
    }
}