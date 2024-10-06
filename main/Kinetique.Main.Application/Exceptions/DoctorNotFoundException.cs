namespace Kinetique.Main.Application.Exceptions;

public class DoctorNotFoundException : Exception
{
    public DoctorNotFoundException(long id) : base($"Doctor with Id {id} was not found")
    {
    }

    public DoctorNotFoundException(string licence) : base($"Doctor with licence number {licence} was not found")
    {
    }
}