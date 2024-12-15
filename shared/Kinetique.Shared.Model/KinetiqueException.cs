using System.Runtime.CompilerServices;

namespace Kinetique.Shared.Model;

public class KinetiqueException(string exception) : Exception(exception)
{
    public string ExceptionMessage { get; init; } = exception;
    public string Code => GetType().Name.Replace("Exception", "");
}