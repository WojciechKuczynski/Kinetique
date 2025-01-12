using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Repositories;

public class InMemoryAppointmentRepository(IClock clock) : IAppointmentRepository
{
    private readonly IList<AppointmentCycle> _appointmentCycles = [];

    private IQueryable<Model.Appointment> _appointments =>
        _appointmentCycles.AsQueryable().SelectMany(x => x.Appointments);

    public async Task<IList<Model.Appointment>> GetAppointmentsForDoctor(long doctorId, DateTime? start = null, DateTime? end = null)
    {
        var query = _appointments.Where(x => x.Cycle.DoctorId == doctorId);

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
        var query = _appointments.Where(x => x.Cycle.PatientId == patientId);

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
            appointents = _appointments.Where(x => x.StartDate.Add(x.Duration) < clock.GetNow()).ToList();
        else
            appointents = _appointments.Where(x => x.StartDate.Add(x.Duration) >= date).ToList();
        
        return await Task.FromResult(appointents);
    }

    public Task<Model.Appointment?> GetById(long id)
    {
        return Task.FromResult(_appointments.SingleOrDefault(x => x.Id == id));
    }

    public Task<IEnumerable<AppointmentCycle>> GetAll()
    {
        return Task.FromResult(_appointmentCycles.AsEnumerable());
    }

    public Task<AppointmentCycle?> Get(long id)
    {
        return Task.FromResult(_appointmentCycles.SingleOrDefault(x => x.Id == id));
    }

    public Task<AppointmentCycle> Add(AppointmentCycle obj)
    {
        if (obj.Id > 0)
            throw new InvalidOperationException("Cannot add Cycle with Id");
        
        _appointmentCycles.Add(obj);
        obj.Id = _appointmentCycles.Count;
        return Task.FromResult(obj);
    }

    public Task Update(AppointmentCycle obj)
    {
        // There is nothing that can be updated
        return Task.CompletedTask;
    }

    public Task<AppointmentCycle?> GetOngoingCycleForPatient(long patientId)
    {
        var cycle = _appointmentCycles.SingleOrDefault(x => x.PatientId == patientId && !x.CycleFull);
        return Task.FromResult(cycle);
    }

    public Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(long doctorId)
    {
        var cycles = _appointmentCycles.Where(x => x.DoctorId == doctorId && !x.CycleFull);
        return Task.FromResult(cycles);
    }
}