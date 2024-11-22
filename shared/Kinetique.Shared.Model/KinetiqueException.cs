namespace Kinetique.Shared.Model;

public class KinetiqueException(string exception) : Exception(exception)
{
    public string ExceptionMessage { get; init; } = exception;
    public string Code { get; init; } = exception.GetType().ToString().Replace("Exception", "");
}