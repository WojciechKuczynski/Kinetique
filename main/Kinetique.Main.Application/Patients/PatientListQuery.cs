using Kinetique.Main.Application.Dtos;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Main.Application.Patients;

public record PatientListQuery : IQuery<IList<PatientDto>>;