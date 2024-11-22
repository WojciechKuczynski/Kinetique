using Kinetique.Shared.Model;

namespace Kinetique.Main.Application.Exceptions;

public class PatientNotFoundException : KinetiqueException
{
    public PatientNotFoundException(long id) : base($"Patient with Id {id} was not found")
    {
    }

    public PatientNotFoundException(string pesel) : base($"Patient with pesel {pesel} was not found")
    {
    }
}