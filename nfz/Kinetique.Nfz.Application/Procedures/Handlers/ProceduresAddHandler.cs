using Kinetique.Nfz.DAL;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Nfz.Application.Procedures.Handlers;

public interface IProcedureAddHandler : ICommandHandler<ProceduresAddCommand>;
 

internal class ProceduresAddHandler(DataContext _context) : IProcedureAddHandler
{
    public async Task Handle(ProceduresAddCommand command, CancellationToken token = default)
    {
        var patientAppointment = await _context.PatientProcedures.FindAsync(command.AppointmentId, token);
        if (patientAppointment is null)
        {
            throw new Exception("Patient appointment not found");
        }
        
        
        
    }
}