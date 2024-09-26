namespace Kinetique.Main.Model.ValueObjects;

public sealed class Pesel
{
    public string Value { get; }

    public Pesel(string value)
    {
        // validaton later
        Value = value;
    }
    
    public static implicit operator string(Pesel pesel) => pesel.Value;
    public static implicit operator Pesel(string pesel) => new(pesel);
    public override string ToString() => Value;
}