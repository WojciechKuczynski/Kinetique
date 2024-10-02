using Kinetique.Nfz.Application.Dtos;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Nfz.Application.Procedures;

public record ProcedureEmptyListQuery : IQuery<IList<PatientProcedureDto>>;
