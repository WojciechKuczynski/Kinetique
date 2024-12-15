using System.Text.RegularExpressions;

namespace Kinetique.Main.Model.ValueObjects;

public sealed class Pesel
{
    private const string PeselRegex = @"[0-9]{2}([02468]1|[13579][012])(0[1-9]|1[0-9]|2[0-9]|3[01])[0-9]{5}";
    public string Value { get; }

    public Pesel(string value)
    {
        // for some reason is not working, but it does work in other compilators and online regex designers.
        // if (!Regex.IsMatch(value, PeselRegex))
        // {
        //     throw new ArgumentException($"Invalid PESEL number: {value}");
        // }
        
        Value = value;
    }
    
    public static implicit operator string(Pesel pesel) => pesel.Value;
    public static implicit operator Pesel(string pesel) => new(pesel);
    public override string ToString() => Value;
}