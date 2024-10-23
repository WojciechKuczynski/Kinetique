using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Patients.RequestArgs;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Main.Application.Patients;

public record PatientSingleQuery(PatientQueryRequest Args) : IQuery<PatientDto>;