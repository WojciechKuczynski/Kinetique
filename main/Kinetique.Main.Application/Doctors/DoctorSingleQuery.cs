using Kinetique.Main.Application.Abstractions;
using Kinetique.Main.Application.Dtos;

namespace Kinetique.Main.Application.Doctors;

public record DoctorSingleQuery(long Id) : IQuery<DoctorDto>;