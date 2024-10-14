using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Nfz.Application.Procedures;

public record ProceduresAddCommand(long AppointmentId, IList<string> codes) : ICommandRequest;