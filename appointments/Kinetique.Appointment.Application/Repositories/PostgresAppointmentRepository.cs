using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class PostgresAppointmentRepository(DataContext context) : PostgresRepositoryBase<Model.Appointment>(context), IAppointmentRepository
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

    //TODO: to be refactored later
    public async Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date)
    {
        List<Model.Appointment> appointments = null;
        if (date == null)
        {
            appointments = await _objects.AsQueryable().Where(x => x.StartDate < DateTime.UtcNow).ToListAsync();
            appointments = appointments.Where(x => x.StartDate.Add(x.Duration) < DateTime.UtcNow).ToList();
        }
        else
        {
            appointments = await _objects.AsQueryable().Where(x => x.StartDate >= date).ToListAsync();
            appointments = appointments.Where(x => x.StartDate.Add(x.Duration) >= date).ToList();
        }
        
        return await Task.FromResult(appointments);
    }
}