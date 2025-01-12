using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class PostgresAppointmentRepository(DataContext context, IClock clock)
    : PostgresRepositoryBase<Model.AppointmentCycle>(context), IAppointmentRepository
{
    private readonly IClock _clock = clock;

    private IQueryable<Model.Appointment> _appointments => _objects.AsQueryable().SelectMany(x => x.Appointments);
    public async Task<IList<Model.Appointment>> GetAppointmentsForDoctor(long doctorId, DateTime? start = null, DateTime? end = null)
    {
        var query = _appointments.Where(x => x.Cycle.DoctorId == doctorId);

        if (start.HasValue)
        {
            query = query.Where(a => 
                a.StartDate >= start.Value 
                || a.StartDate.AddMinutes(a.Duration.TotalMinutes) > start.Value); 
        }
       
        if (end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate <= end.Value 
                && a.StartDate.AddMinutes(a.Duration.TotalMinutes) < end.Value); 
        }

        return await query.ToListAsync();
    }

    public async Task<IList<Model.Appointment>> GetAppointmentsForPatient(long patientId, DateTime? start = null, DateTime? end = null)
    {
        var query = _appointments.Where(x => x.Cycle.PatientId == patientId);

        if (start.HasValue)
        {
            query = query.Where(a => 
                a.StartDate >= start.Value 
                || a.StartDate.AddMinutes(a.Duration.TotalMinutes) >= start.Value); 
        }
       
        if (end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate <= end.Value 
                && a.StartDate.AddMinutes(a.Duration.TotalMinutes) <= end.Value); 
        }

        return await query.ToListAsync();
    }

    //TODO: to be refactored later
    public async Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date)
    {
        List<Model.Appointment> appointments = null;
        if (date == null)
        {
            appointments = await _appointments.Where(x => x.StartDate < _clock.GetNow()).ToListAsync();
            appointments = appointments.Where(x => x.StartDate.AddMinutes(x.Duration.TotalMinutes) < _clock.GetNow()).ToList();
        }
        else
        {
            appointments = await _appointments.Where(x => x.StartDate >= date).ToListAsync();
            appointments = appointments.Where(x => x.StartDate.AddMinutes(x.Duration.TotalMinutes) >= date).ToList();
        }
        
        return await Task.FromResult(appointments);
    }

    public async Task<Model.Appointment?> GetById(long id)
    {
        return await _objects.AsQueryable().SelectMany(x => x.Appointments).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AppointmentCycle?> GetOngoingCycleForPatient(long patientId)
    {
        var cycles = await _objects.Where(x => x.PatientId == patientId && !x.CycleFull).SingleOrDefaultAsync();
        return cycles;
    }

    public async Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(long doctorId)
    {
        var cycles = await _objects.Where(x => x.DoctorId == doctorId && !x.CycleFull).ToListAsync();
        return cycles;
    }
}