using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Repositories;

public class InMemoryAppointmentRepository : InMemoryBaseRepository<Model.Appointment>, IAppointmentRepository
{
    private readonly IClock _clock;
    public InMemoryAppointmentRepository(IClock clock)
    {
        _clock = clock;
    }
    
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

        return await Task.FromResult(query.ToList());
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

        return await Task.FromResult(query.ToList());
    }

    public async Task<IList<Model.Appointment>> GetAppointmentsFinishedAfter(DateTime? date)
    {
        List<Model.Appointment> appointents;
        if (date == null)
            appointents = _objects.Where(x => x.StartDate.Add(x.Duration) < _clock.GetNow()).ToList();
        else
            appointents = _objects.Where(x => x.StartDate.Add(x.Duration) >= date).ToList();
        
        return await Task.FromResult(appointents);
    }
}