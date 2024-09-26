namespace Kinetique.Main.Application.Exceptions;

public class DoctorNotFoundException : Exception
{
    public DoctorNotFoundException(long id) : base($"Doctor with Id {id} was not found")
    {
    }

    public DoctorNotFoundException(string pesel) : base($"Doctor with licence number {pesel} was not found")
    {
    }
}