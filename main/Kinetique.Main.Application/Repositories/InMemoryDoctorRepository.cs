using Kinetique.Main.DAL.Repositories;
using Kinetique.Main.Model;
using Kinetique.Shared.Model.Repositories;

namespace Kinetique.Main.Application.Repositories;

public class InMemoryDoctorRepository : InMemoryBaseRepository<Doctor>, IDoctorRepository
{

}