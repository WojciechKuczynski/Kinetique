using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Repositories;
using Kinetique.Shared.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class PostgresAppointmentRepository(DataContext context, IClock clock)
    : PostgresRepositoryBase<Model.AppointmentCycle>(context), IAppointmentRepository
{
    private readonly IClock _clock = clock;

    private IQueryable<Model.Appointment> _appointments => _objects.AsQueryable().SelectMany(x => x.Appointments);
    public async Task<IList<Model.Appointment>> GetAppointmentsForDoctor(string doctorCode, DateTime? start = null, DateTime? end = null)
    {
        var query = _appointments.Where(x => x.Cycle.DoctorCode == doctorCode);
        query = FindOverlappingAppointments(start, end, query);
        return await query.ToListAsync();
    }
  
    public async Task<IList<Model.Appointment>> GetAppointmentsForPatient(Pesel patientPesel, DateTime? start = null, DateTime? end = null)
     {
        var query = _appointments.Where(x => x.Cycle.PatientPesel.Equals(patientPesel));
        query = FindOverlappingAppointments(start, end, query);
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

    public async Task RemoveAppointment(Model.Appointment appointment)
    {
        var cycle = appointment.Cycle;
        cycle.Appointments.Remove(appointment);
        await Update(cycle);
    }

    public async Task<AppointmentCycle?> GetOngoingCycleForPatient(Pesel patientPesel)
    {
        var cycles = await _objects.Where(x => x.PatientPesel.Equals(patientPesel) && !x.CycleFull).SingleOrDefaultAsync();
        return cycles;
    }

    public async Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(string doctorCode)
    {
        var cycles = await _objects.Where(x => x.DoctorCode == doctorCode && !x.CycleFull).ToListAsync();
        return cycles;
    }

    public async Task<AppointmentCycle> AddOrUpdate(AppointmentCycle cycle)
    {
        if (cycle.Id == 0)
            return await Add(cycle);
        await Update(cycle);
        return cycle;
    }
    
    private static IQueryable<Model.Appointment> FindOverlappingAppointments(DateTime? start, DateTime? end, IQueryable<Model.Appointment> query)
    {
        if (start.HasValue && end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate < end.Value && 
                a.StartDate.AddMinutes(a.Duration.TotalMinutes) > start.Value);
        }
        else if (start.HasValue)
        {
            query = query.Where(a => 
                a.StartDate.AddMinutes(a.Duration.TotalMinutes) > start.Value);
        }
        else if (end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate < end.Value);
        }

        return query;
    }
}