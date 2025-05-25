using Kinetique.Nfz.DAL;
using Kinetique.Nfz.Model;
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
        //TODO: Finish it.
        foreach(var proc in command.Procedures)
        {
            var procedure = new StatisticProcedure()
            {
                Code = proc.StatisticProcedure,
                Treatment = proc.TreatmentCode,
                SettlementProcedure = new SettlementProcedure()
                {
                    Code = proc.SettlementProcedureCode,
                    Name = proc.SettlementProcedureCode,
                    Points = proc.Points
                }
            };
            patientAppointment.AddProcedure(procedure);
        }
        await _context.SaveChangesAsync(token);
    }
}