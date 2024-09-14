namespace Kinetique.Main.Model.ValueObjects;

public sealed class Pesel
{
    public string Value { get; }

    public Pesel(string value)
    {
        // validaton later
        Value = value;
    }
}