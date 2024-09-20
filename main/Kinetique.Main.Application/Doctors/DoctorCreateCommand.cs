using Kinetique.Main.Application.Dtos;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Main.Application.Doctors;

public record DoctorCreateCommand(DoctorDto Doctor) : ICommandRequest;
