using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.Application.Repositories;

public class InMemoryAppointmentRepository(IClock clock) : IAppointmentRepository
{
    private readonly IList<AppointmentCycle> _appointmentCycles = [];

    private IQueryable<Model.Appointment> _appointments =>
        _appointmentCycles.AsQueryable().SelectMany(x => x.Appointments);

    public async Task<IList<Model.Appointment>> GetAppointmentsForDoctor(string doctorCode, DateTime? start = null, DateTime? end = null)
    {
        var query = _appointments.Where(x => x.Cycle.DoctorCode == doctorCode);

        if (start.HasValue)
        {
            query = query.Where(a => 
                a.StartDate >= start.Value 
                || a.StartDate.Add(a.Duration) > start.Value); 
        }

        var checkPoint = query.ToList();
       
        if (end.HasValue)
        {
            query = query.Where(a => 
                a.StartDate <= end.Value 
                && a.StartDate.Add(a.Duration) < end.Value); 
        }

        var result = query.ToList();
        return await Task.FromResult(result);
    }

    public async Task<IList<Model.Appointment>> GetAppointmentsForPatient(Pesel patientPesel, DateTime? start = null, DateTime? end = null)
    {
        var query = _appointments.Where(x => x.Cycle.PatientPesel.Equals(patientPesel));

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

    public Task RemoveAppointment(Model.Appointment appointment)
    {
        throw new NotImplementedException();
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

    public Task<AppointmentCycle?> GetOngoingCycleForPatient(Pesel patientPesel)
    {
        var cycle = _appointmentCycles.SingleOrDefault(x => x.PatientPesel.Equals(patientPesel) && !x.CycleFull);
        return Task.FromResult(cycle);
    }

    public Task<IEnumerable<AppointmentCycle>> GetOngoingCyclesForDoctor(string doctorCode)
    {
        var cycles = _appointmentCycles.Where(x => x.DoctorCode == doctorCode && !x.CycleFull);
        return Task.FromResult(cycles);
    }

    public Task<AppointmentCycle> AddOrUpdate(AppointmentCycle cycle)
    {
        return cycle.Id == 0 ? Add(cycle) : Task.FromResult(cycle);
    }
}