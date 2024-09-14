using Kinetique.Main.Application.Abstractions;
using Kinetique.Main.Application.Dtos;

namespace Kinetique.Main.Application.Patients;

public record PatientCreateCommand(PatientDto Patient) : ICommandRequest;
